using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool enableKeyHints = true;
    public static event Action onKeyHintsSettingChanged;

    private void Awake()
    {
        onKeyHintsSettingChanged += onKeyHintsChanged;
        onKeyHintsSettingChanged();
    }

    private void onKeyHintsChanged() => gameObject.SetActive(enableKeyHints);

    private void OnDestroy()
    {
        onKeyHintsSettingChanged -= onKeyHintsChanged;
    }
}
