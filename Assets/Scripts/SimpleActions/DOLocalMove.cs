using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Events;

public class DOLocalMove : MonoBehaviour
{
    public Transform targetTransform;
    [Tooltip("The target move position. (both to or from)")]
    public Vector3 moveTarget;
    public float duration;
    public bool from;
    public bool playOnStart;
    public Ease tweenEasing = Ease.Linear;
    public UnityEvent onFinishedTweening;

    private void Start()
    {
        if (playOnStart) DoMove();
    }

    public void DoMove()
    {
        Debug.Log("DoMove() called");
        var tween = targetTransform.DOLocalMove(moveTarget, duration);
        tween.SetEase(tweenEasing);
        if (from) tween.From();
        tween.OnComplete(onFinishedTweening.Invoke);
    }

    /// <summary>
    /// Sets a new position. Used in the Unity editor.
    /// Accepts a comma seperated string, then parsed into a Vector3
    /// </summary>
    /// <param name="positionStr"></param>
    public void SetNewPos(string positionStr)
    {
        List<float> floats = new();
        string[] strings = positionStr.Split(',');
        foreach (string item in strings)
        {
            floats.Add(Convert.ToSingle(item));
        }
        moveTarget = new Vector3(floats[0], floats[1], floats[2]);
    }
}
