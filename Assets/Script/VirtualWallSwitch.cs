using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualWallSwitch : MonoBehaviour
{
    public SerialHandler serialHandler;
    // Start is called before the first frame update
    void Start()
    {
        GameObject s = GameObject.Find("Serial Handler2");
        if (s != null)
        {
            serialHandler = s.GetComponent<SerialHandler>();
            Debug.Log("Serial2Start");
        }
    }
    public void VWSwitch(int i)
    {
        serialHandler.Write(i.ToString() + "\n");
        Debug.Log("sent:"+i);
    }
}
