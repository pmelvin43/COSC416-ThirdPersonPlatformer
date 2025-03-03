using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float playerSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public int maxJumps = 2; // Allow double jump

    private Vector3 velocity;
    private float turnSmoothVelocity;
    private int jumpCount; // Track number of jumps

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        jumpCount = maxJumps; // Initialize jumps
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 moveDir = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        // Reset jumps when grounded
        if (controller.isGrounded)
        {
            velocity.y = -2f; // Small negative value to keep character grounded
            jumpCount = maxJumps; // Reset jumps when landing
        }

        // Jumping and Double Jump
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount--; // Reduce jump count
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity acceleration

        // Move the character
        Vector3 finalMovement = moveDir.normalized * playerSpeed + velocity;
        controller.Move(finalMovement * Time.deltaTime);
    }
}
