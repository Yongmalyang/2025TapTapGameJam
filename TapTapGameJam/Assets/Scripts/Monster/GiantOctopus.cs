using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GiantOctopus : BaseMonster
{
    [Header("Tentacle Attack Settings")]
    public float criticalChance = 0.05f;
    public int minNormalDamage = 1;
    public int maxNormalDamage = 4;
    public int criticalDamage = 50;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float animationLength;
    private Color originalColor;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장

        // 현재 재생 중인 애니메이션의 길이 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationLength = stateInfo.length;

        StartCoroutine(TentacleAttackRoutine());
    }

    IEnumerator TentacleAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(animationLength);

            float roll = Random.value;

            if (roll < criticalChance)
            {
                // 💥 크리티컬 연출 시작
                Debug.Log("💥 대왕문어 치명타!!!");

                animator.speed = 0f; // 애니메이션 멈춤

                // 흰색으로 변해감
                spriteRenderer.DOColor(Color.white, 0.5f);

                // 부들부들 떨기
                yield return transform.DOShakePosition(0.5f, 0.3f, 30, 90f, false, true).WaitForCompletion();

                // 데미지 주기
                AttackPlayer(criticalDamage);

                // 색상 원래대로 복구
                spriteRenderer.DOColor(originalColor, 0.5f);
                yield return new WaitForSeconds(0.5f);

                animator.speed = 1f; // 애니메이션 재개
            }
            else
            {
                // 🪼 일반 공격
                int damage = Random.Range(minNormalDamage, maxNormalDamage + 1);
                AttackPlayer(damage);
                Debug.Log($"문어다리 일반 공격: {damage} 데미지");
            }
        }
    }
}
