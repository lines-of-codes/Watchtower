using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SpellBook : MonoBehaviour
{
    public UnityEvent OnSpellCollected;
    [SerializeField] Spell Spell;

    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, 360, 0), 4f, RotateMode.WorldAxisAdd).SetLoops(-1);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void OnTriggerEnter(Collider other)
    {
        Spell.OnCooldown = false;
        PlayerInfo.Instance.spells.Add(Spell);
        PlayerUI.Instance.SwitchSpell((ushort)(PlayerInfo.Instance.spells.Count - 1));
        OnSpellCollected.Invoke();
        Destroy(gameObject);
    }
}
