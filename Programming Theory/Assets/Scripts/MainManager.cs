using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainManager : GameManager
{
    public static int wave = 1;
    public static int waveHealth = 2;
    public static GameObject WorldPortal;
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
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
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

        isPaused = true;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        //Show Restart Button
        pauseScreen.SetActive(true);
        pauseText.SetActive(false);
        gameOverText.SetActive(true);
    }
}
