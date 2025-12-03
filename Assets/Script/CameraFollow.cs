using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerPosition;
    public Transform StartPosition;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(STaticBS.GameStarted == false)
        {
            TrackPosition();
        }
        else
        {
            TrackPlayer();
        }
    }

    public void TrackPlayer()
    {
        Vector3 CamPos = PlayerPosition.position + offset;
        Vector3 SmoothCam = Vector3.Lerp(transform.position, CamPos, 0.007f);
        transform.position = SmoothCam;
    }

    public void TrackPosition()
    {
        transform.position = StartPosition.position;
    }
}
