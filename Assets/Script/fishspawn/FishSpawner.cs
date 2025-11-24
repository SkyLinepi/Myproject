using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;   
    public int spawnAmount = 10;       

    private SpriteRenderer areaSprite;
    private Bounds bounds;

    void Start()
    {
        areaSprite = GetComponent<SpriteRenderer>();
        bounds = areaSprite.bounds;

        for (int i = 0; i < spawnAmount; i++)
        {
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

       
        GameObject prefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

        
        GameObject fishObj = Instantiate(prefab, pos, Quaternion.identity);

       
        FishMovement movement = fishObj.GetComponent<FishMovement>();
        if (movement != null)
        {
            movement.SetArea(areaSprite);
        }
    }
}
