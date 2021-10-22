using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    private bool canContinue = true;
    private DialogueTrigger dt;
    private int count = 0;

    // Start is called before the first frame update
    private void Start()
    {
        dt = FindObjectOfType<DialogueTrigger>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            if (canContinue)
            {
                dt.StartDialogue();
                canContinue = false;
            }
            if (dt.messages.Length == count)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}