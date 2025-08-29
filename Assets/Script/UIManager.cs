using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text DistanceText;
    [SerializeField] private TMP_Text ResDistanceText;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private bool IsPause;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause)
                OnResume();
            else
                OnPause();
        }

    }
    public void GoToMainMenu(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnPause()
    {
        PauseCanvas.SetActive(true);
        IsPause = true;
        Time.timeScale = 0.0f;
    }
    public void OnResume()
    {
        PauseCanvas.SetActive(false);
        IsPause = false;
        Time.timeScale = 1.0f;
    }

    public void Onretry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
