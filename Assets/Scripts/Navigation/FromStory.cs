using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FromStory : MonoBehaviour
{
    public GameObject uiLayer;
    private bool canContinue = false;
    public Text buttonText;
    private bool checkNavigation = false;

    private void Start()
    {
        AnimatorFunctions.trigger = false;
        StartCoroutine(MakeButtonOperatable());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canContinue)
        {
            FindObjectOfType<DialogueTrigger>().StartDialogue();
            canContinue = false;
            checkNavigation = true;
            Destroy(uiLayer);
        }

        if (Input.GetKeyDown(KeyCode.Space) && checkNavigation)
        {
            var dm = FindObjectOfType<DialogueManager>();

            if (dm.activeMessage == dm.currentMessages.Length - 1)
            {
                LeanTween.textAlpha(buttonText.rectTransform, 0, 0);
                buttonText.text = "Become a Woodie";
                buttonText.fontSize = 32;
                LeanTween.textAlpha(buttonText.rectTransform, 1, 0.5f);
            }
            if (dm.activeMessage == dm.currentMessages.Length)
            {
                FindObjectOfType<LevelLoader>().LoadLevel("Level 1");
            }
        }
    }

    private IEnumerator MakeButtonOperatable()
    {
        yield return new WaitForSeconds(12f);
        AnimatorFunctions.trigger = true;
        canContinue = true;
    }
}