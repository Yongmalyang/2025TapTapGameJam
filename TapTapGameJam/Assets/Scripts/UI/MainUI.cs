using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private List<Sprite> stageUI;
    [SerializeField] private Image mainBG;
    private string backPart;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        int curStage = GameManager.Instance.curStageNum;

        backPart = " / " + GameManager.Instance.goalWeight[curStage].ToString() + "KG";
        weightText.text = "0" + backPart;

        mainBG.sprite = stageUI[curStage];
    }

    public void UpdateUIText(int num)
    {
        weightText.text = num.ToString() + backPart;
    }
}
