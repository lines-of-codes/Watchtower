using TMPro;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    /// <summary>
    /// Invoked when the terminal is being submitted.
    /// Provides a value of the terminal input as the parameter.
    /// </summary>
    public event System.Action<string> OnTerminalSubmit;

    public static bool isTerminalActive { get; private set; } = false;

    [SerializeField] GameObject terminal;
    [SerializeField] TMP_InputField terminalInput;
    [SerializeField] TextMeshProUGUI terminalText;

    /// <summary>
    /// When a terminal submit signal has been sent from the PlayerUI script.
    /// </summary>
    public void OnTerminalSubmitInput()
    {
        OnTerminalSubmit?.Invoke(terminalInput.text);
    }

    public void ToggleTerminal()
    {
        isTerminalActive = !isTerminalActive;
        terminal.SetActive(isTerminalActive);
    }

    public void SetRequiringInput(bool requiringInput) => terminalInput.interactable = requiringInput;

    public void Print(string message) => terminalText.text += message;
    public void PrintLn(string message) => Print(message + "\n");

    public void ClearTerminal() => terminalText.text = "";
}
