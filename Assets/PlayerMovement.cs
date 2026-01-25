using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * speed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();

        Debug.Log("Move X: " + moveInput.x);
        animator.SetFloat("InputX", moveInput.x);
        Debug.Log("Move Y: " + moveInput.y);
        animator.SetFloat("InputY", moveInput.y);
    }
}
