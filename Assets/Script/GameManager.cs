using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    PlatformManager platformManager;

    [SerializeField] private int score;
    [SerializeField] private int milestone;
    [SerializeField] private float speedToAdd = 1.05f;
    [SerializeField] private float pointsPerSecond = 10f;
    [SerializeField] private GameObject background;

    private float scoreTimer;

    // Leaderboard
    private const int MaxLeaderboardEntries = 5;
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        platformManager = FindFirstObjectByType<PlatformManager>();
        background.SetActive(true);

        LoadLeaderboard();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        scoreTimer += pointsPerSecond * Time.deltaTime;
        if (scoreTimer >= 1f)
        {
            int pointsToAdd = Mathf.FloorToInt(scoreTimer);
            scoreTimer -= pointsToAdd;
            AddScore(pointsToAdd);
        }
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDied += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDied -= GameOver;
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score % 100 == 0)
        {
            milestone = score;
            OnScoreMilestone(score);
        }
    }

    private void OnScoreMilestone(int milestone)
    {
        Debug.Log($"Reached {milestone} points!");
        platformManager.moveSpeed *= speedToAdd;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        Debug.Log("Game Over");

        string playerName = PlayerProfile.Instance != null ? PlayerProfile.Instance.PlayerName : "Unknown";
        UpdateLeaderboard(playerName, score);

        LeaderboardExporter.Instance.Export(leaderboard);
    }

    // ---------------- Leaderboard Methods ----------------
    private void UpdateLeaderboard(string playerName, int newScore)
    {
        leaderboard.Add(new LeaderboardEntry(playerName, newScore));
        leaderboard = leaderboard
            .OrderByDescending(entry => entry.score)
            .Take(MaxLeaderboardEntries)
            .ToList();

        SaveLeaderboard();

        Debug.Log("Leaderboard:");
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Debug.Log($"{i + 1}. {leaderboard[i].playerName} - {leaderboard[i].score}");
        }
    }

    private void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString("HighScoreName" + i, leaderboard[i].playerName);
            PlayerPrefs.SetInt("HighScoreValue" + i, leaderboard[i].score);
        }
        PlayerPrefs.Save();
    }

    private void LoadLeaderboard()
    {
        leaderboard.Clear();
        for (int i = 0; i < MaxLeaderboardEntries; i++)
        {
            if (PlayerPrefs.HasKey("HighScoreValue" + i))
            {
                string name = PlayerPrefs.GetString("HighScoreName" + i, "Player");
                int score = PlayerPrefs.GetInt("HighScoreValue" + i, 0);
                leaderboard.Add(new LeaderboardEntry(name, score));
            }
        }
    }

    public List<LeaderboardEntry> GetLeaderboard()
    {
        return new List<LeaderboardEntry>(leaderboard);
    }
}
