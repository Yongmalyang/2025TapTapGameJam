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

            // 1. ���� ���� ����
            Vector2 moveDir = Random.insideUnitCircle.normalized;

            // 2. ȸ��: ����(Vector2.up)�� moveDir�� ������ ���� ���
            float angle = Vector2.SignedAngle(Vector2.up, moveDir);
            transform.DORotate(new Vector3(0f, 0f, angle), 0.5f).SetEase(Ease.InOutSine);

            // 3. �̵� ��ǥ ��ġ
            Vector3 targetPos = transform.position + (Vector3)(moveDir * moveDistance);

            // 4. �̵�
            yield return transform.DOMove(targetPos, moveDuration).SetEase(Ease.InOutSine).WaitForCompletion();

            // 5. ���
            yield return new WaitForSeconds(waitTime);

            isMoving = false;
        }
    }
}
