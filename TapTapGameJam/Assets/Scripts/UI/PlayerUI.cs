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

        //����ٰ� ����Ʈ ���ڵ� ���� �ڵ� �ֱ�, dotween
    }
}
