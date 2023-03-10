using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField] Color OnCooldownSpellIconColor;
    [SerializeField] Image SpellImage;
    [SerializeField] GameObject SpellOnCooldownText;
    [SerializeField] TextMeshProUGUI SpellName;

    public void UpdateCurrentSpell()
    {
        Spell spell = PlayerUI.Instance.CurrentSpell;
        SpellName.text = spell.SpellName;
        SpellImage.color = spell.OnCooldown ? OnCooldownSpellIconColor : Color.white;
        SpellImage.sprite = spell.SpellIcon;
        SpellOnCooldownText.SetActive(spell.OnCooldown);
    }
}
