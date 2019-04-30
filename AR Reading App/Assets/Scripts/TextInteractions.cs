using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractions : MonoBehaviour, INavigationHandler
{
    public float speed = 1.5f;

    ScrollController scrollController;

    private void Awake()
    {
        scrollController = GetComponentInChildren<ScrollController>();
    }

    public void OnNavigationCanceled(NavigationEventData eventData)
    {
        InputManager.Instance.OverrideFocusedObject = null;
    }

    public void OnNavigationCompleted(NavigationEventData eventData)
    {
        InputManager.Instance.OverrideFocusedObject = null;
    }

    public void OnNavigationStarted(NavigationEventData eventData)
    {
        InputManager.Instance.OverrideFocusedObject = gameObject;
    }

    public void OnNavigationUpdated(NavigationEventData eventData)
    {
        scrollController.Scroll(- speed * eventData.NormalizedOffset.y);
    }
}
