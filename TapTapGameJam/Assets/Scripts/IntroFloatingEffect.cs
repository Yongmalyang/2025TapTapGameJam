using UnityEngine;
using DG.Tweening;

public class IntroFloatingEffect : MonoBehaviour
{
    [Header("움직임 설정")]
    public float floatRange = 0.5f;       // 위아래 총 이동량의 절반
    public float moveDuration = 1.5f;     // 위→아래 한 방향 시간
    public float startDelay = 0f;         // 시작 전 대기 시간

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
        StartCoroutine(StartFloatAfterDelay());
    }

    private System.Collections.IEnumerator StartFloatAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        // 위로 floatRange만큼 이동하고, 아래로 다시 돌아오는 yoyo 반복
        transform.DOLocalMoveY(originalPos.y + floatRange, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
