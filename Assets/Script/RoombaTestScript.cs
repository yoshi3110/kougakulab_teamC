using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaTestScript : MonoBehaviour
{
    public Text text;
    public SerialHandler serialHandler;
    int c = 0;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
        StartCoroutine(sendRoombaData());
    }

    IEnumerator sendRoombaData()
    {
        //Debug.Log(c);
        //c++;
        yield return new WaitForSeconds(0.05f);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            serialHandler.Write("w");
            text.text = "↑";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            serialHandler.Write("d");
            text.text = "→";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            serialHandler.Write("a");
            text.text = "←";
        }
        else
        {
            text.text = "停";
        }

        StartCoroutine(sendRoombaData());
    }

    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { "\t" }, System.StringSplitOptions.None);
        Debug.Log(message);
        if (data.Length < 2) return; // ここではLengthは1なので、特に何も記述しない。

        try
        {

        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}