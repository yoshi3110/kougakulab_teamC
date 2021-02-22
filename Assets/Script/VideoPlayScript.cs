using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoPlayScript : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    [SerializeField] GameObject panel;
    double vtime = 0;
    bool movieend = false;
    // Start is called before the first frame update
    void Start()
    {
        vtime = video.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.K))
        {
            PlayerPrefs.SetInt("Restart", 1);
            SceneManager.LoadScene("RoombaMain");
        }
      
    }

    public void VideoStart() {
        video.Play();
        panel.SetActive(false);
        Invoke("CallScene", (float)vtime+0.5f);
    }
    void CallScene() {
        PlayerPrefs.SetInt("Restart", 0);
        SceneManager.LoadScene("RoombaMain");
    }
}
