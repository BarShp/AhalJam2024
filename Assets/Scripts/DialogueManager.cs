using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class DialogueManager : BaseMonoBehaviour
{
    DialogueSO textDialogue;
    public TextMeshProUGUI textComponent;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && textDialogue != null)
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
            gameObject.SetActive(false);
        }
            
    }
}
