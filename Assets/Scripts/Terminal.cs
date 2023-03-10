using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct TerminalChoice
{
    public string choiceName;
    public UnityEvent choiceEvent;
}

public class Terminal : MonoBehaviour
{
    public const string TerminalName = "<color=green>MintyMint</color>";
    [Tooltip("Can UI Ghost appear on this terminal?")]
    public bool ghostableTerminal = true;
    [SerializeField] TerminalChoice[] terminalChoices;
    [SerializeField] string programName;
    [TextArea(2, 5)]
    [SerializeField]
    string programDescription;
    [SerializeField] TerminalManager manager;

    public void ToggleTerminal()
    {
        manager.ToggleTerminal();
        // Only is active when the terminal is becoming active
        if (TerminalManager.isTerminalActive)
        {
            InitializeTerminal();
            manager.OnTerminalSubmit += OnTerminalEntered;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            manager.OnTerminalSubmit -= OnTerminalEntered;
        }
    }

    void InitializeTerminal()
    {
        manager.ClearTerminal();
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine($"{TerminalName} Terminal // (C) {DateTime.Now.Year} Mintytech\n");
        stringBuilder.Append("Running script: ");
        stringBuilder.AppendLine(programDescription);
        stringBuilder.AppendLine();
        for (ushort i = 0; i < terminalChoices.Length; i++)
        {
            TerminalChoice terminalChoice = terminalChoices[i];
            stringBuilder.Append(i.ToString() + " > ");
            stringBuilder.AppendLine(terminalChoice.choiceName);
        }
        stringBuilder.AppendLine("Please enter the number of the choice you wanted.");
        Print(stringBuilder.ToString());
        manager.SetRequiringInput(true);
    }

    public void Print(string text) => manager.Print(text);
    public void PrintLn(string text) => manager.PrintLn(text);

    public void OnTerminalEntered(string input)
    {
        ushort selectedChoice = Convert.ToUInt16(input);
        terminalChoices[selectedChoice].choiceEvent.Invoke();
    }
}
