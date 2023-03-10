using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable UNT0008
public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }
    [field: SerializeField] public Spell CurrentSpell { get; private set; }
    public Slider StaminaSlider;
    public Slider ManaSlider;
    public SpellUI SpellUI;
    [SerializeField] TerminalManager terminalManager;
    [SerializeField] GameObject map;
    [SerializeField] GameObject pauseMenu;

    int activeSpellIndex;
    bool isMapActive;
    bool isPauseMenuActive;

    private void Awake()
    {
        Instance = this;
    }

    public void OnMapToggle(InputValue _)
    {
        isMapActive = !isMapActive;
        map?.SetActive(isMapActive);
    }

    public void OnPauseMenuToggle(InputValue _)
    {
        isPauseMenuActive = !isPauseMenuActive;
        pauseMenu.SetActive(isPauseMenuActive);
        if (isPauseMenuActive)
            Pause();
        else Resume();
    }

    public void OnTerminalSubmit(InputValue _)
    {
        TriggerStorySubtitle.ActiveStory?.TriggerStory();
        terminalManager?.OnTerminalSubmitInput();
    }

    public void OnSwitchSpell(InputValue axis)
    {
        if (PlayerInfo.Instance.spells.Count == 0)
            return;

        int addToSpellIndex = (int)axis.Get<float>();

        if (activeSpellIndex == 0 && addToSpellIndex < 0)
        {
            addToSpellIndex = PlayerInfo.Instance.spells.Count - 1;
        }
        else if (activeSpellIndex + addToSpellIndex >= PlayerInfo.Instance.spells.Count)
        {
            addToSpellIndex = -activeSpellIndex;
        }

        activeSpellIndex += addToSpellIndex;

        CurrentSpell = PlayerInfo.Instance.spells[activeSpellIndex];
        SpellUI.UpdateCurrentSpell();
    }

    public void SwitchSpell(ushort spellIndex)
    {
        activeSpellIndex = spellIndex - activeSpellIndex;
        CurrentSpell = PlayerInfo.Instance.spells[activeSpellIndex];
        SpellUI.UpdateCurrentSpell();
    }

    void Pause()
    {
        Time.timeScale = 0.1f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SaveAndQuit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
