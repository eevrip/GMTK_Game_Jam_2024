using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool played = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!played && other.gameObject.CompareTag("Player"))
        {
            if (dialogue != null)
                TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        played = true;
    }
}