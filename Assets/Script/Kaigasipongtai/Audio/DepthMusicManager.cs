using UnityEngine;

public class DepthMusicManager : MonoBehaviour
{
    
     public AudioSource backgroundMusicSource;
    
    
     public AudioClip[] depthMusicTracks;

    
    public PlayerDeep _playerDepthManager; 

    
    public int currentDepthLevel = 0; 

    
     public float[] depthThresholds = new float[4]; 

    void Start()
    {
        backgroundMusicSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        CheckDepthLevel();
    }

    private void CheckDepthLevel()
    {
       if (_playerDepthManager == null) return;

    float currentY = _playerDepthManager.CurrentDepth;
    int nextLevel = currentDepthLevel;
    
   
    if (currentY >= depthThresholds[0]) 
    {
        nextLevel = 0;
    }
    
    else if (currentY >= depthThresholds[1] && currentY < depthThresholds[0]) 
    {
        nextLevel = 1;
    }
    
    else if (currentY >= depthThresholds[2] && currentY < depthThresholds[1])
    {
        nextLevel = 2;
    }
    
    else if (currentY >= depthThresholds[3] && currentY < depthThresholds[2])
    {
        nextLevel = 3;
    }
    
    else if (currentY < depthThresholds[3]) 
    {
        nextLevel = 4;
    }


    
    if (nextLevel != currentDepthLevel)
    {
        currentDepthLevel = nextLevel;
        ChangeTrack(currentDepthLevel);
    }
    }

    private void ChangeTrack(int levelIndex)
    {
        if (depthMusicTracks.Length > levelIndex && backgroundMusicSource != null)
        {
            AudioClip newClip = depthMusicTracks[levelIndex];
            
            if (backgroundMusicSource.clip != newClip)
            {
                backgroundMusicSource.clip = newClip;
                backgroundMusicSource.Play();
                
            }
        }
    }
}