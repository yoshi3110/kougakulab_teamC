using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CallButtonScript : MonoBehaviour
{
    public Text buttonText;
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    public void setName(string scene)
    {
        sceneName = scene;
        buttonText.text = sceneName;
    }

    public void addScene()
    {
        Application.LoadLevelAdditive(sceneName);
    }
}
