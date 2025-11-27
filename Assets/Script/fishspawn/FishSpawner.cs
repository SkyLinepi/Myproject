using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [Header("Normal Fish Settings")]
    public GameObject fishPrefab;       
    public fish[] FishData;            
    public int spawnAmount = 10;       

    [Header("Special Event Fish")]
    public GameObject specialFishPrefab; 
    public fish SpecialFishData;         

    [Header("Event Timer Settings")]
    public float eventInterval = 20f;   

    [Header("Respawn Settings")]
    public float respawnDelay = 3f;

    private SpriteRenderer areaSprite;
    private Bounds bounds;

    private List<GameObject> spawnedFish = new List<GameObject>();

    void Start()
    {
        areaSprite = GetComponent<SpriteRenderer>();
        bounds = areaSprite.bounds;

       
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnRandomNormalFish();
        }

        
        StartCoroutine(EventTimer());
    }

  

    IEnumerator EventTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(eventInterval);
            SpawnSpecialFish();           
        }
    }


    void SpawnRandomNormalFish()
    {
        fish chosenData = FishData[Random.Range(0, FishData.Length)];
        SpawnFish(fishPrefab, chosenData);
    }

    void SpawnSpecialFish()
    {
        SpawnFish(specialFishPrefab, SpecialFishData);
    }

    void SpawnFish(GameObject prefab, fish fishData)
    {
        Vector3 pos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );

        GameObject fishObj = Instantiate(prefab, pos, Quaternion.identity);

        FishMovement movement = fishObj.GetComponent<FishMovement>();
        movement.FishIdentity = fishData;
        movement.SetArea(areaSprite);

    
        spawnedFish.Add(fishObj);

      
        FishDeathHandler deathHandler = fishObj.AddComponent<FishDeathHandler>();
        deathHandler.onFishDead = () =>
        {
            spawnedFish.Remove(fishObj);
            StartCoroutine(RespawnAfterDelay());
        };
    }



    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnRandomNormalFish();
    }
}


public class FishDeathHandler : MonoBehaviour
{
    public System.Action onFishDead;

    void OnDestroy()
    {
        if (onFishDead != null)
            onFishDead.Invoke();
    }
}
