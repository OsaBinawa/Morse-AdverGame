using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance { get; private set; }
    public string PlayerName { get; private set; } = "Player";
    public string DropdownChoice { get; private set; } = "None";

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
}
