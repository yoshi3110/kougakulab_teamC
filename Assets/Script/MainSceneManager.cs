using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] float timeRimitDefault = 1500;//秒
    float nowTime = 0;
    [SerializeField] Text batteryText;
    bool batteryEmpty = false;
    bool gameClear = false;
    public InputField passField;
    public subScene[] subScenes;
    public GameObject callButtonObj;
    public Transform scrollViewContent;
    public RawImage subCamImage;
    AudioSource audioSource;
    [SerializeField] AudioClip passClear;
    [SerializeField] AudioClip buttonSE;

    [System.Serializable]
    public class subScene
    {
        public string sceneName;
        public string password;
        public bool opened = false;
        public GameObject specialButton;//ここがnullでないときは指定されたボタンを有効にする
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(PlayerPrefs.GetInt("Restart",0) == 0)
        {
            DataReset();
            PlayerPrefs.SetInt("Restart", 0);
        }
        else
        {
            ReStart();
            PlayerPrefs.SetInt("Restart", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(batteryEmpty == false && gameClear == false)nowTime += Time.deltaTime;
        if (nowTime > timeRimitDefault) {
            nowTime = timeRimitDefault;
            batteryEmpty = true;
            Application.LoadLevelAdditive("BadEnd");
        }
        PlayerPrefs.SetFloat("Time", nowTime);
        batteryText.text = "バッテリー残量：" + (((timeRimitDefault - nowTime) / timeRimitDefault) * 100f).ToString("N2") + "%";

    }

    void DataReset()
    {
        PlayerPrefs.SetFloat("Time", 0);
        for (int i = 0; i < subScenes.Length; i++)
        {
            PlayerPrefs.SetInt("SubSceneKey" + i, 0);
        }
    }

    void ReStart()
    {
        nowTime = PlayerPrefs.GetFloat("Time", 0);
        for (int i = 0; i < subScenes.Length; i++)
        {
            bool b = false;
            if (PlayerPrefs.GetInt("SubSceneKey" + i, 0) == 1) b = true;
            if (b)
            {
                if (subScenes[i].specialButton != null)
                {
                    subScenes[i].specialButton.transform.SetParent(scrollViewContent);
                    subScenes[i].specialButton.SetActive(true);
                }
                else
                {
                    GameObject newButton = Instantiate(callButtonObj);
                    newButton.transform.SetParent(scrollViewContent);
                    newButton.GetComponent<RectTransform>().localScale = Vector3.one;
                    newButton.SetActive(true);
                    CallButtonScript callButtonScript = newButton.GetComponent<CallButtonScript>();
                    callButtonScript.setName(subScenes[i].sceneName);
                }
            }
        }
    }
    public void sendPassword()
    {
        int i = checkPassWord(passField.text);
        if (i >= 0)
        {
            if(subScenes[i].opened==false){
                PlayerPrefs.SetInt("SubSceneKey" + i, 1);
                subScenes[i].opened = true;
                audioSource.PlayOneShot(passClear);
                if (subScenes[i].specialButton != null)
                {
                    subScenes[i].specialButton.transform.SetParent(scrollViewContent);
                    subScenes[i].specialButton.SetActive(true);
                }
                else
                {
                    GameObject newButton = Instantiate(callButtonObj);
                    newButton.transform.SetParent(scrollViewContent);
                    newButton.GetComponent<RectTransform>().localScale = Vector3.one;
                
                    newButton.SetActive(true);
                    CallButtonScript callButtonScript = newButton.GetComponent<CallButtonScript>();
                    callButtonScript.setName(subScenes[i].sceneName);
                }
            }
            else
            {
                audioSource.PlayOneShot(buttonSE);
            }
        }
        else
        {
            audioSource.PlayOneShot(buttonSE);
        }
    }

    int checkPassWord(string inputStr)
    {
        Debug.Log("check:" + inputStr);
        for (int i = 0; i < subScenes.Length; i++)
        {
            if (inputStr.Equals(subScenes[i].password))
            {
                return i;
            }
        }
        return -1;
    }

    public void CamChange() {
        audioSource.PlayOneShot(buttonSE);
        subCamImage.enabled = !subCamImage.IsActive();
    }
    public void SetGameClear() {
        gameClear = true;
    }
}
