using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextPulsing : MonoBehaviour
{
    // Grow parameters
    private float approachSpeed = 0.001f;
    private float growthBound = 1.0f;
    private float shrinkBound = 0.9f;
    private float currentRatio = 1;

    // The text object we're trying to manipulate
    private Text text;
    private float originalFontSize;

    // And something to do the manipulating
    private Coroutine routine;
    private bool keepGoing = true;

    // Attach the coroutine
    void Awake()
    {
        // Find the text  element we want to use
        text = gameObject.GetComponent<Text>();

        // Then start the routine
        routine = StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (currentRatio != growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                text.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (currentRatio != shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                text.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}