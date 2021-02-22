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
    string RecentKey;
    string nowKey;
    [SerializeField] Image wImage;
    [SerializeField] Image sImage;
    [SerializeField] Image dImage;
    [SerializeField] Image aImage;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
        StartCoroutine(sendRoombaData());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            virtualWallActive = 25;
        } 
    }

    IEnumerator sendRoombaData()
    {
        //Debug.Log(c);
        c++;

        if (c % 12 == 0) {
            serialHandler.Write("v");
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
        if ((Input.GetKey(KeyCode.UpArrow)|| nowKey == "w") && virtualWallActive<=0)
        {
            NowKeyColor("w");
            if (RecentKey!="w"){
                serialHandler.Write("w");
            }
            RecentKey = "w";
            text.text = "前進";
        }
        else if (Input.GetKey(KeyCode.RightArrow)|| nowKey == "d")
        {
            NowKeyColor("d");
            if (RecentKey!="d"){
                serialHandler.Write("d");
            }
            RecentKey = "d";
            text.text = "右転";
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || nowKey == "a")
        {
            NowKeyColor("a");
            if (RecentKey != "a"){
                serialHandler.Write("a");
            }
            RecentKey = "a";
            text.text = "左転";
        }
        else if (Input.GetKey(KeyCode.DownArrow)|| nowKey == "s")
        {
            NowKeyColor("s");
            if (RecentKey != "s"){
                serialHandler.Write("s");
            }
            RecentKey = "s";
            text.text = "後退";
        }
        else
        {
            NowKeyColor("n");
            if (RecentKey != "n"){
                serialHandler.Write("n");
            }
            RecentKey = "n";
            text.text = "停止";
        }
        

        StartCoroutine(sendRoombaData());
    }

    public void SetNowKey(string k)
    {
        nowKey = k;
        NowKeyColor(k);
    }
    public void NowKeyColor(string k)
    {
        wImage.color = Color.white;
        sImage.color = Color.white;
        dImage.color = Color.white;
        aImage.color = Color.white;
        switch (k)
        {
            case "w":
                wImage.color = Color.green;
                break;
            case "s":
                sImage.color = Color.green;
                break;
            case "d":
                dImage.color = Color.green;
                break;
            case "a":
                aImage.color = Color.green;
                break;
            default:
                break;

        }
    }

    void OnDataReceived(string message)
    {
        Debug.Log("Received"+ message);
        virtualWallActive = 20;

        var data = message.Split(
                new string[] { "\t" }, System.StringSplitOptions.None);
       
        if (data.Length < 2) return; // ここではLengthは1なので、特に何も記述しない。

        try
        {
            if(data[0] == "v")
            {
                virtualWallActive = 25;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}