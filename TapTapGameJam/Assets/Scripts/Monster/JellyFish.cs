using UnityEngine;
using DG.Tweening;
using System.Collections;

public class JellyFish : BaseMonster
{
    public float moveDistance = 2f;
    public float moveDuration = 1.5f;
    public float waitTime = 0.5f;

    private bool isMoving = false;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (isMoving) yield return null;
            isMoving = true;

            // 1. 랜덤 방향 생성
            Vector2 moveDir = Random.insideUnitCircle.normalized;

            // 2. 회전: 위쪽(Vector2.up)을 moveDir로 돌리는 각도 계산
            float angle = Vector2.SignedAngle(Vector2.up, moveDir);
            transform.DORotate(new Vector3(0f, 0f, angle), 0.5f).SetEase(Ease.InOutSine);

            // 3. 이동 목표 위치
            Vector3 targetPos = transform.position + (Vector3)(moveDir * moveDistance);

            // 4. 이동
            yield return transform.DOMove(targetPos, moveDuration).SetEase(Ease.InOutSine).WaitForCompletion();

            // 5. 대기
            yield return new WaitForSeconds(waitTime);

            isMoving = false;
        }
    }
}
