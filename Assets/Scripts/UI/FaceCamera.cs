using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            Vector3 direction = transform.position - Camera.main.transform.position;
            direction.y = 0; // Optional: keep it upright
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
