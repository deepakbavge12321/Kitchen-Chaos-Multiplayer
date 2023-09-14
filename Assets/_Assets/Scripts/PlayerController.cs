using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 180.0f;

    void Update()
    {
        // Movement
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.Translate(0, 0, moveZ);
        transform.Translate(moveX, 0, 0);

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);

        transform.forward = Vector3.Slerp(transform.forward, moveDir,Time.deltaTime*rotationSpeed);
    }
}
