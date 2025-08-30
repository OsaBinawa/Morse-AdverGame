using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] enemySpawner;
    [Range(1f, 2.5f)] public float interval = 2.5f;
    float timer;

    void OnEnable()
    {
        GameManager.OnMilestone += decreaseInterval;
    }

    void OnDisable()
    {
        GameManager.OnMilestone -= decreaseInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            int randIndex = Random.Range(0, enemies.Length);
            GameObject chosenEnemy = enemies[randIndex];
            int randIndexES = Random.Range(0, enemySpawner.Length);
            Transform chosenSpawner = enemySpawner[randIndexES];
            var spawnTrans = chosenSpawner.transform.position;
            Instantiate(chosenEnemy, spawnTrans, Quaternion.identity);
            timer -= interval;
        }
    }

    void decreaseInterval()
    {
        interval -= 0.2f;
    }
}
