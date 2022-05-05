using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandler : GameManager
{
    [SerializeField] TextMeshProUGUI highScoreText;
    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        UpdateHighScoreDisplay();
    }
    private void UpdateHighScoreDisplay()
    {
        if (LoadScore() != null)
        {
            Score score = LoadScore();
            highScoreText.text = $"High Score: {score.playerName} - {score.m_waves}";
        }
        else
        {
            highScoreText.text = "High Score: 0";
        }
    }
    public void StartGame()
    {
        LoadMain();
    }
    public void ExitGame()
    {
        Exitgame();
    }
}
