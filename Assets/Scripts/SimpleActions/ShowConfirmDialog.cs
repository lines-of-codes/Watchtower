using UnityEngine;
using UnityEngine.Events;

public class ShowConfirmDialog : MonoBehaviour
{
    [SerializeField] string question;
    [SerializeField] UnityEvent OnConfirmed;
    [SerializeField] UnityEvent OnCancelled;

    public void ShowDialog()
    {
        ConfirmDialog.ShowConfirmationDialog(question, (answer) =>
        {
            if (answer)
                OnConfirmed.Invoke();
            else OnCancelled.Invoke();
        });
    }
}
