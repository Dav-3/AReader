using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Improves the contrast of the text against the background
/// Not used in the HoloLens version
/// </summary>
public class TextContrastController : MonoBehaviour
{

    Texture2D currentPhoto;
    Text textUI;

    void Start()
    {
        textUI = GetComponent<Text>();
        // register for the photo taken event
        HoloCameraController.Instance.PhotoChanged += NewPhotoTaken;
    }

    /// <summary>
    /// Called if a new photo was taken
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">Event arguments</param>
    private void NewPhotoTaken(object sender, EventArgs e)
    {
        Debug.Log("Photo taken");
        currentPhoto = HoloCameraController.Instance.CurrentPhoto;
        Color32 averageColor = AverageColor(currentPhoto);
        float perceivedBrightness = 0.299f * averageColor.r + 0.587f * averageColor.g + 0.144f * averageColor.b;
        Debug.Log("perceived brightness: " + perceivedBrightness);
        if (perceivedBrightness > 186)
        {
            textUI.color = Color.black;
        }
        else
        {
            textUI.color = Color.white;
        }
    }

    /// <summary>
    /// Calculates the average color of an image
    /// </summary>
    /// <param name="tex">The image texture</param>
    /// <returns>The average color of tex</returns>
    private Color32 AverageColor(Texture2D tex)
    {
        Color32[] pixels = tex.GetPixels32();
        int totalNumberOfPixels = pixels.Length;
        int sumR = 0;
        int sumG = 0;
        int sumB = 0;

        for (int i=0;i<totalNumberOfPixels;i++)
        {
            sumR += pixels[i].r;
            sumG += pixels[i].g;
            sumB += pixels[i].b;
        }

        Color32 res = new Color32((byte) (sumR / totalNumberOfPixels), (byte) (sumG / totalNumberOfPixels), (byte) (sumB / totalNumberOfPixels), 0);
        return res;
    }

    /// <summary>
    /// Un-registers from the photo taken event
    /// </summary>
    private void OnDestroy()
    {
        HoloCameraController.Instance.PhotoChanged -= NewPhotoTaken;
    }
}
