using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct SimpleTimelineAction
{
    public float delayBeforeAction;
    public UnityEvent action;
}

public class SimpleTimeline : MonoBehaviour
{
    public List<SimpleTimelineAction> timelineActions;
    public bool isPaused;

    [Tooltip("Start when the Start() method is called by Unity.")]
    [SerializeField]
    bool startOnStart;

    private void Start()
    {
        if (startOnStart) Play();
    }

    public void Play()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        foreach(SimpleTimelineAction action in timelineActions)
        {
            while (isPaused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(action.delayBeforeAction);
            action.action.Invoke();
        }
    }
}
