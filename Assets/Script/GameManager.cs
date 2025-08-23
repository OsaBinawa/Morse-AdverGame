using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    PlatformManager platformManager;

    public int score;
    [SerializeField] private int milestone;
    [SerializeField] private float speedToAdd = 1.05f;
    [SerializeField] private float pointsPerSecond = 10f;
    private float scoreTimer;

    [Header("BG Settings")]
    [SerializeField] private GameObject background;
    [SerializeField] private float lerpSpeed = 2f;
    [SerializeField] private float currentMultiplier = 1f;
    [SerializeField] private float targetMultiplier = 1.2f;
    private float[] baseSpeeds = { 0.05f, 0.1f, 0.3f };
    private Renderer bgRenderer;
    private Material bgMaterial;

    // Leaderboard
    private const int MaxLeaderboardEntries = 5;
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        platformManager = FindFirstObjectByType<PlatformManager>();
        background.SetActive(true);
        bgRenderer = background.GetComponent<Renderer>();
        bgMaterial = bgRenderer.material;   

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

        Mathf.Lerp(currentMultiplier, targetMultiplier, Time.deltaTime * lerpSpeed);

        bgMaterial.SetFloat("_BG2_speed", baseSpeeds[0] * currentMultiplier);
        bgMaterial.SetFloat("_BG3_speed", baseSpeeds[1] * currentMultiplier);
        bgMaterial.SetFloat("_BG4_speed", baseSpeeds[2] * currentMultiplier);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        Debug.Log("Game Over");

        string playerName = PlayerProfile.Instance != null ? PlayerProfile.Instance.PlayerName : "Unknown";
        string dropdownChoice = PlayerProfile.Instance != null ? PlayerProfile.Instance.DropdownChoice : "None";

        UpdateLeaderboard(playerName, dropdownChoice, score);
        FindFirstObjectByType<OnlineLeaderboard>().UploadScore(playerName, dropdownChoice, score);

        LeaderboardExporter.Instance.Export(leaderboard);
    }

    // ---------------- Leaderboard Methods ----------------
    private void UpdateLeaderboard(string playerName, string dropdownChoice, int newScore)
    {
        leaderboard.Add(new LeaderboardEntry(playerName, dropdownChoice, newScore));
        leaderboard = leaderboard
            .OrderByDescending(entry => entry.score)
            .Take(MaxLeaderboardEntries)
            .ToList();

        SaveLeaderboard();

        Debug.Log("Leaderboard:");
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Debug.Log($"{i + 1}. {leaderboard[i].playerName} - {leaderboard[i].dropdownChoice} - {leaderboard[i].score}");
        }
    }

    private void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString("HighScoreName" + i, leaderboard[i].playerName);
            PlayerPrefs.SetString("HighScoreDropdown" + i, leaderboard[i].dropdownChoice); // NEW: save dropdown
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
                string dropdown = PlayerPrefs.GetString("HighScoreDropdown" + i, "None"); // NEW: load dropdown (defaults if missing)
                int savedScore = PlayerPrefs.GetInt("HighScoreValue" + i, 0);

                leaderboard.Add(new LeaderboardEntry(name, dropdown, savedScore)); // ✅ pass all 3 args
            }
        }
    }

    public List<LeaderboardEntry> GetLeaderboard()
    {
        return new List<LeaderboardEntry>(leaderboard);
    }
}