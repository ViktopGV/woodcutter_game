using UnityEngine;

public class CameraRatio : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        float ratio = ((float)Screen.width / Screen.height);
        cam.orthographicSize = cam.orthographicSize - ratio * 1.2f;
        cam.transform.position = new Vector3(0, cam.transform.position.y - ratio * 1.1f, -10);
    }
}
