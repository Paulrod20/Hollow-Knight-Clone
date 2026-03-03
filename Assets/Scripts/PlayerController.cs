using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{   
    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 1f;
    private float xAxis;
    private Rigidbody2D rb;

    [Header("Ground Check Settings")]
    [SerializeField] private float jumpForce = 45f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Keyboard.current.dKey.isPressed ? 1f :
                Keyboard.current.aKey.isPressed ? -1f : 0f;
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(walkSpeed * xAxis, rb.linearVelocity.y);
    }

    private void Jump()
    {
        // Jump cancel — releasing space while moving up cuts the jump short
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }

        if(Keyboard.current.spaceKey.wasPressedThisFrame && Grounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool Grounded()
    {
        if(Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) 
        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
        || Physics2D.Raycast(groundCheckPoint.position - new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))

        {
            return true;
        }
        return false;
    }
}
