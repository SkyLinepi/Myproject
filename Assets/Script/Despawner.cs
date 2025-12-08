using UnityEngine;

public class Despawner : MonoBehaviour
{
    public GameManager GM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D cd)
    {
        if (cd.CompareTag("fish"))
        {
            FishMovement fm = cd.GetComponent<FishMovement>();
            fish fishdata = fm.FishIdentity;
            GM.TriggerMiniGame(fishdata);
            GM.FIshNaja.transform.position = transform.position;
        }
    }
}
