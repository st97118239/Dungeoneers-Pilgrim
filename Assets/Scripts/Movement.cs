using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed = 5f;

    public Vector3 rotateDir;
    public Transform cam;
    public Vector3 camRotate;
    public float sensitivity = 100f;
    public float lookXLimit = 80.0f;

    // Store the yaw and pitch separately to avoid issues with Euler angles
    private float pitch = 0f;
    private Player player;

    void Start()
    {
        // Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (!player.isDead)
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
    }

    void Update()
    {
        if (!player.isDead)
        {
            // Handle rotation of the character based on the mouse input
            rotateDir.y = Input.GetAxisRaw("Mouse X") * sensitivity; // * Time.deltaTime;
            camRotate.x = -Input.GetAxisRaw("Mouse Y") * sensitivity; // * Time.deltaTime;

            // Apply vertical rotation to the camera (Pitch - up/down)
            // We limit the camera's vertical rotation to avoid flipping
            pitch = Mathf.Clamp(pitch + camRotate.x, -80f, 80f);

            // Apply the rotation to the character (Yaw - horizontal rotation)
            transform.Rotate(rotateDir);

            // Apply vertical rotation to the camera (Pitch - up/down rotation)
            // Use Quaternion.Euler to directly set pitch and yaw
            cam.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}