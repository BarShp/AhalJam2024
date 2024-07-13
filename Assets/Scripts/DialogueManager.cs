using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class DialogueManager : BaseMonoBehaviour
{
    DialogueSO textDialogue;
    private GameObject dialogueBox;
    public TextMeshProUGUI textComponent;
    private int index;

    void Start()
    {
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueBox.SetActive(false);
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && textDialogue != null)
        {
            if (textComponent.text == textDialogue.lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = textDialogue.lines[index];
            }
        }
    }

    public void StartDialogue(DialogueSO newDialogueText)
    {                
        StopAndClearDialogue();
        dialogueBox.SetActive(true);
        EventsManager.Instance.InvokeEvent(EventType.OnDialogueChange, true);
        textDialogue = newDialogueText;
        StartCoroutine(TypeLine());
        
    }

    public void StopAndClearDialogue()
    {
        StopAllCoroutines();
        textComponent.text = string.Empty;        
        index = 0;
    }

    IEnumerator TypeLine()
    {
        foreach(char c in textDialogue.lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textDialogue.textSpeed);
        }
    }

    void NextLine()
    {
        if (index < textDialogue.lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
            EventsManager.Instance.InvokeEvent(EventType.OnDialogueChange, false);
        }
            
    }
}
