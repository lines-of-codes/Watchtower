using UnityEngine;

public class FMODCutsceneEventEmitter : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform player;

    private void Update()
    {
        target.position = player.position;
    }
}
