using UnityEngine;

public class GameManager : MonoBehaviour
{
    public fish fishdata;
    public int Money;
    public bool fishIsCaught;
    private float shuffleTimer;
    public GameObject[] directions;
    public fish[] fishBackpack;
    public float PullStrength;
    public GameObject FIshNaja;

    static public bool miniGameActive = false;
    static public GameObject direct;
    private float MaxFishPatience;
    static public float fishPatience;
    public float patienceDrainRate = 1f;

    public void TriggerMiniGame(fish fishCaught)
    {
        fishdata = fishCaught;
        MaxFishPatience = fishdata.maxFishPatience;
        fishPatience = MaxFishPatience;
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
        Debug.Log(STaticBS.GameStarted);
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

    void BurningTime()
    {
        fishPatience -= patienceDrainRate * Time.deltaTime;
        if(fishPatience == 0)
        {
            fckyouiamOut();
        }
    }

    void fckyouiamOut()
    {
        
    }
}
