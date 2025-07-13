using UnityEngine;

public class FloatBob : MonoBehaviour
{
    [Tooltip("How much the object moves up and down (in world units)")]
    [SerializeField] private float bobHeight = 0.01f;

    [Tooltip("How fast the bobbing animation cycles")]
    [SerializeField] private float bobSpeed = 0.1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;

        // Safety check to override Unity defaults if 0
        if (bobHeight == 0f) bobHeight = 0.01f;
        if (bobSpeed == 0f) bobSpeed = 0.1f;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = startPos + new Vector3(0f, offset, 0f);
    }
}
