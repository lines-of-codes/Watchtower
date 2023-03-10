using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [Tooltip("The player object")]
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform minimapCamera;
    [Tooltip("The minimum x, z pos the camera can go to. (Y axis is ignored)")]
    [SerializeField]
    Vector3 minPos;
    [Tooltip("The maximum x, z pos that the camera can go to. (Y axis is ignored)")]
    [SerializeField]
    Vector3 maxPos;

    private void Update()
    {
        Vector3 playerPos = player.position;
        minimapCamera.position = new Vector3(
            Mathf.Clamp(playerPos.x, minPos.x, maxPos.x), 
            transform.position.y, 
            Mathf.Clamp(playerPos.z, minPos.z, maxPos.z)
        );
    }
}
