using UnityEngine;

public class Fish : BaseMonster
{
    public float moveSpeed = 2f;
    public float moveRange = 3f;

    [Header("Attack Settings")]
    public float fishDamage = 10f;
    public float attackCooldown = 2f;

    private Vector3 startPos;
    private bool movingRight = true;

    private float lastAttackTime = -Mathf.Infinity;
    public bool canDealDamage = true;

    protected override void Start()
    {
        base.Start();
        oxygenDamage = fishDamage;
        startPos = transform.position;
    }

    private void Update()
    {
        float dir = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * dir);

        if (Mathf.Abs(transform.position.x - startPos.x) >= moveRange)
        {
            movingRight = !movingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        // 공격 쿨타임 체크
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            canDealDamage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDealDamage && collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer(fishDamage);
            canDealDamage = false;
            lastAttackTime = Time.time;
        }
    }
}
