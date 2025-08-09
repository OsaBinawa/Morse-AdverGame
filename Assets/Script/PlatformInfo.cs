using UnityEngine;

public class PlatformInfo : MonoBehaviour
{
    public float length = 5f; // manually set in Inspector or calculate from collider

    void Reset()
    {
        // Try to auto-set from collider
        var col = GetComponent<BoxCollider2D>();
        if (col)
            length = col.size.x;
    }
}
