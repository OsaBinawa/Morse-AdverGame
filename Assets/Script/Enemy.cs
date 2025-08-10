using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocityX = -1 * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health -= 1;
            
            if (health <= 0)
            {
                Die();
            }
        }

        if (collision.CompareTag("Border"))
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}