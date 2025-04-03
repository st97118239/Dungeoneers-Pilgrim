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

    private float pitch = 0f;
    private Player player;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (!player.isPaused)
        {
            Vector3 m_Input = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 moveDirection = cam.TransformDirection(m_Input);
            moveDirection.y = 0;

            if (moveDirection.magnitude > 1)
                moveDirection.Normalize();

            m_Rigidbody.MovePosition(transform.position + m_Speed * Time.fixedDeltaTime * moveDirection);
        }
    }

    void Update()
    {
        if (!player.isPaused)
        {
            rotateDir.y = Input.GetAxisRaw("Mouse X") * sensitivity;
            camRotate.x = -Input.GetAxisRaw("Mouse Y") * sensitivity;

            pitch = Mathf.Clamp(pitch + camRotate.x, -80f, 80f);

            transform.Rotate(rotateDir);

            cam.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}