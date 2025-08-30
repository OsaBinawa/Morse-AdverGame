using UnityEngine;

public class Collectibles : MonoBehaviour
{
    PlatformManager platformManager;
    void Update()
    {
        platformManager = FindAnyObjectByType<PlatformManager>();
        float speed = platformManager.moveSpeed;
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }
    }
}
