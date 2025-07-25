using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResetter : MonoBehaviour
{
    public List<GameObject> middleWalls;
    public List<GameObject> stageMonsters;
    public GameObject monsterGroupHolder;

    private int curStage;

    private float cur, max;

    private void Start()
    {
        curStage = GameManager.Instance.curStageNum;
        ResetWalls();
        ResetStageMonsters(GameManager.Instance.curStageNum);

    }

    public void Reset()
    {
        ResetWalls();
        ResetStageMonsters(GameManager.Instance.curStageNum);
    }

    public void CheckForMiddleWallBreak()
    {
        cur = (float)GameManager.Instance.curPlayerWeight;

        if (cur >= max * 0.6)
        {
            middleWalls[1].GetComponent<WallLine>().DestroyWall();
            //middleWalls[1].SetActive(false);
        }
        if(cur >= max * 0.3)
        {
            middleWalls[0].GetComponent<WallLine>().DestroyWall();
            //middleWalls[0].SetActive(false);
        }
    }

    public void ResetWalls()
    {
        cur = (float)GameManager.Instance.curPlayerWeight;
        max = (float)GameManager.Instance.goalWeight[curStage];

        for (int i=0; i<2; i++)
        {
            middleWalls[i].SetActive(true);
        }
    }

    public void ClearStageMonsters(int stageNum)
    {
        foreach (Transform child in monsterGroupHolder.transform)
        {
            Destroy(child.gameObject);
        }

    }

    public void ResetStageMonsters(int stageNum)
    {
        Instantiate(stageMonsters[stageNum], monsterGroupHolder.transform);
    }
}
