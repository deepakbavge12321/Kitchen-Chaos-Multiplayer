using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _mobileMoveSpeed;
    [SerializeField] private float _pcMoveSpeed = 10f; // Change PC move speed here
    public float rotationSpeed = 180.0f;

    private void FixedUpdate()
    {
        float moveSpeed = IsMobilePlatform() ? _mobileMoveSpeed : _pcMoveSpeed;

        if (IsMobilePlatform())
        {
            // Mobile Joystick Controls
            float horizontalInput = _joystick.Horizontal;
            float verticalInput = _joystick.Vertical;

            Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (moveDir != Vector3.zero)
            {
                // Calculate the target rotation based on the input vector
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);

                // Smoothly rotate the player towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            _rigidbody.velocity = moveDir * moveSpeed;

            _animator.SetBool("isWalking", moveDir != Vector3.zero);
        }
        else
        {
            // PC Keypad Controls
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;

            if (moveDir != Vector3.zero)
            {
                // Calculate the target rotation based on the input vector
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);

                // Smoothly rotate the player towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            _rigidbody.velocity = moveDir * moveSpeed;

            _animator.SetBool("isWalking", moveDir != Vector3.zero);
        }
    }

    private bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}
