using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDied;

    Rigidbody2D rb;
    [SerializeField] private float JumpImpulse;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject bulletpref;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int Magazine;
    [SerializeField] private int HP;
    //[SerializeField] private bool isHaveBullet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

    }

    public void Onjump(InputAction.CallbackContext context)
    {
        if (Grounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (haveBullet())
            {
                GameObject bullet = Instantiate(bulletpref, firePoint.position, firePoint.rotation);
                Magazine--;
            }
            else
            {
                Debug.Log("Melee");
            }

        }

    }

    public void TakeDamage()
    {
        HP--;
        isDie();
    }

    public bool isDie()
    {
        if (HP <= 0)
        {
            Debug.Log("Player Dead");
            OnPlayerDied?.Invoke();
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool Grounded()
    {
        
        if (Physics2D.Raycast(transform.position, Vector2.down, 2f, ground))
        {
            Debug.Log("Hit");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool haveBullet()
    {
        if (Magazine != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
