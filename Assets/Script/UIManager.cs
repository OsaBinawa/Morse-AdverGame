using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text DistanceText;
    [SerializeField] private TMP_Text ResDistanceText;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        DistanceText.text = $"{gameManager.score} M";
        ResDistanceText.text = $"{gameManager.score} M";
    }
    public void GoToMainMenu(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Onretry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
