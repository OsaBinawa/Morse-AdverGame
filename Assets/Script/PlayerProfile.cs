using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance { get; private set; }
    public string PlayerName { get; private set; } = "Player";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            PlayerName = name;
        else
            PlayerName = "Player";

        Debug.Log("👤 Player name set to: " + PlayerName);
    }
}
