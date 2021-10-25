using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    public static float duration = 1f;
    private static float pendingFreezeDuration = 0f;
    private bool isFrozen = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (pendingFreezeDuration > 0 && !isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public static void Freeze()
    {
        pendingFreezeDuration = duration;
    }

    private IEnumerator DoFreeze()
    {
        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = original;
        pendingFreezeDuration = 0;
        isFrozen = false;
    }
}