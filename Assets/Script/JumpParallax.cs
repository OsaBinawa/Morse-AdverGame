using UnityEngine;

public class JumpParallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer;       // Background object
        [Range(0f, 1f)]
        public float parallaxFactor;  // 0 = static, 1 = moves with target
    }

    [SerializeField] private Transform target;        // Player or Camera
    [SerializeField] private ParallaxLayer[] layers;  // List of backgrounds

    private Vector3 lastTargetPos;

    private void Start()
    {
        if (target == null)
            target = Camera.main.transform;  // fallback to camera

        lastTargetPos = target.position;
    }

    private void Update()
    {
        float deltaY = target.position.y - lastTargetPos.y;

        foreach (var l in layers)
        {
            if (l.layer != null)
            {
                l.layer.position += new Vector3(0, -deltaY * l.parallaxFactor, 0);
            }
        }

        lastTargetPos = target.position;
    }
}
