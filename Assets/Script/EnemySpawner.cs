using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] enemySpawner;
    [Range(2.5f, 5)] public float interval = 5;
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
        interval -= 0.125f;
    }
}
