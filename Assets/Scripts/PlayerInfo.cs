using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }
    public ushort maxStamina = 300;
    public ushort maxMana = 250;

    float _stamina;
    public float stamina {
        get => _stamina; 
        set
        {
            _stamina = value;
            PlayerUI.Instance.StaminaSlider.value = _stamina;
        }
    }
    float _mana;
    public float mana
    {
        get => _mana;
        set
        {
            _mana = value;
            PlayerUI.Instance.ManaSlider.value = _mana;
        }
    }

    public List<Spell> spells = new();

    [SerializeField] FirstPersonController controller;
    [SerializeField] GameObject flashlight;

    bool flashlightActive = false;

    float sprintSpeed = 14;
    bool isSprinting;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach(Spell spell in spells)
        {
            spell.OnCooldown = false;
        }
        stamina = maxStamina;
        mana = maxMana;
        PlayerUI.Instance.StaminaSlider.maxValue = maxStamina;
        PlayerUI.Instance.ManaSlider.maxValue = maxMana;
        sprintSpeed = controller.SprintSpeed;
    }

    private void FixedUpdate()
    {
        if (isSprinting)
        {
            if (stamina <= 0)
                controller.SprintSpeed = controller.MoveSpeed;
            else stamina -= 1;
        }
        else if (stamina < maxStamina)
            stamina += 1;
        if (mana < maxMana)
            mana += 0.1f;
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
        if (value.isPressed)
            controller.SprintSpeed = sprintSpeed;
        else controller.SprintSpeed = controller.MoveSpeed;
    }

    public void OnToggleFlashlight(InputValue _)
    {
        flashlightActive = !flashlightActive;
        flashlight.SetActive(flashlightActive);
    }
}
