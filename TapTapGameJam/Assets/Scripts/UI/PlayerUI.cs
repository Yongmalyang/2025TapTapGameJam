using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void UpdateOxygenUI(float curOxyValue)
    {
        float sliderValue = curOxyValue / GameManager.Instance.maxOxygen;
        slider.value = sliderValue;
    }
}
