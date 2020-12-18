using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortSetting : MonoBehaviour
{
    public string myPortKey;
    public InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = PlayerPrefs.GetString(myPortKey, "COM");
    }
    public void setPort()
    {
        PlayerPrefs.SetString(myPortKey, inputField.text);
        Debug.Log(inputField.text);
    }
}
