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

    private Animator animator;

    private float jellyfishDamage = 10f;
    private float jellyfishDamage_dash = 20f;

    protected override void Start()
    {
        base.Start();
        oxygenDamage = jellyfishDamage;
        player = GameManager.Instance.Player.transform;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (isMoving || isAttacking) yield return null;
            isMoving = true;

            // �ִϸ��̼� ����: �̵� ��
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsStopped", false);

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            bool canAttack = Time.time >= lastAttackTime + attackCooldown;

            if (distanceToPlayer < detectRange && canAttack)
            {
                StartCoroutine(AttackRoutine());
                yield break;
            }

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
        lastAttackTime = Time.time;

        // �ִϸ��̼� ����: ���� (���� ����)
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsStopped", true);

        float timer = 0f;
        while (timer < aimDuration)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            timer += Time.deltaTime;
            yield return null;
        }

        // ���� ���� (Stop �ִϸ��̼� ���� ��)
        yield return transform.DOShakePosition(shakeDuration, 0.1f, 20, 90, false, false)
            .SetEase(Ease.Linear).WaitForCompletion();

        // Attack Ʈ���� �ߵ� �� Attack �� Stop(�ڵ� ��ȯ)
        animator.SetTrigger("Attack");

        Vector3 rushTarget = player.position;
        Vector2 rushDir = (rushTarget - transform.position).normalized;
        float rushDistance = 10f;
        Vector3 finalTarget = transform.position + (Vector3)(rushDir * rushDistance);

        // ���� ���� ���� ����
        canDealDamage = true;
        oxygenDamage = jellyfishDamage_dash;
        transform.DOMove(finalTarget, 1f / rushSpeed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                canDealDamage = false;
                oxygenDamage = jellyfishDamage;
            });

        // ���� �� Stop �ִϸ��̼� 1�� ����
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsStopped", true);
        animator.SetBool("IsMoving", false);

        // �ٽ� ����
        isAttacking = false;
        StartCoroutine(MoveRoutine());
    }


}
