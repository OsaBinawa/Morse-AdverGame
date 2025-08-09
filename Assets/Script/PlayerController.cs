using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float JumpImpulse;
    [SerializeField] private LayerMask ground;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Onjump(InputAction.CallbackContext context)
    {
        if (Grounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpImpulse);
            //anim.SetTrigger("Jump");
        }
    }

    public bool Grounded()
    {
        //Debug.DrawRay(transform.position, Vector2.down, 0.2f, Color.green);
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, ground))
        {
            Debug.Log("Hit");
            return true;
        }
        else
        {
            return false;
        }
    }
}
