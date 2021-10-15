using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    public Message[] currentMessages;
    private Actor[] currentActors;
    public int activeMessage = 0;

    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseOutBounce();
    }

    private void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
            AnimateTextMessage();
        }
        else
        {
            isActive = false;
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInBounce();
        }
    }

    private void AnimateTextMessage()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
        LeanTween.textAlpha(actorName.rectTransform, 0, 0);
        LeanTween.textAlpha(actorName.rectTransform, 1, 0.5f);
    }

    // Start is called before the first frame update
    private void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            NextMessage();
        }
    }
}