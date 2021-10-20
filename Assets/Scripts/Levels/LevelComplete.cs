using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public DialogueTrigger trigger;
    private new CapsuleCollider2D collider;
    private new AudioSource audio;
    public Text countText;
    public string countCompleteText;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            trigger.StartDialogue();
            collider.enabled = false;
            StartCoroutine(BreakObject());
        }
    }

    private IEnumerator BreakObject()
    {
        audio.Play();
        countText.text = countCompleteText;
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}