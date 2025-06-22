using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip horrorBGM;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private List<AudioSource> UISoundSource;

    void Start()
    {
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => OnAnyButtonClicked(btn));
        }
    }
    

    private void OnAnyButtonClicked(Button clickedButton)
    {
        PressButton();
    }

    public void DiveIntoDeepWaters()
    {
        BGM.clip = horrorBGM;
        BGM.Play();
    }

    public void PressButton()
    {
        UISoundSource[0].Play();
    }

    public void GetWeight()
    {
        UISoundSource[1].Play();

    }

    public void Warning()
    {
        UISoundSource[2].Play();

    }

    public void Clear()
    {
        UISoundSource[3].Play();

    }

    public void Fail()
    {
        UISoundSource[4].Play();

    }

    public void HitByFish()
    {
        UISoundSource[5].Play();

    }


}
