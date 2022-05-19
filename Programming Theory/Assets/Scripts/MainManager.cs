using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainManager : GameManager
{
    [SerializeField] int startWave = 1;
    [SerializeField] int startWaveHealth = 2;
    public static int wave = 1;
    public static int waveHealth = 2;
    public static GameObject WorldPortal;
    [Header("MainManager")]
    [SerializeField] SpriteRenderer _CursorSprite;
    public static SpriteRenderer CursorSprite { get; private set; }
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] GameObject pauseText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    protected override void Awake()
    {
        base.Awake();
        CursorSprite = _CursorSprite;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        pauseScreen.SetActive(false);
        wave = startWave;
        waveHealth = startWaveHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !hasGameEnded)
        {
            PauseGame();
        }
    }
    private void UpdateScoreDisplay()
    {
        scoreText.text = "Waves: " + (wave - 1);
        SaveScore();
        Score score = LoadScore();
        if (score != null)
        {
            highScoreText.text = $"Most Waves: {score.playerName} - {score.m_waves}";
        }
        else
        {
            highScoreText.text = "Most Waves: 0";
        }
    }
    
    public void PauseGame()
    {
        if (isPaused)
        {
            ResetTime();
            pauseScreen.SetActive(false);

            return;
        }

        Time.timeScale = 0;

        pauseScreen.SetActive(true);
        pauseText.SetActive(true);
        gameOverText.SetActive(false);

        Cursor.visible = true;

        isPaused = true;
    }
    public void GameOver()
    {
        Time.timeScale = 0;

        pauseScreen.SetActive(true);
        pauseText.SetActive(false);
        gameOverText.SetActive(true);

        hasGameEnded = true;

        Cursor.visible = true;

        isPaused = true;
        UpdateScoreDisplay();
    }
}
