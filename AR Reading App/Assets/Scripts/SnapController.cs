using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the snapping and radial view of the attached solvers
/// It switches between the two solvers in order to realize snapping or mid-air floating text
/// </summary>
public class SnapController : MonoBehaviour, IInputClickHandler
{
    // necessary references to UI components so that they can be hidden or activated at the right moments
    [SerializeField] private Button snapButton;
    [SerializeField] private Transform snappingMessage;

    // we need the time since we last enabled the snapping because clicking a button is also recognized as tapping on the window
    private float timeSinceEnabled = 0f;

    SolverSurfaceMagnetism magnetism;
    SolverRadialView radial;
    Button[] allButtons;

    /// <summary>
    /// Get references to the solver components
    /// </summary>
    private void Awake()
    {
        magnetism = GetComponent<SolverSurfaceMagnetism>();
        radial = GetComponent<SolverRadialView>();
    }

    /// <summary>
    /// Enables the snapping so that the gameobject sticks to the walls
    /// </summary>
    public void EnableSnapping()
    {
        timeSinceEnabled = 0f;
        magnetism.enabled = true;
        radial.enabled = false; // needs to be disabled so that the text is oriented correctly to be flat on the wall
        snappingMessage.gameObject.SetActive(true);
        snapButton.gameObject.SetActive(false);
        // disable all buttons so that the user does not accidentaly press one when disabling snapping again
        SetButtonStates(false);
    }

    /// <summary>
    /// Called if the user clicks on the whole window
    /// This is the necessary action to deactivate the snapping again
    /// It is also the only action which the user can do while the snapping is active because the window moves exactly with the gaze
    /// Therefore no button can be reached
    /// </summary>
    /// <param name="eventData">The event data of the click</param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        // only disable the snapping again after 1 second
        // this avoids activation and immediate deactivation because click the button to activate snapping is also registered as tapping on the window
        if (timeSinceEnabled > 1f)
        {
            magnetism.enabled = false;
            radial.enabled = true;
            snappingMessage.gameObject.SetActive(false);
            snapButton.gameObject.SetActive(true);
            // re-enable the button interactivity
            SetButtonStates(true);
        }
    }

    /// <summary>
    /// Cummulates the time since snapping was enabled
    /// </summary>
    private void Update()
    {
        timeSinceEnabled += Time.deltaTime;
    }

    /// <summary>
    /// Sets the interactable state of all buttons
    /// </summary>
    /// <param name="state"></param>
    private void SetButtonStates(bool state)
    {
        allButtons = GetComponentsInChildren<Button>(true);
        foreach (Button b in allButtons)
        {
            b.interactable = state;
        }
    }
}
