using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandler : GameManager
{
    [Header("MenuHandler")]
    [SerializeField] TextMeshProUGUI highScoreText;
    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        UpdateHighScoreDisplay();
        playerName = null;
    }
    private void UpdateHighScoreDisplay()
    {
        Score score = LoadScore();
        print("Show: " + score);
        if (score != null)
        {
            highScoreText.text = $"High Score: {score.playerName} - {score.m_waves}";
        }
        else
        {
            highScoreText.text = "High Score: 0";
        }
    }
    public override void ResetScore()
    {
        base.ResetScore();
        UpdateHighScoreDisplay();
    }
    public override void LoadMain()
    {
        if (playerName == null)
        {
            placeholderName.text = "Please Enter Name...";
            return;
        }
        print("LoadMain()");

        SetName(nameInput);
        base.LoadMain();
    }
    public void StartGame()
    {
        LoadMain();
    }
}
