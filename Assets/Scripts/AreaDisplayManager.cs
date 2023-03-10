using CodeMonkey.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AreaDisplayManager : MonoBehaviour
{
    public static AreaDisplayManager Instance { get; private set; }
    [SerializeField] RectTransform areaDisplayObject;
    [SerializeField] TextMeshProUGUI placeNameText;
    [SerializeField] float hideAtYPos = 60f;
    [SerializeField] float showAtYPos = -75f;
    [SerializeField] float showDuration = 2f;
    [SerializeField] float transitionDuration = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPlayerTouchedNewArea(string areaName)
    {
        areaDisplayObject.gameObject.SetActive(true);
        areaDisplayObject.DOAnchorPos3DY(showAtYPos, transitionDuration);
        placeNameText.text = areaName;
        FunctionTimer.Create(() =>
        {
            areaDisplayObject.DOAnchorPos3DY(hideAtYPos, transitionDuration);
            FunctionTimer.Create(
                () => areaDisplayObject.gameObject.SetActive(false), 
                transitionDuration);
        }, showDuration);
    }
}
