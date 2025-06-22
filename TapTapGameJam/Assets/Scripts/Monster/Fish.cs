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
        group = GetComponentInParent<FishGroup>();
    }

    private void Update()
    {
        // ���� ��Ÿ�� üũ
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            canDealDamage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDealDamage && collision.gameObject.CompareTag("Player"))
        {
            if (!GameManager.Instance.tutorialManager.isTutoImageShown[3]) GameManager.Instance.tutorialManager.ShowTutorialImage(3);

            AttackPlayer(fishDamage);
            canDealDamage = false;
            lastAttackTime = Time.time;
            group.PauseAfterCollision();
        }
    }
}
