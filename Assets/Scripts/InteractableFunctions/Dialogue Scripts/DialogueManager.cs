using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;

    private Queue<string> sentences;
    private bool dialogueStarted;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        dialogueStarted = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!dialogueStarted)
        {
            Debug.Log("Starting new dialogue interaction.");
            dialogueBox.SetActive(true);
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            dialogueStarted = true;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            Debug.Log("No more phrases to display. Requesting text box closure.");
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log("Displaying sentence: " + sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("Closing text box.");
        dialogueBox.SetActive(false);
        FindObjectOfType<PlayerController>().UnlockMovement();
        dialogueStarted = false; // Reset dialogue status
    }
}

