using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed = 5f;

    public Vector3 rotateDir;
    public Transform cam;
    public Vector3 camRotate;
    public float sensitivity = 100f;

    // Store the yaw and pitch separately to avoid issues with Euler angles
    private float pitch = 0f;
    private float yaw = 0f;

    void Start()
    {
        // Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // Store user input as a movement vector (horizontal and vertical)
        Vector3 m_Input = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Adjust the movement direction based on the camera's rotation
        Vector3 moveDirection = cam.TransformDirection(m_Input);
        moveDirection.y = 0; // We don't want to apply any vertical movement

        // Normalize the movement vector to avoid faster diagonal movement
        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();

        // Apply the movement vector to the Rigidbody using MovePosition
        m_Rigidbody.MovePosition(transform.position + m_Speed * Time.fixedDeltaTime * moveDirection);
    }

    public float lookSpeed = 2.0f;
    public float lookXLimit = 80.0f;

    void Update()
    {
        //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        //rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        //cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Handle rotation of the character based on the mouse input
        rotateDir.y = Input.GetAxisRaw("Mouse X") * lookSpeed; // * Time.deltaTime;
        camRotate.x = -Input.GetAxisRaw("Mouse Y") * lookSpeed; // * Time.deltaTime;

        // Apply horizontal rotation to the character (Yaw - left/right)
        yaw += rotateDir.y;

        // Apply vertical rotation to the camera (Pitch - up/down)
        // We limit the camera's vertical rotation to avoid flipping
        pitch = Mathf.Clamp(pitch + camRotate.x, -80f, 80f);

        // Apply the rotation to the character (Yaw - horizontal rotation)
        transform.Rotate(rotateDir);

        // Apply vertical rotation to the camera (Pitch - up/down rotation)
        // Use Quaternion.Euler to directly set pitch and yaw
        cam.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }
}