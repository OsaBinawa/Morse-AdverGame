using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance { get; private set; }
    public string PlayerName { get; private set; } = "Player";
    public string DropdownChoice { get; private set; } = "None";
    public string Username { get; private set; } = "Unknown";

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

    public void SetPlayerName(string name, string dropdown = "None")
    {
        PlayerName = !string.IsNullOrEmpty(name) ? name : "Player";
        DropdownChoice = dropdown;
        Debug.Log($"👤 Player set: {PlayerName}, Dropdown: {DropdownChoice}");
    }
    public void SetPlayerProfile(string name, string dropdown = "None", string username = "Unknown")
    {
        PlayerName = !string.IsNullOrEmpty(name) ? name : "Player";
        DropdownChoice = dropdown;
        Username = !string.IsNullOrEmpty(username) ? username : "Unknown";
        Debug.Log($"👤 Player set: {PlayerName}, Dropdown: {DropdownChoice}, Username: {Username}");
    }
}
