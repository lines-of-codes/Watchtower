using FMODUnity;
using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    [SerializeField] StudioEventEmitter soundEmitter;

    private void OnEnable()
    {
        soundEmitter.Play();
        soundEmitter.SetParameter("FlashlightState", 0);
    }

    private void OnDisable()
    {
        soundEmitter.Play();
        soundEmitter.SetParameter("FlashlightState", 1);
    }
}
