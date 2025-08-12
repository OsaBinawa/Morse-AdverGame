using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    PlatformManager platformManager;
    [SerializeField] private int score;
    [SerializeField] private int milestone;
    [SerializeField] private float speedToAdd;
    [SerializeField] private float pointsPerSecond = 10f;
    private float scoreTimer;
    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        platformManager = FindFirstObjectByType<PlatformManager>();
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
        PlayerController.OnPlayerDied += gameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDied -= gameOver;
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
        platformManager.moveSpeed += speedToAdd;
    }

    public void gameOver()
    {
        Time.timeScale = 0.0f;
        Debug.Log("Game Over");
    }
}
