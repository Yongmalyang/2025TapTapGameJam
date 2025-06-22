using UnityEngine;
using DG.Tweening;

public class IntroFloatingEffect : MonoBehaviour
{
    [Header("������ ����")]
    public float floatRange = 0.5f;       // ���Ʒ� �� �̵����� ����
    public float moveDuration = 1.5f;     // ����Ʒ� �� ���� �ð�
    public float startDelay = 0f;         // ���� �� ��� �ð�

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
        StartCoroutine(StartFloatAfterDelay());
    }

    private System.Collections.IEnumerator StartFloatAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        // ���� floatRange��ŭ �̵��ϰ�, �Ʒ��� �ٽ� ���ƿ��� yoyo �ݺ�
        transform.DOLocalMoveY(originalPos.y + floatRange, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
