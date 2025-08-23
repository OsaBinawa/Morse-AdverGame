using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDied;

    Rigidbody2D rb;
    Animator anim;
    [SerializeField] private float JumpImpulse;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject bulletpref;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int Magazine;
    [SerializeField] private int HP;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip melee, ranged, jump;
    //[SerializeField] private bool isHaveBullet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {

    }

    public void Onjump(InputAction.CallbackContext context)
    {
        if (Grounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
            anim.SetTrigger("Jump");
            audioSource.clip = jump;
            audioSource.Play();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (haveBullet())
            {
                anim.SetTrigger("Range");
                audioSource.clip = ranged;
                audioSource.Play();
            }
            else
            {
                anim.SetTrigger("Attack");
                Debug.Log("Melee");
                audioSource.clip = melee;
                audioSource.Play();
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

    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletpref, firePoint.position, firePoint.rotation);
        Magazine--;
    }

    public bool haveBullet()
    {
        if (Magazine > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
