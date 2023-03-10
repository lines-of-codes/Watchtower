using UnityEngine;

public class ScifiDoorAnimator : MonoBehaviour
{
    [Tooltip("The area that the door is in.")]
    [SerializeField] Area area;
    [SerializeField] Animator animator;
    [SerializeField] bool toggleOnTrigger;
    bool isDoorOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && toggleOnTrigger) ToggleDoorOpen();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && toggleOnTrigger) ToggleDoorOpen();
    }

    public void ToggleDoorOpen()
    {
        if (area != null && !area.isPowered)
        {
            return;
        }
        isDoorOpened = !isDoorOpened;
        animator.SetBool("character_nearby", isDoorOpened);
    }
}
