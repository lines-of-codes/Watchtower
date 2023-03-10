using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

public class TriggerStorySubtitle : MonoBehaviour
{
    [System.Serializable]
    public struct Story
    {
        public string speaker;
        public string message;
        public UnityEvent onMessageDisplayed;
    }

    public static TriggerStorySubtitle ActiveStory;
    [SerializeField] bool triggerOnStart;
    [SerializeField] List<Story> stories;
    [SerializeField] StudioEventEmitter cutsceneEventEmitter;
    [SerializeField] float eachSentenceAddBy = 0.05f;
    [SerializeField] StoryUI storyUI;
    [Tooltip("An interval between characters in the text animation.")]
    [SerializeField]
    float intervalBetweenCharacters = 0.05f;
    public bool queueToOtherComponent;
    public TriggerStorySubtitle targetQueue;

    Queue<char> textToBeAdded = new();
    byte storyIndex = 0;
    float sentence = 0f;

    private void Start()
    {
        if (triggerOnStart) TriggerStory();
    }

    public void TriggerStory()
    {
        if (queueToOtherComponent)
        {
            targetQueue.QueueStory(stories);
            return;
        }
        if (storyIndex >= stories.Count && textToBeAdded.Count == 0)
        {
            ActiveStory = null;
            storyUI.dialogUI.SetActive(false);
            return;
        }
        ActiveStory = this;
        storyUI.dialogUI.SetActive(true);
        if (textToBeAdded.Count != 0)
        {
            Story story = stories[--storyIndex];
            textToBeAdded.Clear();
            storyUI.message.text = story.message;
        }
        else
        {
            Story story = stories[storyIndex];
            storyUI.speaker.text = story.speaker;
            storyUI.message.text = string.Empty;
            textToBeAdded = new(story.message);
            StartCoroutine(UpdateTextAnimation());
            story.onMessageDisplayed.Invoke();
            cutsceneEventEmitter?.Play();
            cutsceneEventEmitter?.SetParameter("Sentence", sentence);
            sentence += eachSentenceAddBy;
        }
        storyIndex++;
    }

    public void QueueStory(IEnumerable<Story> newStories)
    {
        stories.AddRange(newStories);
        if (ActiveStory == null)
            TriggerStory();
    }

    IEnumerator UpdateTextAnimation()
    {
        while (true)
        {
            if (textToBeAdded.Count == 0) break;
            storyUI.message.text += textToBeAdded.Dequeue();
            yield return new WaitForSeconds(intervalBetweenCharacters);
        }
    }
}
