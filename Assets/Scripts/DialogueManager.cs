using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text skipButtonText;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeBetweenLetterAnimation = 0.02f;
    [SerializeField] private AudioSource skipSFX;
    [SerializeField] private AudioSource blipSFX;
    [SerializeField] private AudioSource sentenceAudioSource;

    [SerializeField] private bool waitForUserToClickContinue = false;
    [SerializeField] private float pauseBetweenMessages = 2f;

    private Queue<Sentence> sentences;
    private int timesClickedOnSkip = 0;
    private int amountOfSentencesAtStart;
    private int functionAtSentence = 0;

    private UnityEvent function;

    public GameObject player;
    public GameObject tim;

    public void Awake()
    {
        sentences = new Queue<Sentence>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tim = GameObject.FindGameObjectWithTag("Tim");
    }
    // Start is called before the first frame update
    public void StartDialogue(Dialogue dialogue)
    {
        functionAtSentence = dialogue.functionAtSentence;
        function = dialogue.function;

        if (animator)
            animator.SetBool("IsOpen", true);

        if (dialogue.freezeTime)
            Time.timeScale = 0;

        if (dialogue.name != "")
        {
            if (nameText)
                nameText.text = dialogue.name;
        }
        if (sentences != null)
        {
            sentences.Clear();
        }

        if (dialogue.pickRandom)
        {
            int r = Random.Range(0, dialogue.sentences.Length);
            sentences.Enqueue(dialogue.sentences[r]);
            amountOfSentencesAtStart = 1;
        }
        else
        {
            foreach (var sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            amountOfSentencesAtStart = sentences.Count;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (functionAtSentence == amountOfSentencesAtStart - sentences.Count)
        {
            if (function != null)
            {
                function.Invoke();
            }
        }
        Sentence sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(CheckNextSentence(sentence));
    }

    private IEnumerator CheckNextSentence(Sentence sentence)
    {
        if (sentence.sentenceSFX)
        {
            sentenceAudioSource.clip = sentence.sentenceSFX;
            sentenceAudioSource.Play();
        }

        yield return StartCoroutine(TypeSentence(sentence));

        if (!waitForUserToClickContinue)
        {
            yield return new WaitForSecondsRealtime(pauseBetweenMessages);

            DisplayNextSentence();
        }
    }

    private IEnumerator TypeSentence(Sentence sentence)
    {
        dialogueText.text = "";
        if (blipSFX)
            blipSFX.Play();
        foreach (var letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;

            // We want to keep the scroller at the bottom so new text is visible
            if (scrollbar)
            {
                Canvas.ForceUpdateCanvases();
                scrollbar.value = 0f;
            }

            yield return new WaitForSecondsRealtime(timeBetweenLetterAnimation);
        }
        if (blipSFX)
            blipSFX.Stop();
    }

    private void EndDialogue()
    {
        Time.timeScale = 1;
        if (animator)
            animator.SetBool("IsOpen", false);
        timesClickedOnSkip = 0;
        if (skipButtonText)
            skipButtonText.text = "Skip";

        player.GetComponent<PlayerMovement>().attacked = false;
        tim.GetComponent<TimTheTardigrade>().canMove = true;
    }

    public void SkipDialogue()
    {
        timesClickedOnSkip++;
        switch (timesClickedOnSkip)
        {
            case 1:
                skipButtonText.text = "Sure?";
                break;

            case 2:
                EndDialogue();
                if (skipSFX)
                    skipSFX.Play();
                if (sentenceAudioSource.isPlaying)
                    sentenceAudioSource.Stop();
                FindAnyObjectByType<PlayerMovement>().attacked = false;
                break;
        }
        
    }
}