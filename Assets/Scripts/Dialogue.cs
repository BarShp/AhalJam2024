using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] DialogueSO textDialogue;
    public TextMeshProUGUI textComponent;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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

    void StartDialogue(DialogueSO newDialogueText)
    {
        index = 0;
        textDialogue = newDialogueText;
        StartCoroutine(TypeLine());
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
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
            
    }
}
