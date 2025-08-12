using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    [SerializeField] private float lifeTime;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifeTime);
    }

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        rb.linearVelocity = transform.right * travelSpeed * Time.deltaTime;
    }
}
