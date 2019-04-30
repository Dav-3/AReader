using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.WebCam;

public class HoloCameraController : Singleton<HoloCameraController>
{
    public float intervalInSeconds = 10f;
    public event EventHandler PhotoChanged;

    private PhotoCapture capturer;
    private CameraParameters cameraParams;
    private bool takingPhoto = false;

    public Texture2D CurrentPhoto { get; private set; }

    void Start()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        CurrentPhoto = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject)
        {
            capturer = captureObject;
            cameraParams = new CameraParameters();
            cameraParams.hologramOpacity = 0.0f;
            cameraParams.cameraResolutionWidth = cameraResolution.width;
            cameraParams.cameraResolutionHeight = cameraResolution.height;
            cameraParams.pixelFormat = CapturePixelFormat.BGRA32;

            // once created: take photos periodically
            InvokeRepeating("TakePhoto", 1f, intervalInSeconds);
        }
        );
    }

    private void TakePhoto()
    {
        if (capturer == null || takingPhoto)
        {
            return;
        }

        takingPhoto = true;

        // start the camera
        capturer.StartPhotoModeAsync(cameraParams, delegate (PhotoCapture.PhotoCaptureResult result)
        {
            // take picture
            capturer.TakePhotoAsync(OnPhotoCaptured);
        });
    }

    /// <summary>
    /// Called when a photo was taken
    /// </summary>
    /// <param name="result"></param>
    /// <param name="photoCaptureFrame"></param>
    private void OnPhotoCaptured(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // copy to texture
        photoCaptureFrame.UploadImageDataToTexture(CurrentPhoto);

        // decativate camera
        capturer.StopPhotoModeAsync(OnPhotoModeStoppped);
    }

    private void OnPhotoModeStoppped(PhotoCapture.PhotoCaptureResult result)
    {
        takingPhoto = false;
        OnPhotoChanged(EventArgs.Empty);
    }

    private void OnPhotoChanged(EventArgs e)
    {
        EventHandler handler = PhotoChanged;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // release photo capture resource
        capturer.Dispose();
        capturer = null;
    }
}
