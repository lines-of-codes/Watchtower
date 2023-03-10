using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] Area area;
    [SerializeField] Light targetLight;

    private void Awake()
    {
        UpdateLight();
        area.OnPowerToggled.AddListener(UpdateLight);
    }

    public void UpdateLight()
    {
        targetLight.enabled = area.isPowered;
    }
}
