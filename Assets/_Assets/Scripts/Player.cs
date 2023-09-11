using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private Vector3 lastInteractDir;

    private Vector3 movDir;

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        movDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (movDir != Vector3.zero)
        {
            lastInteractDir = movDir;
        }

        float interactDistace = 2f; // Typo: Should be interactDistance

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistace))
        {
            Debug.Log(raycastHit.transform);
        }
        else
        {
            Debug.Log("-");
        }
    }

    private void HandleMovement()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards movDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                movDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on the Z
                    movDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                    movDir = Vector3.zero; // Added to stop movement if no directions are available
                }
            }
        }

        if (canMove)
        {
            transform.position += movDir * moveSpeed * Time.deltaTime;
        }

        isWalking = movDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);
    }
}
