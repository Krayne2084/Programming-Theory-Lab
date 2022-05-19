using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRate = 2.5f;
    [SerializeField] float portalSpawnRate = 2f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject portalPrefab;
    [SerializeField] int maxEnemyCount = 10;
    [SerializeField] TextMeshProUGUI waveDisplay;
    [SerializeField] float waveDisplayTime = 2f;
    int spawnCount = 0;
    bool isSpawnReady = true;
    bool canSpawnEnemy = true;
    bool canSpawnPortal = true;
    bool doSpeedUp = false;
    Camera cam;
    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        StartCoroutine(CO_ShowWaveCount());
    }

    private void Update()
    {
        if (spawnCount < maxEnemyCount && canSpawnEnemy && isSpawnReady)
        {
            StartCoroutine(CO_SpawnTime("Enemy"));
        }
        if (canSpawnPortal && isSpawnReady)
        {
            StartCoroutine(CO_SpawnTime("Portal"));
        }
        if (!FindObjectOfType<Enemy>()&& isSpawnReady)
        {
            NextWave();
        }
    }

    IEnumerator CO_SpawnTime(string type)
    {
        if (type == "Enemy")
        {
            SpawnEnemy();
            spawnCount++;
            canSpawnEnemy = false;
            yield return new WaitForSeconds(spawnRate);
            canSpawnEnemy = true;
        }
        if(type == "Portal")
        {
            SpawnPortal();
            canSpawnPortal = false;
            yield return new WaitForSeconds(portalSpawnRate);
            canSpawnPortal = true;
            Destroy(MainManager.WorldPortal);
        }
    }

    IEnumerator CO_ShowWaveCount()
    {
        GameObject waveText = waveDisplay.gameObject;
        isSpawnReady = false;
        waveText.SetActive(true);
        yield return new WaitForSeconds(waveDisplayTime);
        isSpawnReady = true;
        waveText.SetActive(false);
    }

    void NextWave()
    {
        doSpeedUp = false;
        MainManager.wave++;
        int wave = MainManager.wave;

        if (wave % 2 == 0)
        {
            maxEnemyCount++;
        }
        else
        {
            if (MainManager.waveHealth < enemyPrefab.GetComponent<Enemy>().enemySprite.Length)
            {
                MainManager.waveHealth++;
            }
            else
            {
                doSpeedUp = true;
            }
        }

        waveDisplay.text = "Wave " + wave;
        StartCoroutine(CO_ShowWaveCount());
        Destroy(MainManager.WorldPortal);
        spawnCount = 0;
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = RandomPointOnScreen(2, 2);
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        if (doSpeedUp)
        {
            enemy.GetComponent<Enemy>().IncreaseSpeed();
        }
        enemy.SetActive(true);
    }

    void SpawnPortal()
    {
        Vector2 spawnPos = RandomPointOnScreen(-5, -5);
        MainManager.WorldPortal = Instantiate(portalPrefab, spawnPos, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
    }

    Vector2 RandomPointOnScreen(float xBuffer, float yBuffer)
    {
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float xPos = Random.Range(-screenBounds.x - xBuffer, screenBounds.x + xBuffer);
        float yPos = Random.Range(-screenBounds.y - yBuffer, screenBounds.y + yBuffer);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        return spawnPos;
    }
}
