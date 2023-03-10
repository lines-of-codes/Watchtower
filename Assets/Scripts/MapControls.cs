using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapControls : MonoBehaviour
{
    public short minYZoom;
    public short maxYZoom;
    public ushort minMoveSpeed = 1;
    public ushort maxMoveSpeed = 5;
    public Slider zoomSlider;
    [SerializeField] Transform fullMapCamera;
    [Tooltip("The minimum x, z pos the camera can go to when in the maximum zoom. (Y axis is ignored)")]
    [SerializeField]
    Vector3 maxZoomMinPos;
    [Tooltip("The maximum x, z pos that the camera can go to when in the maximum zoom. (Y axis is ignored)")]
    [SerializeField]
    Vector3 maxZoomMaxPos;
    [Tooltip("The minimum x, z pos the camera can go to when in the minimum zoom. (Y axis is ignored)")]
    [SerializeField]
    Vector3 minZoomMinPos;
    [Tooltip("The maximum x, z pos that the camera can go to when in the minimum zoom. (Y axis is ignored)")]
    [SerializeField]
    Vector3 minZoomMaxPos;

    Vector2 moveDir;
    float currentMoveSpeed;

    private void Awake()
    {
        currentMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, zoomSlider.value);
        fullMapCamera.position = new Vector3(
                fullMapCamera.position.x,
                Mathf.Lerp(minYZoom, maxYZoom, zoomSlider.value),
                fullMapCamera.position.z);
        zoomSlider.onValueChanged.AddListener((float value) =>
        {
            currentMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, value);
            fullMapCamera.position = new Vector3(
                fullMapCamera.position.x, 
                Mathf.Lerp(minYZoom, maxYZoom, value), 
                fullMapCamera.position.z);
            ClampCameraPosition();
        });
    }

    private void Update()
    {
        if (moveDir.magnitude != 0)
        {
            fullMapCamera.position += new Vector3(moveDir.x * currentMoveSpeed, 0, moveDir.y * currentMoveSpeed);
            ClampCameraPosition();
        }
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    public void OnMapZoom(InputValue value)
    {
        float zoomDir = value.Get<float>();
        if (zoomDir != 0)
            zoomSlider.value += zoomDir / 1000f;
    }

    void ClampCameraPosition()
    {
        Vector3 camPos = fullMapCamera.position;
        fullMapCamera.position = new Vector3(
            Mathf.Clamp(
                camPos.x,
                Mathf.Lerp(maxZoomMinPos.x, minZoomMinPos.x, zoomSlider.value),
                Mathf.Lerp(maxZoomMaxPos.x, minZoomMaxPos.x, zoomSlider.value)),
            camPos.y,
            Mathf.Clamp(
                camPos.z,
                Mathf.Lerp(maxZoomMinPos.z, minZoomMinPos.z, zoomSlider.value),
                Mathf.Lerp(maxZoomMaxPos.z, minZoomMaxPos.z, zoomSlider.value)));
    }
}
