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
            inputField.text = PlayerPrefs.GetInt(myPortKey, 0).ToString();
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
            PlayerPrefs.SetInt(myPortKey, int.Parse(inputField.text));
        }
        else
        {
            PlayerPrefs.SetString(myPortKey, inputField.text);
        }
        Debug.Log(inputField.text);
    }
}
