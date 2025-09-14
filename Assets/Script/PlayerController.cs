using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDied, OnPlayerDamaged;

    Rigidbody2D rb;
    Animator anim;
    [SerializeField] private float JumpImpulse;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject bulletpref;
    [SerializeField] private Transform firePoint;
    [Range(0,20)][SerializeField] private int Magazine;
    [SerializeField] private int HP;
    [SerializeField] private int curHP;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip melee, ranged, jump;
    [SerializeField] private Image[] hearts;
    [SerializeField] private TMP_Text MagazineUI;
    [SerializeField] private BoxCollider2D meeleRange;
    [SerializeField] private bool isGrounded;
    private SpriteRenderer sr;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    //[SerializeField] private bool isHaveBullet;

    void Start()
    {
        curHP = HP;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        UpdateHearts();
        UpdateUIBullet();
    }


    void Update()
    {
        isGrounded = Grounded();
        anim.SetBool("Run", isGrounded);
        anim.SetBool("Fall",!isGrounded);
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
        curHP--;
        curHP = Mathf.Clamp(curHP, 0, HP);
        isDie();
        UpdateHearts();
        StartCoroutine(FlashRed());
        OnPlayerDamaged?.Invoke();
    }

    private IEnumerator FlashRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
    }

    public bool isDie()
    {
        if (curHP <= 0)
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
            //Debug.Log("Hit");
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
        // Magazine = Mathf.Clamp(Magazine, 0, 20);
        UpdateUIBullet();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < curHP)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void OnMeeleDamage()
    {

    }
    void UpdateUIBullet()
    {
        Magazine = Mathf.Clamp(Magazine, 0, 20);
        MagazineUI.text = Magazine.ToString();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            TakeDamage();
        }

        if (collision.CompareTag("Border"))
        {
            curHP -= HP;
            isDie();
        }
        if (collision.CompareTag("Heal"))
        {
            curHP++;
            curHP = Mathf.Clamp(curHP, 0, HP);
            UpdateHearts();
        }
        if (collision.CompareTag("Ammo"))
        {
            Magazine += 20;
            // Magazine = Mathf.Clamp(Magazine, 0, 20);
            UpdateUIBullet();
        }
    }
}
