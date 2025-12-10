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

    public AudioClip _Audio;

    static public bool miniGameActive = false;
    static public GameObject direct;
    private float MaxFishPatience;
    public float fishPatience;
    public float patienceDrainRate;
    public GameObject currentPlayerDirection;
    public Transform pullTarget;
    public Player player;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

    public GameObject ParentDirecn;
    public void TriggerMiniGame(fish fishCaught)
    {
        ParentDirecn.SetActive(true);
        player.enabled = false;
        fishdata = fishCaught;
        MaxFishPatience = fishdata.maxFishPatience;
        fishPatience = MaxFishPatience;
        FIshNaja.SetActive(true);
        SpriteRenderer sr = FIshNaja.GetComponent<SpriteRenderer>();
        sr.sprite = fishdata.fishPic;
        fishIsCaught = false;
        miniGameActive = true;
        patienceDrainRate = fishdata.fishPatienceDrainRates;


        SetShuffleTime();
        FishPullDirection();
    }

    void Update()
    {
        MoveFish();
        directionToPlayer();
        UpdatePlayerDirection();
        if (!miniGameActive || fishIsCaught) return;
        PlayerPulling(currentPlayerDirection);
        shuffleTimer -= Time.deltaTime;

        if (shuffleTimer <= 0f)
        {
            FishPullDirection();
            SetShuffleTime();
        }
    }

    void FishPullDirection()
    {
        // Pick new direction
        direct = directions[Random.Range(0, directions.Length)];

        // Change color of all direction objects
        for (int i = 0; i < directions.Length; i++)
        {
            if (directions[i] == direct)
                directions[i].SetActive(true);     // เปิดทิศที่ถูกต้อง
            else
                directions[i].SetActive(false);    // ปิดทิศอื่นทั้งหมด
        }
        Debug.Log("Direction: " + direct.name);
    }
    public void PlayerPulling(GameObject playerInputDirection)
    {
        if (playerInputDirection == direct && Input.GetMouseButtonDown(0))
        {
            fishPatience += PullStrength; // recover patience while pulling
            fishPatience = Mathf.Clamp(fishPatience, 0, MaxFishPatience * 2f);
            if (fishPatience >= MaxFishPatience * 2)
                CatchFish();
        }
        else
        {
            fishPatience -= patienceDrainRate * Time.deltaTime;
            if (fishPatience <= 0)
                fckyouiamOut();
        }
    }


    void SetShuffleTime()
    {
        shuffleTimer = Random.Range(fishdata.ShortestShuffle, fishdata.LongestShuffle);
    }

    void BurningTime()
    {
        fishPatience -= patienceDrainRate * Time.deltaTime;
        if (fishPatience == 0)
        {
            fckyouiamOut();
        }
    }

    void fckyouiamOut()
    {
        miniGameActive = false;
        fishIsCaught = false;
        fishPatience = 0f;
        FIshNaja.SetActive(false);
        direct = null;
        player.enabled = true;
        Debug.Log("its escape");
        ParentDirecn.SetActive(false);
    }

    public void UpdatePlayerDirection()
    {
        bool up = Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        int keyCount = 0;
        if (up) keyCount++;
        if (down) keyCount++;
        if (left) keyCount++;
        if (right) keyCount++;

        // ❌ More than 2 keys pressed → INVALID
        // (2 keys allowed for diagonals only)
        if (keyCount > 2)
        {
            currentPlayerDirection = null;
            return;
        }

        // ❌ Opposites pressed → INVALID
        if (up && down || left && right)
        {
            currentPlayerDirection = null;
            return;
        }

        // ➕ 8-direction mapping
        if (up && right) { currentPlayerDirection = directions[0]; return; }
        if (up && left) { currentPlayerDirection = directions[1]; return; }
        if (down && right) { currentPlayerDirection = directions[2]; return; }
        if (down && left) { currentPlayerDirection = directions[3]; return; }

        if (up) { currentPlayerDirection = directions[4]; return; }
        if (down) { currentPlayerDirection = directions[5]; return; }
        if (left) { currentPlayerDirection = directions[6]; return; }
        if (right) { currentPlayerDirection = directions[7]; return; }

        // No input
        currentPlayerDirection = null;
    }

    void MoveFish()
    {
        Vector3 fishPos = FIshNaja.transform.position;
        Vector3 playerPos = pullTarget.position;

        float dist = Vector3.Distance(fishPos, playerPos);

        // Normalize patience → 0 to 1
        float t = Mathf.InverseLerp(0, MaxFishPatience * 2f, fishPatience);

        // Distance based on patience
        float targetDistance = Mathf.Lerp(MaxFishPatience, 0f, t);

        // Direction from fish → player
        Vector3 dir = (playerPos - fishPos).normalized;

        // Target final position
        Vector3 targetPos = playerPos - dir * targetDistance;

        // Move the fish smoothly
        FIshNaja.transform.position =
            Vector3.MoveTowards(fishPos, targetPos, Time.deltaTime * 5f);
    }


    void CatchFish()
    {
        Debug.Log("We got him!");

        // Stop minigame
        miniGameActive = false;
        fishIsCaught = true;

        // Add fish to backpack
        AddFishToBackpack(fishdata);

        // Hide fish sprite
        FIshNaja.SetActive(false);

        // Reset patience & direction
        fishPatience = 0f;
        direct = null;

        // Re-enable player movement
        player.enabled = true;

        // Optional: Give money reward
        // Money += fishdata.price;v
        ParentDirecn.SetActive(false);
    }
    void AddFishToBackpack(fish caughtFish)
    {
        for (int i = 0; i < fishBackpack.Length; i++)
        {
            if (fishBackpack[i] == null)
            {
                fishBackpack[i] = caughtFish;
                Debug.Log("Fish added to backpack: ");
                return; // stop after inserting
            }
        }

        Debug.Log("Backpack FULL. Could not add fish.");
    }

    void directionToPlayer()
    {
        ParentDirecn.transform.position = FIshNaja.transform.position;
    }


}
