using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool hasGameEnded { get; protected set; } = false;
    public static string playerName { get; protected set; }
    [Header("GameManager")]
    public TextMeshProUGUI placeholderName;
    public TextMeshProUGUI nameInput;
    string savePath;
    string savePref = "PortalPusherSave";

    protected virtual void Awake()
    {
        savePath = Application.persistentDataPath + "/Saves/saveFile.json";
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

    public virtual void LoadMain()
    {
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
    public virtual void ResetScore()
    {
        /*if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }*/
        if(PlayerPrefs.GetString(savePref) != null)
        {
            PlayerPrefs.DeleteKey(savePref);
        }
    }
    public void SetName(TextMeshProUGUI text)
    {
        playerName = text.text;
    }
    public void SaveScore()
    {
        Score score = new Score();
        int currentScore = MainManager.wave - 1;
        Score scoreSave = LoadScore();
        int highScore;
        string json;

        print("Save1: " + scoreSave);

        highScore = GetHighScore(scoreSave);
        
        if (currentScore > highScore)
        {
            score.m_waves = currentScore;
            score.playerName = playerName;

            print($"New High Score: {score.playerName} - {score.m_waves}");

            json = JsonUtility.ToJson(score);
            //File.WriteAllText(savePath, json);

            print("Save2: " + scoreSave);
            PlayerPrefs.SetString(savePref, json);
            PlayerPrefs.Save();
        }
        
    }
    int GetHighScore(Score scoreSave)
    {
        int highScore;
        if (scoreSave != null)
        {
            highScore = scoreSave.m_waves;
            print("High Score: " + highScore);
        }
        else
        {
            print("No High Score");
            highScore = 0;
        }
        return highScore;
    }
    public Score LoadScore()
    {
        /*if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Score score = JsonUtility.FromJson<Score>(json);
            print($"Load: {score}, {json}");
            return score;
        }*/
        if (PlayerPrefs.GetString(savePref) != null)
        {
            string json = PlayerPrefs.GetString(savePref);
            Score score = JsonUtility.FromJson<Score>(json);
            print($"Load: {score}, {json}");
            return score;
        }
        return null;
    }
    public void ExitGame()
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
