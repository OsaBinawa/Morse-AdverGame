using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;

    public LeaderboardEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}
public class LeaderboardExporter : MonoBehaviour
{
    public static LeaderboardExporter Instance { get; private set; }

    private string saveFolder;
    private string filePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Create /Saves/ folder inside persistent data path
        saveFolder = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
            Debug.Log("📂 Save folder created at: " + saveFolder);
        }
        else
        {
            Debug.Log("📂 Save folder exists at: " + saveFolder);
        }

        filePath = Path.Combine(saveFolder, "leaderboard.csv");

        // 🔑 Debug where persistentDataPath is located
        Debug.Log("💾 Persistent data path: " + Application.persistentDataPath);
        Debug.Log("📄 Leaderboard file path: " + filePath);
    }


    public void Export(List<LeaderboardEntry> leaderboard)
    {
        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Rank,Name,Score");

        for (int i = 0; i < leaderboard.Count; i++)
        {
            csv.AppendLine($"{i + 1},{leaderboard[i].playerName},{leaderboard[i].score}");
        }

        File.WriteAllText(filePath, csv.ToString());
        Debug.Log("Leaderboard exported to: " + filePath);
    }
}
