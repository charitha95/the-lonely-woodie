using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] private MenuButtonController menuButtonController;
    public bool disableOnce;
    public static bool trigger = true;

    private void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce && trigger)
        {
            menuButtonController.audioSource.PlayOneShot(whichSound);
        }
        else
        {
            disableOnce = false;
        }
    }
}