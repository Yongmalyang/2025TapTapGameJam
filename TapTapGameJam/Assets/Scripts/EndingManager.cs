using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

public class EndingManager : MonoBehaviour
{    void Start()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            SceneManager.LoadScene("Start"); // 원하는 씬 이름
        }).SetUpdate(true); // timeScale이 0이어도 실행됨
    }

}
