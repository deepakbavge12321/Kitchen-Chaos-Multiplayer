using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;
    public float rotationSpeed = 180.0f;

    private void FixedUpdate()
    {
        if (IsMobilePlatform())
        {
            // Mobile Joystick Controls
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                Vector3 moveDir = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
                transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }
        else
        {
            // PC Keypad Controls
            float moveZ = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
            float moveX = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;

            transform.Translate(moveX, 0, moveZ);

            Vector3 moveDir = new Vector3(moveX, 0, moveZ);
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }
    }

    private bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}
