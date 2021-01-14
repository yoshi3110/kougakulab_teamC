using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void sendPassword()
    {
        int i = checkPassWord(passField.text);
        if (i >= 0)
        {
            if(subScenes[i].opened==false){
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
}
