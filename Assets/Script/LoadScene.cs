using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sName;
    public void callScene()
    {
        SceneManager.LoadScene(sName);
    }
}