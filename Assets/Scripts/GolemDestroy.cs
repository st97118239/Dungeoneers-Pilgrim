using System.Collections;
using UnityEngine;

public class GolemDestroy : MonoBehaviour
{
    private ParticleSystem particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void StartCheck()
    {
        StartCoroutine(IsParticleDone());
    }

    private IEnumerator IsParticleDone()
    {
        while (particles.isPlaying) // return nothing until the particle system stops playing
        {
            yield return null;
        }

        Debug.Log("Golem explosion finished");
        GetComponentInParent<Enemy>().Destroy();
    }
}
