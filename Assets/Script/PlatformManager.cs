using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Platform pooler;
    [Range(10,20)]public float moveSpeed = 5f;
    public int visiblePlatforms = 5;
    public float spawnY = 0f; // << Manually set this in Inspector

    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        // Spawn initial platforms
        Vector3 spawnPos = new Vector3(0, spawnY, 0);
        for (int i = 0; i < visiblePlatforms; i++)
        {
            GameObject platform = pooler.GetFromPool(spawnPos);
            activePlatforms.Add(platform);

            float length = platform.GetComponent<PlatformInfo>().length;
            spawnPos.x += length;
        }
    }

    void Update()
    {
        for (int i = 0; i < activePlatforms.Count; i++)
        {
            GameObject p = activePlatforms[i];
            p.transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // If fully off-screen
            float endX = p.transform.position.x + p.GetComponent<PlatformInfo>().length;
            if (endX < -10f) // Despawn point
            {
                // Find last platform
                GameObject last = GetRightMostPlatform();
                float lastEndX = last.transform.position.x + last.GetComponent<PlatformInfo>().length;

                // New position with fixed spawnY
                Vector3 newPos = new Vector3(lastEndX, spawnY, 0);

                GameObject newPlatform = pooler.GetFromPool(newPos);
                activePlatforms[i] = newPlatform;
            }
        }
    }

    GameObject GetRightMostPlatform()
    {
        GameObject rightMost = activePlatforms[0];
        float maxEndX = rightMost.transform.position.x + rightMost.GetComponent<PlatformInfo>().length;

        foreach (var p in activePlatforms)
        {
            float endX = p.transform.position.x + p.GetComponent<PlatformInfo>().length;
            if (endX > maxEndX)
            {
                rightMost = p;
                maxEndX = endX;
            }
        }
        return rightMost;
    }
}
