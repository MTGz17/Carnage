using UnityEngine;
using Unity.Cinemachine;

public class CameraSpin : MonoBehaviour
{
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
    [SerializeField] private float spinSpeed = 30f;

    void Update()
    {
        if (orbitalFollow == null)
            return;

        var axis = orbitalFollow.HorizontalAxis;

        axis.Value += spinSpeed * Time.deltaTime;

        orbitalFollow.HorizontalAxis = axis;
    }
}