using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reacts to navigation gestures and translates them to text scrolling
/// </summary>
public class TextInteractions : MonoBehaviour, INavigationHandler
{
    /// <summary>
    /// The scrolling speed
    /// </summary>
    public float speed = 1.5f;

    ScrollController scrollController;

    /// <summary>
    /// Gets the necessary reference to the scroll controller
    /// </summary>
    private void Awake()
    {
        scrollController = GetComponentInChildren<ScrollController>();
    }

    /// <summary>
    /// Called if the navigation is cancelled, e.g. the user's hand leaves the tracked area while performing a navigation gesture
    /// </summary>
    /// <param name="eventData">The data of this event</param>
    public void OnNavigationCanceled(NavigationEventData eventData)
    {
        // release the focus override again
        InputManager.Instance.OverrideFocusedObject = null;
    }

    /// <summary>
    /// Called if the navigation gesture is completed
    /// </summary>
    /// <param name="eventData">The data of this event</param>
    public void OnNavigationCompleted(NavigationEventData eventData)
    {
        // release the focus override again
        InputManager.Instance.OverrideFocusedObject = null;
    }

    /// <summary>
    /// Called if a navigation gesture is started
    /// </summary>
    /// <param name="eventData">The data of this event</param>
    public void OnNavigationStarted(NavigationEventData eventData)
    {
        // override the focused object so that the focus stays on this object
        // otherwise, further events cannot be sent to this gameobject if the user looks away
        InputManager.Instance.OverrideFocusedObject = gameObject;
    }

    /// <summary>
    /// Called every frame during the navigation gesture
    /// Sets the scroll amount
    /// </summary>
    /// <param name="eventData">The data of this event</param>
    public void OnNavigationUpdated(NavigationEventData eventData)
    {
        // scroll based on the amount that the user drags up or down
        // the dragging gesture controls the scrolling speed
        scrollController.Scroll(- speed * eventData.NormalizedOffset.y);
    }
}
