using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;   // Drag InputField
    [SerializeField] private TMP_Dropdown nameDropdown;       // Drag Dropdown
    [SerializeField] private string gameplaySceneName = "Gameplay";

    private string selectedName = "Player";
    private string selectedDropdown = "None";

    private void Start()
    {
        // Default InputField text
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
            selectedName = nameInputField.text;

        // Listeners
        if (nameInputField != null)
            nameInputField.onEndEdit.AddListener(OnNameInputChanged);

        if (nameDropdown != null)
            nameDropdown.onValueChanged.AddListener(OnDropdownChanged);

        // Initialize dropdown choice
        if (nameDropdown != null && nameDropdown.options.Count > 0)
            selectedDropdown = nameDropdown.options[nameDropdown.value].text;
    }

    private void OnNameInputChanged(string newName)
    {
        if (!string.IsNullOrEmpty(newName))
            selectedName = newName;
        Debug.Log("✍️ InputField name: " + selectedName);
    }

    private void OnDropdownChanged(int index)
    {
        if (nameDropdown != null && index >= 0 && index < nameDropdown.options.Count)
        {
            selectedDropdown = nameDropdown.options[index].text;
            Debug.Log("📜 Dropdown choice: " + selectedDropdown);
        }
    }

    public void OnStartGame()
    {
        string playerName = (!string.IsNullOrEmpty(nameInputField.text)) ? nameInputField.text : selectedName;

        // Save into PlayerProfile (so GameManager can grab it later)
        PlayerProfile.Instance.SetPlayerName(playerName, selectedDropdown);

        Debug.Log($"🎮 Starting game with Name: {playerName}, Dropdown: {selectedDropdown}");

        SceneManager.LoadScene(gameplaySceneName);
    }
}
