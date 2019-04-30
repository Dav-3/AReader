using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextContrastController : MonoBehaviour
{

    Texture2D currentPhoto;
    Text textUI;

    // Use this for initialization
    void Start()
    {
        textUI = GetComponent<Text>();
        HoloCameraController.Instance.PhotoChanged += NewPhotoTaken;
    }

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

    private void OnDestroy()
    {
        HoloCameraController.Instance.PhotoChanged -= NewPhotoTaken;
    }
}
