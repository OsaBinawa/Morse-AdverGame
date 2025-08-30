using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.2f;
    private int addCount = 0;
    private Material mat;
    float speed;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        GameManager.OnMilestone += AddSpeed;
    }

    void OnDisable()
    {
        GameManager.OnMilestone -= AddSpeed;
    }

    void Update()
    {
        speed += Time.deltaTime * scrollSpeed;
        mat.SetTextureOffset("_MainTex", Vector2.right * speed);

        if (addCount == 20)
        {
            GameManager.OnMilestone -= AddSpeed;
        }
    }

    void AddSpeed()
    {
        scrollSpeed *= 1.1f;
        addCount += 1;
    }
}