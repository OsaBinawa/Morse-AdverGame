using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public List<GameObject> prefabList;
    public int poolSize = 20;

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            
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
        poolQueue.Enqueue(obj); 
        return obj;
    }
}
