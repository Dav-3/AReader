using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test script which was used to get the camera image of the HoloLens
/// Not used in the final prototype
/// </summary>
public class CameraTester : MonoBehaviour
{

    Renderer rend;
    Texture2D tex;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (HoloCameraController.Instance != null && tex != HoloCameraController.Instance.CurrentPhoto && HoloCameraController.Instance.CurrentPhoto != null)
        {
            tex = HoloCameraController.Instance.CurrentPhoto;
            rend.material.SetTexture("_MainTex", tex);
        }
    }
}
