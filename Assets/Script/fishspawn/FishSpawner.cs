using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefabs;
    public fish[] FishData;
    public int spawnAmount = 10;       
    private fish ChosenData;

    private SpriteRenderer areaSprite;
    private Bounds bounds;

    void Start()
    {
        areaSprite = GetComponent<SpriteRenderer>();
        bounds = areaSprite.bounds;

        for (int i = 0; i < spawnAmount; i++)
        {
            RandomFishData();
            SpawnRandomFish();
        }
    }

    void SpawnRandomFish()
    {
     
        Vector3 pos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
 
        GameObject fishObj = Instantiate(fishPrefabs, pos, Quaternion.identity);
       
        FishMovement movement = fishObj.GetComponent<FishMovement>();
        if (movement != null)
        {
            movement.FishIdentity = ChosenData;
            movement.SetArea(areaSprite);
        }
    }

    public void RandomFishData()
    {
        fish ChoseFishData = FishData[Random.Range(0, FishData.Length)];
        ChosenData = ChoseFishData;
    }
}
