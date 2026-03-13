using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM { get; private set; }

    // Data for the CURRENT session
    private string playerName = "New Player";

    // Data for the SAVED highscore
    private string highscoreHolder = "None";
    private int highscore = 0;

    [System.Serializable]
    class SaveData
    {
        public string highscoreHolder;
        public int highscore;
    }

    private void Awake()
    {
        if (GM != null)
        {
            Destroy(gameObject);
            return;
        }

        GM = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highscoreHolder = highscoreHolder;
        data.highscore = highscore;

        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "highscores.json");

        File.WriteAllText(path, json);
        Debug.Log("Highscore Saved: " + highscoreHolder + " with " + highscore);
    }

    public void LoadHighScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "highscores.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscoreHolder = data.highscoreHolder;
            highscore = data.highscore;
        }
    }

    // Set by the Input Field in the Menu
    public void SetName(string name) => playerName = name;
    public string GetName() => playerName;

    // Called by MainManager when a record is broken
    public void SetHighScore(int score)
    {
        highscore = score;
        highscoreHolder = playerName; // The current player now owns the highscore
        SaveHighScore(); // Save immediately when a record is broken
    }

    public int GetHighScore() => highscore;
    public string GetHighscoreHolder() => highscoreHolder;

    // Returns a formatted string like "Highscore: Peter : 58"
    public string GetHighScoreString()
    {
        return $"Highscore: {highscoreHolder} : {highscore}";
    }

    private void OnApplicationQuit()
    {
        SaveHighScore();
    }
}