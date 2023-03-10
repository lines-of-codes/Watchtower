using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent onInteracted;
    public string interactActionName;

    private void OnTriggerEnter(Collider other)
    {
        InteractableManager.CurrentInteractableObject = this;
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableManager.CurrentInteractableObject = null;
    }

    public void OnInteracted()
    {
        Debug.Log("OnInteracted called.");
        onInteracted.Invoke();
    }
}
