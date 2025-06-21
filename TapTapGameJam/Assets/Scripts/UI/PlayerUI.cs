using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void UpdateOxygenUI(int oxyValue)
    {
        float sliderValue = oxyValue / GameManager.Instance.maxOxygen;
        slider.value += sliderValue;
        Debug.Log("UI updated by" + sliderValue);

        //여기다가 이펙트 숫자도 띄우는 코드 넣기, dotween
    }
}
