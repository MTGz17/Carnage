using UnityEngine;
using Unity.Cinemachine;

public class EndSequence : MonoBehaviour
{
    [SerializeField] private CinemachineCamera endCamera;
    [SerializeField] private int newPriority = 20;
    private bool isTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered || !other.CompareTag("Player")) return;
        isTriggered = true;

        if (endCamera != null)
        {
            endCamera.LookAt = other.transform;

            endCamera.Priority = newPriority;

            Debug.Log("End camera activated and now looking at player.");
        }
        else
        {
            Debug.LogWarning("No CinemachineCamera found in the scene!");
        }

    }
}