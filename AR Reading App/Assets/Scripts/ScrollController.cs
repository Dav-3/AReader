using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the scrolling of the text view; e.g. it implements the automatic scrolling
/// </summary>
public class ScrollController : MonoBehaviour
{
    /// <summary>
    /// The scrolling speed
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// If set to true, the text will scroll automatically by the set speed
    /// </summary>
    public bool automaticScroll;

    // necessary references to UI components and their settings
    ScrollRect scrollRect;
    RectTransform contentTransform;
    float height;

    /// <summary>
    /// Gets the necessary references of the UI
    /// </summary>
    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentTransform = transform.Find("Viewport/Content").GetComponent<RectTransform>();
    }

    /// <summary>
    /// Called once per frame; scrolls the text by the speed if automatic scrolling is activated
    /// </summary>
    void Update()
    {
        if (automaticScroll)
        {
            Scroll(speed);
        }
    }

    /// <summary>
    /// Toggles the automatic scrolling
    /// </summary>
    public void ToggleAutomaticScrolling()
    {
        automaticScroll = !automaticScroll;
    }

    /// <summary>
    /// Scrolls the text by the given speed (for one frame only)
    /// To get a repeated scrolling movement, call this function periodically
    /// </summary>
    /// <param name="speed">The speed per second at which the text should be scrolled</param>
    public void Scroll(float speed)
    {
        // get the height of the text content; consider this so that the text scrolls with the same speed for all text lengths
        height = contentTransform.rect.height;

        // if the height is 0, there is nothing to scroll (and it avoids a division by 0)
        if (height != 0)
        {
            scrollRect.verticalNormalizedPosition -= speed * Time.deltaTime / height;
            // make sure that the scrolling does not exceed the limits
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
        }
    }
}
