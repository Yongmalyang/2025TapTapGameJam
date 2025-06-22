using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject player;
    public Transform target;  // 목표 위치 B
    public float height = 2f; // 곡선 높이
    public float duration = 1.2f;

    public float floatRange = 0.5f;       // 위아래 총 이동량의 절반
    public float moveDuration = 1.5f;     // 위→아래 한 방향 시간
    public float startDelay = 0f;         // 시작 전 대기 시간

    void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        // 시작 위치와 목표 위치 계산
        Vector3 start = player.transform.position;
        Vector3 end = target.position;
        Vector3 mid = (start + end) / 2f;
        mid.y -= 2f; // 원하는 높이 조절

        Vector3[] path = new Vector3[] { start, mid, end };

        // 전체 시퀀스 만들기
        Sequence fullSequence = DOTween.Sequence();

        // 1. 경로 이동 + 스케일 증가
        fullSequence.Append(
            player.transform.DOPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)
        );
        fullSequence.Join(
            player.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), duration)
                .SetEase(Ease.InExpo)
        );

        // ✅ 1번 끝난 후 소팅 레이어 변경
        fullSequence.AppendCallback(() =>
        {
            var sr = player.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "Overlay"; // 원하는 레이어명
            }
        });

        // 2. 위아래 요요 애니메이션
        fullSequence.Append(
            player.transform.DOLocalMoveY(target.position.y + floatRange, moveDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(4, LoopType.Yoyo) // 무한 반복 대신 2회 반복 후 끝남
        );

        // 3. 일정 시간 후 씬 전환
        fullSequence.AppendCallback(() =>
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                SceneManager.LoadScene("Start");
            }).SetUpdate(true);
        });

    }

}
