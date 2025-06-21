using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weightText;
    private string backPart;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        backPart = " / " + GameManager.Instance.goalWeight[GameManager.Instance.curStageNum].ToString();
        weightText.text = "0" + backPart;
    }

    public void UpdateUIText(int num)
    {
        weightText.text = num.ToString() + backPart;
    }
}
