using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool pickRandom = false;
    public int functionAtSentence = 0;
    public UnityEvent function;
    //public AudioClip sentenceSFX;

    //[TextArea(3, 10)]
    //public string[] sentences;
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence
{
    [TextArea(3, 10)]
    public string text;
    public AudioClip sentenceSFX;
}
