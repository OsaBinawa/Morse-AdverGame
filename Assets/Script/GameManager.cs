using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDied += gameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDied -= gameOver;
    }

    public void gameOver()
    {
        Time.timeScale = 0.0f;
        Debug.Log("Game Over");
    }
}
