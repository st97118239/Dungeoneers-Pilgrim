using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 moveDir;
    public float speed;
    public Vector3 rotateDir;
    public Transform cam;
    public Vector3 camRotate;
    public float sensitivity;

    void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        transform.Translate(moveDir * Time.deltaTime * speed);

        rotateDir.y = Input.GetAxis("Mouse X");
        camRotate.x = Input.GetAxis("Mouse Y");
        transform.Rotate(rotateDir * Time.deltaTime * sensitivity);

        cam.Rotate(-camRotate * Time.deltaTime * sensitivity);
    }
}
