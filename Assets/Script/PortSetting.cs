using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortSetting : MonoBehaviour
{
    public string myPortKey;
    public InputField inputField;
    public bool Camera = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Camera)
        {
            inputField.text = PlayerPrefs.GetInt(myPortKey+"INT", 0).ToString();
        }
        else
        {
            inputField.text = PlayerPrefs.GetString(myPortKey, "COM");
        }
    }
    public void setPort()
    {
        if (Camera)
        {
            string sBuf = inputField.text;
            if (sBuf != "")
            {
                int num = int.Parse(sBuf);
                Debug.Log("num;" + num);

                WebCamDevice[] devices = WebCamTexture.devices;
                if (devices.Length > num)
                {
                    string camName = devices[num].name;
                    PlayerPrefs.SetInt(myPortKey + "INT", num);
                    PlayerPrefs.SetString(myPortKey, camName);
                    Debug.Log(myPortKey + ":" + camName);
                }
                else
                {
                    Debug.Log("”ÍˆÍŠO‚ÌƒJƒƒ‰‚ğw’è‚µ‚Ä‚¢‚Ü‚·");
                }
            }
        }
        else
        {
            PlayerPrefs.SetString(myPortKey, inputField.text);
        }
        Debug.Log(inputField.text);
    }
}
