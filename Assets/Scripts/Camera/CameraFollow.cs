using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public string targetTag = "Player"; // The tag of the GameObject to follow
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // Find the CinemachineVirtualCamera component attached to this GameObject
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Find the GameObject with the specified tag and set it as the Follow target
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target != null)
        {
            virtualCamera.Follow = target.transform;
        }
        else
        {
            Debug.LogError("No GameObject with the tag '" + targetTag + "' found!");
        }
    }
}
