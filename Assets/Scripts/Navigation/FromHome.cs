using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromHome : MonoBehaviour
{
    public MenuButtonController menuButtonController;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (menuButtonController.index == 0)
            {
                FindObjectOfType<LevelLoader>().LoadLevel("Story");
            }
            else if (menuButtonController.index == 1)
            {
                FindObjectOfType<LevelLoader>().LoadLevel("About");
            }
        }
    }
}