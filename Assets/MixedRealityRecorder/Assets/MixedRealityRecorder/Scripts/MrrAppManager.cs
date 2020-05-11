﻿using UnityEngine;
using MRR.Settings;
using MRR.Video;
using System.Collections;

namespace MRR.Controller
{

    public class MrrAppManager : MonoBehaviour
    {

        public MrrUiController uiController;

        public MrrVirtualCamera virtualCamera;

        public Material matForegroundMask;
        private RenderTexture foregroundMaskTexture;

        void Start()
        {
            // TMP
            //ApplyApplicationSettings();

            // screen size
            Vector2Int screenSize = new Vector2Int(1920, 1080);

            foregroundMaskTexture = new RenderTexture(screenSize.x, screenSize.y, 0, RenderTextureFormat.ARGBHalf);

            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            yield return new WaitForSeconds(1.0f);

            // set foreground shader depth texture
            matForegroundMask.SetTexture("_DepthTex", virtualCamera.GetDepthTexture());

            // assign the textures to the ui raw image component
            uiController.SetScreenVirtualCamera(virtualCamera.GetColorTexture());
            uiController.SetScreenForegroundMask(foregroundMaskTexture);
        }

        public void UpdateForegroundMask()
        {
            // we only use this method to receive our processed foreground mask texture
            // all shader properties are already set in Initialize() method
            matForegroundMask.SetFloat("_HmdDepth", virtualCamera.GetHmdDepth());
            Graphics.Blit(virtualCamera.GetDepthTexture(), foregroundMaskTexture, matForegroundMask);
        }

        private void ApplyApplicationSettings()
        {
            // is there a better way to lock the camera rendering framerate?
            // most vr sdks set the application framerate to 75 or 90 
            // we cant control our virtual camera framerate with this settings
            Application.targetFrameRate = (int)virtualCamera.GetCameraSettings().framerate;
        }
    }
}