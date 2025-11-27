using UnityEngine;

public class GameManager : MonoBehaviour
{
    public fish fishdata;
    public bool fishIsCaught;
    private float shuffleTimer;
    public GameObject[] directions;
    public float PullStrength;
    public GameObject FIshNaja;

    private bool miniGameActive = false;
    static public GameObject direct;

    public void TriggerMiniGame(fish fishCaught)
    {
        fishdata = fishCaught;
        FIshNaja.SetActive(true);
        SpriteRenderer sr = FIshNaja.GetComponent<SpriteRenderer>();
        sr.sprite = fishdata.fishPic;
        fishIsCaught = false;
        miniGameActive = true;

        SetShuffleTime();
        FishPullDirection();
    }

    void Update()
    {
        if (!miniGameActive || fishIsCaught) return;

        shuffleTimer -= Time.deltaTime;

        if (shuffleTimer <= 0f)
        {
            FishPullDirection();
            SetShuffleTime();
        }
    }

    void FishPullDirection()
    {
        GameObject chosenDirection = directions[Random.Range(0, directions.Length)];
        Debug.Log("Direction: " + chosenDirection.name);
    }

    void SetShuffleTime()
    {
        shuffleTimer = Random.Range(fishdata.ShortestShuffle, fishdata.LongestShuffle);
    }
}
