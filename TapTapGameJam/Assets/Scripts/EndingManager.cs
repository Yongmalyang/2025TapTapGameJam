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
            SceneManager.LoadScene("Start"); // ���ϴ� �� �̸�
        }).SetUpdate(true); // timeScale�� 0�̾ �����
    }

}
