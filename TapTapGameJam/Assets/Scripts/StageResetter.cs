using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResetter : MonoBehaviour
{
    public List<GameObject> middleWalls;
    public List<GameObject> stageMonsters;

    private int curStage;

    private void Start()
    {
        curStage = GameManager.Instance.curStageNum;
    }

    public void Reset()
    {
        ResetWalls();
        ResetStageMonsters(GameManager.Instance.curStageNum);
    }

    public void CheckForMiddleWallBreak()
    {
        float cur = (float) GameManager.Instance.curPlayerWeight;
        float max = (float) GameManager.Instance.goalWeight[curStage];
        if(cur >= max * 0.75)
        {
            middleWalls[2].SetActive(false);
        }
        if(cur >= max * 0.5)
        {
            middleWalls[1].SetActive(false);
        }
        if(cur >= max * 0.25)
        {
            middleWalls[0].SetActive(false);
        }
    }

    public void ResetWalls()
    {
        for(int i=0; i<3; i++)
        {
            middleWalls[i].SetActive(true);
        }
    }

    public void ClearStageMonsters(int stageNum)
    {

    }

    public void ResetStageMonsters(int stageNum)
    {

    }
}
