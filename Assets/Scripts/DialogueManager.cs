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
        if((Input.GetKeyDown(KeyCode.G) || textDialogue.lines[index].AutoSkip) && textDialogue != null)
        {
            if (textComponent.text == textDialogue.lines[index].text)
            {
                NextLine();
            }
            else if (!textDialogue.lines[index].AutoSkip)
            {
                StopAllCoroutines();
                textComponent.text = textDialogue.lines[index].text;
            }
        }
    }

    public void StartDialogue(DialogueSO newDialogueText)
    {                
        StopAndClearDialogue();
        EventsManager.Instance.InvokeEvent(EventType.OnDialogueChange, true);
        dialogueBox.SetActive(true);
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
        switch (textDialogue.lines[index].speaker)
        {
            case DialogueSpeaker.VoicesInMyHead:
                textComponent.color = Color.black;            
                textComponent.fontStyle = FontStyles.Italic;
                break;

            case DialogueSpeaker.Therapist:
                textComponent.color = Color.grey;
                textComponent.fontStyle = FontStyles.Normal;                
                break;
            
            case DialogueSpeaker.Self:
                textComponent.color = Color.black;
                textComponent.fontStyle = FontStyles.Normal;
            break;
        }
        foreach(char c in textDialogue.lines[index].text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textDialogue.lines[index].textSpeed);
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
