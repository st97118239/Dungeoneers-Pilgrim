using System;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private Player player;

    public Action<Stone> DestroyFunc = null;

    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            if (player.heartsActive.Count > 0)
            {
                player.RemoveHeart();
            }
        }
        else if (!collision.gameObject.CompareTag("Golem"))
        {
            DestroyFunc?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void PauseStone()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        savedVelocity = rb.velocity;
        savedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;
    }

    public void ResumeStone()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
    }
}
