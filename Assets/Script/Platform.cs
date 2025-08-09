using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public List<GameObject> prefabList; // all your platform prefabs
    public int poolSize = 20;           // total pooled objects

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            // Pick a random prefab to fill the pool initially
            GameObject obj = Instantiate(prefabList[Random.Range(0, prefabList.Count)]);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(Vector3 position)
    {
        GameObject obj = poolQueue.Dequeue();
        obj.transform.position = position;
        obj.SetActive(true);
        poolQueue.Enqueue(obj); // put back into rotation
        return obj;
    }
}
