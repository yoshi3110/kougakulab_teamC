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
    [System.Serializable]
    public class subScene
    {
        public string sceneName;
        public string password;
        public bool opened = false;
    }


    // Start is called before the first frame update
    void Start()
    {

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
                GameObject newButton = Instantiate(callButtonObj);
                newButton.transform.SetParent(scrollViewContent);
                newButton.SetActive(true);
                CallButtonScript callButtonScript = newButton.GetComponent<CallButtonScript>();
                callButtonScript.setName(subScenes[i].sceneName);
            }
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
}
