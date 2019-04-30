using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapController : MonoBehaviour, IInputClickHandler
{
    [SerializeField] private Button snapButton;
    [SerializeField] private Transform snappingMessage;

    private float timeSinceEnabled = 0f;

    SolverSurfaceMagnetism magnetism;
    SolverRadialView radial;
    Button[] allButtons;

    private void Awake()
    {
        magnetism = GetComponent<SolverSurfaceMagnetism>();
        radial = GetComponent<SolverRadialView>();
    }

    public void EnableSnapping()
    {
        timeSinceEnabled = 0f;
        magnetism.enabled = true;
        radial.enabled = false;
        snappingMessage.gameObject.SetActive(true);
        snapButton.gameObject.SetActive(false);
        SetButtonStates(false);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (timeSinceEnabled > 1f)
        {
            magnetism.enabled = false;
            radial.enabled = true;
            snappingMessage.gameObject.SetActive(false);
            snapButton.gameObject.SetActive(true);
            SetButtonStates(true);
        }
    }

    private void Update()
    {
        timeSinceEnabled += Time.deltaTime;
    }

    private void SetButtonStates(bool state)
    {
        allButtons = GetComponentsInChildren<Button>(true);
        foreach (Button b in allButtons)
        {
            b.interactable = state;
        }
    }
}
