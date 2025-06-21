using UnityEngine;

public class Fish : BaseMonster
{
    [Header("Attack Settings")]
    public float fishDamage = 10f;
    public float attackCooldown = 2f;

    private float lastAttackTime = -Mathf.Infinity;
    public bool canDealDamage = true;

    private FishGroup group;

    protected override void Start()
    {
        base.Start();
        oxygenDamage = fishDamage;
        group = GetComponentInParent<FishGroup>();
    }

    private void Update()
    {
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
            group.PauseAfterCollision();
        }
    }
}
