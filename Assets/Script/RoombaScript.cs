using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaScript : MonoBehaviour
{
    public Text text;
    public SerialHandler serialHandler;
    int c = 0;
    int virtualWallActive = 0;
    public GameObject virtualWallText;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
        StartCoroutine(sendRoombaData());
    }

    IEnumerator sendRoombaData()
    {
        //Debug.Log(c);
        c++;

        if (c % 12 == 0) {
            serialHandler.Write("v");
            Debug.Log("WallCheck");
        }

        if (virtualWallActive > 0)
        {
            virtualWallText.SetActive(true);
            virtualWallActive--;
        }
        else
        {
            virtualWallText.SetActive(false);
        }
        yield return new WaitForSeconds(0.05f);
        if (Input.GetKey(KeyCode.UpArrow) && virtualWallActive<=0)
        {
            serialHandler.Write("w");
            text.text = "↑";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            serialHandler.Write("d");
            text.text = "↷";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            serialHandler.Write("a");
            text.text = "↶";
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            serialHandler.Write("s");
            text.text = "↓";
        }
        else
        {
            text.text = "停";
        }
        

        StartCoroutine(sendRoombaData());
    }

    void OnDataReceived(string message)
    {
        Debug.Log("Received"+ message);
        virtualWallActive = 25;

        var data = message.Split(
                new string[] { "\t" }, System.StringSplitOptions.None);
       
        if (data.Length < 2) return; // ここではLengthは1なので、特に何も記述しない。

        try
        {
            if(data[0] == "v")
            {
                virtualWallActive = 100;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}