using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text DistanceText;
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
    }
}
