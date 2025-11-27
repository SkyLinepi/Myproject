using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    public ParticleSystem ripple;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            ripple.transform.position = other.transform.position;
            ripple.Play();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
