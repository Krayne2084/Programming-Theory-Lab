using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static bool isPaused { get; protected set; } = false;
    float seconds;
    string playerName;
    [SerializeField] TMPro.TextMeshProUGUI placeholderName;
    string savePath;
    private void Start()
    {
        savePath = Application.persistentDataPath + "/saveFile.json";
        LoadScore();
    }

    protected virtual void Awake()
    {
        ResetTime();
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
    public void Save()
    {
        Score score = new Score();
        score.m_score = Mathf.FloorToInt(seconds);
        score.playerName = playerName;
        string json =  JsonUtility.ToJson(score);
        File.WriteAllText(savePath, json);
    }
    public void LoadScore()
    {
        if (!File.Exists(savePath))
        {
            return;
        }
        string json = File.ReadAllText(savePath);
        Score score = JsonUtility.FromJson<Score>(json);
        playerName = score.playerName;
        seconds = score.score;
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
    public int m_score;
    public int score 
    { 
        get 
        {
            return m_score;
        } 
        protected set
        {
            if (value < 0)
            {
                return;
            }
            else
            {
                m_score = value;
            }
        } 
    }
}
