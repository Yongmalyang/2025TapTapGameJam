using UnityEngine;
using DG.Tweening;
using System.Collections;

public class JellyFish : BaseMonster
{
    [Header("Idle Movement")]
    public float moveDistance = 2f;
    public float moveDuration = 1.5f;
    public float waitTime = 0.5f;

    [Header("Attack Settings")]
    public float detectRange = 5f;
    public float aimDuration = 1f;
    public float shakeDuration = 0.5f;
    public float rushSpeed = 1f;
    public float attackCooldown = 3f;

    private float lastAttackTime = -Mathf.Infinity;

    private Transform player;
    private bool isAttacking = false;
    private bool isMoving = false;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (isMoving || isAttacking) yield return null;
            isMoving = true;

            // �÷��̾� �Ÿ� üũ
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            bool canAttack = Time.time >= lastAttackTime + attackCooldown;

            if (distanceToPlayer < detectRange && canAttack)
            {
                StartCoroutine(AttackRoutine());
                yield break;
            }

            // �Ϲ� ���� ��ƾ
            Vector2 moveDir = Random.insideUnitCircle.normalized;
            float angle = Vector2.SignedAngle(Vector2.up, moveDir);
            transform.DORotate(new Vector3(0f, 0f, angle), 0.5f).SetEase(Ease.InOutSine);

            Vector3 targetPos = transform.position + (Vector3)(moveDir * moveDistance);
            yield return transform.DOMove(targetPos, moveDuration).SetEase(Ease.InOutSine).WaitForCompletion();

            yield return new WaitForSeconds(waitTime);
            isMoving = false;
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        lastAttackTime = Time.time; // ��Ÿ�� ����

        // 1. ���� �ܰ� (1�� ���� ��� �÷��̾� �ٶ󺸱�)
        float timer = 0f;
        while (timer < aimDuration)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            timer += Time.deltaTime;
            yield return null;
        }

        // 2. �ε�ε� ���� (shake)
        yield return transform.DOShakePosition(shakeDuration, 0.1f, 20, 90, false, false)
            .SetEase(Ease.Linear).WaitForCompletion();

        // 3. ���� (�� �������� �÷��̾� ��ġ ����)
        Vector3 rushTarget = player.position;
        Vector2 rushDir = (rushTarget - transform.position).normalized;
        float rushDistance = 10f; // ����� �� �Ÿ� ����

        Vector3 finalTarget = transform.position + (Vector3)(rushDir * rushDistance);
        transform.DOMove(finalTarget, 1f / rushSpeed).SetEase(Ease.OutQuad);

        // ���� �� �ٽ� �̵� ��ƾ ����
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        StartCoroutine(MoveRoutine());
    }
}
