using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Webカメラ
public class WebCam : MonoBehaviour
{
    private static int INPUT_SIZE = 256;
    private static int FPS = 30;
    public int CamNum = 0;
    public string CamName;

    // UI
    RawImage rawImage;
    WebCamTexture webCamTexture;

    // スタート時に呼ばれる
    void Start () 
    {
        CamNum = PlayerPrefs.GetInt(CamName, 0);

        WebCamDevice[] devices = WebCamTexture.devices;
        // Webカメラの開始
        this.rawImage = GetComponent<RawImage>();
       // this.webCamTexture = new WebCamTexture(INPUT_SIZE, INPUT_SIZE, FPS);
        this.webCamTexture = new WebCamTexture(devices[CamNum].name);
        Debug.Log(devices[CamNum].name);
        this.rawImage.texture = this.webCamTexture;
        this.webCamTexture.Play();
    }

    public void closeCam() {
        this.webCamTexture.Stop();
    }
}