using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 1;
    [SerializeField] private ParticleSystem dieEffect;
    public static event Action OnEnemyDied;
    HealthSystem healthSystem;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = new HealthSystem(health);
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = -1 * speed;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }
            healthSystem.TakeDamage(1);
            var nyawa = healthSystem.GetHealth();

            if (nyawa <= 0)
            {
                Die();
            }
        }
        if (collision.CompareTag("Bullet"))
        {
            Die();
        }
        if (collision.CompareTag("Border"))
        {
            Destroy(this.gameObject); 
        }
    }

    public void Die()
    {
        dieEffect.transform.parent = null;
        dieEffect.Play();

        OnEnemyDied?.Invoke();
        
        Destroy(dieEffect.gameObject, dieEffect.main.duration + dieEffect.main.startLifetime.constantMax);
        Destroy(this.gameObject); 
    }
}