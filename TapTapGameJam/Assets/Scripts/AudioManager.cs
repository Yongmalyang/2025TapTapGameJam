using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip horrorBGM;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private List<AudioSource> UISoundSource;

    public void DiveIntoDeepWaters()
    {
        BGM.clip = horrorBGM;
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
