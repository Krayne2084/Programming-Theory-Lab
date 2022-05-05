using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static bool isPaused { get; protected set; } = false;
    public static bool hasGameEnded { get; protected set; } = false;
    public static string playerName { get; protected set; }
    [SerializeField] TextMeshProUGUI placeholderName;
    [SerializeField] TextMeshProUGUI nameInput;
    string savePath;
    private void Start()
    {
        savePath = Application.persistentDataPath + "/saveFile.json";
    }

    protected virtual void Awake()
    {
        ResetTime();
        hasGameEnded = false;
        isPaused = false;
        /*if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    public void LoadMain()
    {
        if (playerName == null)
        {
            placeholderName.text = "Please Enter Name...";
            return;
        }
        SetName(nameInput);
        SceneManager.LoadScene(1);
    }
    public void LoadMenu()
    {
        ResetTime();
        SceneManager.LoadScene(0);
    }
    
    public void ResetTime()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
    public void ResetScore()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
    public void SetName(TMPro.TextMeshProUGUI text)
    {
        playerName = text.text;
    }
    public void SaveScore()
    {
        Score score = new Score();
        int currentScore = MainManager.wave - 1;
        int highScore;
        if (LoadScore() != null)
        {
            highScore = LoadScore().m_waves;
        }
        else
        {
            highScore = 0;
        }
        if (currentScore > highScore)
        {
            score.m_waves = currentScore;
            score.playerName = playerName;

            string json = JsonUtility.ToJson(score);
            File.WriteAllText(savePath, json);
        }
    }
    public Score LoadScore()
    {
        if (!File.Exists(savePath))
        {
            return null;
        }
        string json = File.ReadAllText(savePath);
        Score score = JsonUtility.FromJson<Score>(json);
        return score;
    }
    public void Exitgame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
public class Score
{
    public string playerName;
    public int m_waves;
    public int waves 
    { 
        get 
        {
            return m_waves;
        } 
        protected set
        {
            if (value < 0)
            {
                return;
            }
            else
            {
                m_waves = value;
            }
        } 
    }
}
