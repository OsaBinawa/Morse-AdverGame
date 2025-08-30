using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;   // Drag InputField
    [SerializeField] private TMP_Dropdown nameDropdown;       // Drag Dropdown
    [SerializeField] private TMP_InputField extraInputField;
    [SerializeField] private string gameplaySceneName = "Gameplay";
    public List<GameObject> panels = new();

    private string selectedName = "Player";
    private string selectedDropdown = "None";
    private string selectedExtra = "None";

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        // Default InputField text
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
            selectedName = nameInputField.text;
        if (extraInputField != null)
            extraInputField.onEndEdit.AddListener(OnExtraInputChanged);
        // Listeners
        if (nameInputField != null)
            nameInputField.onEndEdit.AddListener(OnNameInputChanged);

        if (nameDropdown != null)
            nameDropdown.onValueChanged.AddListener(OnDropdownChanged);

        // Initialize dropdown choice
        if (nameDropdown != null && nameDropdown.options.Count > 0)
            selectedDropdown = nameDropdown.options[nameDropdown.value].text;
    }
    private void OnExtraInputChanged(string newExtra)
    {
        if (!string.IsNullOrEmpty(newExtra))
            selectedExtra = newExtra;

        Debug.Log("➕ Extra input: " + selectedExtra);
    }


    public void ShowPanel(GameObject panelToShow)
    {
        foreach (var panel in panels)
            panel.SetActive(panel == panelToShow);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
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
        string extraField = (!string.IsNullOrEmpty(extraInputField.text)) ? extraInputField.text : selectedExtra;

        // Save into PlayerProfile
        PlayerProfile.Instance.SetPlayerProfile(playerName, selectedDropdown, extraField);
        SceneManager.LoadScene(gameplaySceneName);
        Debug.Log($"🎮 Starting game with Name: {playerName}, Dropdown: {selectedDropdown}, Extra: {extraField}");

    }
}
