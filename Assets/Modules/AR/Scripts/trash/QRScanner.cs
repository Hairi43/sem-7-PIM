using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using TMPro;

public class QRScanner : MonoBehaviour
{
    WebCamTexture webcamTexture;
    string QrCode = string.Empty;

    public TMP_Text output;

    void Start()
    {
        var renderer = GetComponent<RawImage>();
        // webcamTexture = new WebCamTexture(512, 512);
        webcamTexture = new WebCamTexture(256, 256);
        renderer.texture = webcamTexture;
        renderer.material.mainTexture = webcamTexture;
        StartCoroutine(GetQRCode());
    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            yield return new WaitForSeconds(2);
            try
            {
                Color32[] rotatedPixels = Rotate90(webcamTexture.GetPixels32(), webcamTexture.width, webcamTexture.height);
                // snap.SetPixels32(webcamTexture.GetPixels32());
                snap.SetPixels32(rotatedPixels);
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    output.text = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                        // output.text = QrCode;
                        break;
                    }
                }
                else
                {
                    output.text = "result is null";
                }
            }
            catch (Exception ex) 
            { 
                Debug.LogWarning(ex.Message); 
                output.text = ex.Message;
            }
            yield return null;
        }
        webcamTexture.Stop();
    }

    Color32[] Rotate90(Color32[] original, int width, int height)
    {
        Color32[] rotated = new Color32[original.Length];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                rotated[x * height + (height - y - 1)] = original[y * width + x];
            }
        }
        return rotated;
    }
    
    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        string text =QrCode;

        Vector2 pivotPoint = new Vector2(w / 2, h / 2);
        GUIUtility.RotateAroundPivot(90, pivotPoint);

        Rect rect = new Rect(pivotPoint.x, pivotPoint.y, w, h * 2 / 100);
        GUI.Label(rect, text, style);
    }
}
