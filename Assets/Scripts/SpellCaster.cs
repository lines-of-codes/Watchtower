using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] Transform spellParent;

    public void OnCastSpell(InputValue _)
    {
        Spell spell = PlayerUI.Instance.CurrentSpell;
        if (spell == null || spell.OnCooldown) return;
        spell.OnCooldown = true;
        PlayerUI.Instance.SpellUI.UpdateCurrentSpell();
        GameObject createdSpell = Instantiate(spell.SpellPrefab, spellParent);
        PlayerInfo.Instance.mana -= spell.ManaCost;
        FunctionTimer.Create(() => Destroy(createdSpell), spell.Duration);
        FunctionTimer.Create(() =>
        {
            spell.OnCooldown = false;
            PlayerUI.Instance.SpellUI.UpdateCurrentSpell();
        }, spell.Cooldown);
    }
}
