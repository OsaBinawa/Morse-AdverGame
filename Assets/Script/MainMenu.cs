using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private string gameplaySceneName = "Gameplay"; // set your scene name

    public void OnStartGame()
    {
        string playerName = "Player";

        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
            playerName = nameInputField.text;

        PlayerProfile.Instance.SetPlayerName(playerName);

        Debug.Log("🎮 Starting game with player name: " + playerName);

        SceneManager.LoadScene(gameplaySceneName);
    }
}
