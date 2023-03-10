using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance { get; private set; }
    static InteractableObject _currentInteractableObject = null;
    public static InteractableObject CurrentInteractableObject
    {
        get => _currentInteractableObject;
        set
        {
            _currentInteractableObject = value;
            Instance.OnCurrentInteractableChanged();
        }
    }

    [SerializeField] GameObject interactOption;
    [SerializeField] TextMeshProUGUI interactActionLabel;

    private void Awake()
    {
        Instance = this;
    }

    void OnCurrentInteractableChanged()
    {
        if (CurrentInteractableObject == null)
        {
            interactOption.SetActive(false);
        } else
        {
            interactOption.SetActive(true);
            interactActionLabel.text = CurrentInteractableObject.interactActionName;
        }
    }

    public void OnInteract(InputValue _)
    {
        CurrentInteractableObject?.OnInteracted();
    }
}
