using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public fish[] Data;
    public int spawnAmount = 1;

    private Bounds bounds;

    void Start()
    {
        
        bounds = GetComponent<SpriteRenderer>().bounds;

        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnFishInsideBounds();
        }
    }

    void SpawnFishInsideBounds()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 pos = new Vector3(x, y, 0);
        Instantiate(fishPrefab, pos, Quaternion.identity);
    }
}
