using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CombatDialogue : MonoBehaviour
{
    [SerializeField] private BreathingController breathingController;
    [SerializeField] private string[] startingMessages;
    [FormerlySerializedAs("messages")] [SerializeField] private string[] battleTips;
    [SerializeField] private float messagesInterval;
    [SerializeField] private string winText;
    [SerializeField] private string loseText;

    [SerializeField] private TMP_Text text;

    private int startingMessagesIndex = 0;
    private bool isDialoguesRunning;
    private bool gameStarted = false;

    private void Start()
    {
        text.text = startingMessages[startingMessagesIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            startingMessagesIndex++;
            if (startingMessagesIndex >= startingMessages.Length && !gameStarted)
            {
                breathingController.StartGame();
                gameStarted = true;
            }
        }
    }

    public void StartDialogues()
    {
        isDialoguesRunning = true;
        StartCoroutine(StartDialoguesCoroutine());
    }

    public void EndDialogues(bool win)
    {
        isDialoguesRunning = false;
        StopAllCoroutines();
        text.text = win ? winText : loseText;
    }

    private IEnumerator StartDialoguesCoroutine()
    {
        while (isDialoguesRunning)
        {
            var randomIndex = Random.Range(0, battleTips.Length);
            text.text = battleTips[randomIndex];
            
            yield return new WaitForSeconds(messagesInterval);
        }
    }
}
