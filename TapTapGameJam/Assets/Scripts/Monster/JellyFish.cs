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
    private bool isRushing = false;

    private Animator animator;

    private float jellyfishDamage = 10f;
    private float jellyfishDamage_dash = 20f;
    public bool canDealDamage = true;

    public GameObject jellyFishPrefab; // �ڱ� �ڽ� ������ (Inspector���� ����)

    protected override void Start()
    {
        base.Start();
        oxygenDamage = jellyfishDamage;
        player = GameManager.Instance.Player.transform;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
        StartCoroutine(CheckBoundsRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (isMoving || isAttacking) yield return null;
            isMoving = true;

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

        // ��Ÿ�� �ʱ�ȭ + �� ���� ������ X
        canDealDamage = false;
        lastAttackTime = Time.time;

        yield return transform.DOShakePosition(shakeDuration, 0.1f, 20, 90, false, false)
            .SetEase(Ease.Linear).WaitForCompletion();

        animator.SetTrigger("Attack");

        Vector3 rushTarget = player.position;
        Vector2 rushDir = (rushTarget - transform.position).normalized;
        float rushDistance = 10f;
        Vector3 finalTarget = transform.position + (Vector3)(rushDir * rushDistance);

        // ���� ���� ����
        isRushing = true;
        canDealDamage = true;
        oxygenDamage = jellyfishDamage_dash;

        transform.DOMove(finalTarget, 1f / rushSpeed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                isRushing = false;
                canDealDamage = false;
                oxygenDamage = jellyfishDamage;
            });

        yield return new WaitForSeconds(1f);
        animator.SetBool("IsStopped", true);
        animator.SetBool("IsMoving", false);

        isAttacking = false;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator CheckBoundsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // 5�ʸ��� �˻�

            if (!GameManager.Instance.IsInsideBounds(gameObject))
            {
                Debug.Log("������!!!");
                GameManager.Instance.SpawnObjectInBounds(jellyFishPrefab);
                Destroy(gameObject);
                yield break; // �ڱ� �ı��Ǹ� ���� ����
            }
            Debug.Log("���� �ȳ���");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (canDealDamage)
        {
            AttackPlayer(oxygenDamage);

            if (isRushing)
            {
                // ���� �� ������ �ָ� ��Ÿ�� ����
                lastAttackTime = Time.time;
            }

            canDealDamage = false;
        }
        else
        {
            // �⺻ ���� �浹�� �� ��Ÿ���� �������� �˻�
            if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer(jellyfishDamage);
                lastAttackTime = Time.time;
                canDealDamage = false;
            }
        }
    }
}
