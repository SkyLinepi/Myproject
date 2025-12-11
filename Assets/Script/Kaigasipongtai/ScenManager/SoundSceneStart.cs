using UnityEngine;

public class SoundSceneStart : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
