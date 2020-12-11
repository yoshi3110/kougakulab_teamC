using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualWallSwitch : MonoBehaviour
{
    public SerialHandler serialHandler;
    // Start is called before the first frame update
   
    public void VWSwitch(int i)
    {
        serialHandler.Write(i.ToString() + "\n");
        Debug.Log("sent:"+i);
    }
}
