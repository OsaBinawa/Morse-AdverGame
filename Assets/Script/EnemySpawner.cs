using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public float interval = 5;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            Instantiate(enemy);
            timer -= interval;
        }
    }
}
