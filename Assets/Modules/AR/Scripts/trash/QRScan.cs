using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using ZXing;

public class QRScan : MonoBehaviour
{
    public int m_captureWidth;
    public int m_captureHeight;

    // public Camera camera;
    
    public RenderTexture renderTexture; // render target, na który będziesz kopiował obraz z kamery
    public ARCameraBackground m_ARCameraBackground; // dostęp do tła AR kamery
    private Texture2D m_LastCameraTexture; // bufor na skopiowany obraz


    // string QrCode = string.Empty;
    // public TMP_Text output;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // StartCoroutine(Scan());
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // IEnumerator Scan()
    // {
    //     yield return new WaitForEndOfFrame();
    //     // int width = Screen.width;
    //     // if (m_captureHeight == null || m_captureWidth == null)
    //     // {
    //     //     m_captureHeight = 512;
    //     //     m_captureWidth = 512;
    //     // }

    //     RenderTexture rt = new RenderTexture(m_captureWidth, m_captureHeight, 24);
    //     camera.targetTexture = rt;

    //     var currentRT = RenderTexture.active;
    //     RenderTexture.active = rt;

    //     camera.Render();

    //     Texture2D image = new Texture2D(m_captureWidth, m_captureHeight);
    //     image.ReadPixels(new Rect(0, 0, m_captureWidth, m_captureHeight), 0, 0);
    //     image.Apply();

    //     camera.targetTexture = null;

    //     RenderTexture.active = currentRT;

    // }


    IEnumerator Test()
    {
        while(true)
        {
            yield return new WaitForSeconds(4);
            // Copy the camera background to a RenderTexture
            Graphics.Blit(null, renderTexture, m_ARCameraBackground.material);

            // Copy the RenderTexture from GPU to CPU
            var activeRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            if (m_LastCameraTexture == null)
                m_LastCameraTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, true);
            m_LastCameraTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            m_LastCameraTexture.Apply();
            RenderTexture.active = activeRenderTexture;

            // Write to file
            var bytes = m_LastCameraTexture.EncodeToPNG();
            var path = Application.persistentDataPath + "/camera_texture.png";
            File.WriteAllBytes(path, bytes);
        }
    }
}
