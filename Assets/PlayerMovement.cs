using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the movement speed.
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get input for horizontal and vertical movement.
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the new velocity.
        Vector3 movement = new Vector3(moveX * moveSpeed, 0f, moveZ * moveSpeed);

        // Apply the velocity to the rigidbody.
        rb.velocity = movement;

        // Set the "ismoving" parameter in the animator based on whether the player is moving.
        bool isMoving = movement.magnitude > 0.1f; // You can adjust this threshold as needed.
        animator.SetBool("ismoving", isMoving);
    }
}
