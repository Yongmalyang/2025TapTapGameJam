using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public AudioSource ButtonClick;

    public void OnClickStart()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("PlayMap");
    }

    public void OnClickContinue()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("PlayMap");
    }

    public void OnClickQuit()
    {
        ButtonClick.Play();
        Application.Quit();
    }
}
