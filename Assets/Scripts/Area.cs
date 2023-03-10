using UnityEngine;
using UnityEngine.Events;

public class Area : MonoBehaviour
{
    public bool isPowered;
    public UnityEvent OnPowerToggled;
    [SerializeField] string areaName;

    private void OnTriggerEnter(Collider other)
    {
        AreaDisplayManager.Instance.OnPlayerTouchedNewArea(areaName);
    }

    public void TogglePower()
    {
        isPowered = !isPowered;
        OnPowerToggled.Invoke();
    }
}
