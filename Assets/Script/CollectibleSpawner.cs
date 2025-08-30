using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public Transform Spawner;
    [SerializeField] private GameObject[] Collectibles;
    public float interval = 5;
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            int randIndex = Random.Range(0, Collectibles.Length);
            GameObject chosenItem = Collectibles[randIndex];
            Instantiate(chosenItem, Spawner.position, Quaternion.identity);
            timer -= interval;
        }
    }
}
