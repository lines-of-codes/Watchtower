using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Objects/Spells")]
public class Spell : ScriptableObject
{
    public string SpellName;
    public GameObject SpellPrefab;
    public Sprite SpellIcon;
    public byte ManaCost;
    public byte Duration;
    public byte Cooldown;
    public byte Damage;
    public bool OnCooldown;
}
