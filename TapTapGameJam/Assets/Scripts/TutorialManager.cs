using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> TutoImages;
    public List<bool> isTutoImageShown = new List<bool>() { false, false, false, false, false, false, false};

    public void ShowTutorialImage(int index, System.Action onComplete = null)
    {
        Time.timeScale = 0f;
        TutoImages[index].SetActive(true);

        DOVirtual.DelayedCall(3f, () =>
        {
            Time.timeScale = 1f;
            TutoImages[index].SetActive(false);
            isTutoImageShown[index] = true;

            // �Ϸ�Ǹ� ���� �۾� ����
            onComplete?.Invoke();

        }).SetUpdate(true);
    }
}
