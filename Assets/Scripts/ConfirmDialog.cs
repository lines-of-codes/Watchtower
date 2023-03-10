using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialog : MonoBehaviour
{
    static ConfirmDialog Instance;
    static Action<bool> onAnswerReceived;
    [SerializeField] GameObject confirmationBox;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] Button yesButton;

    private void Awake()
    {
        Instance = this;
    }

    public static void ShowConfirmationDialog(string question, Action<bool> onAnswerReceivedCallback)
    {
        Instance.ShowDialog(question);
        onAnswerReceived = onAnswerReceivedCallback;
    }

    public void ShowDialog(string question)
    {
        confirmationBox.SetActive(true);
        questionText.text = question;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnAnswer(bool isConfirmed)
    {
        Cursor.lockState = CursorLockMode.Locked;
        onAnswerReceived(isConfirmed);
        confirmationBox.SetActive(false);
    }
}
