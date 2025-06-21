using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFish : BaseMonster
{
    public Transform hidePosition;
    public Transform attackPosition;
    public Transform mouthPoint;
    public Collider2D mouthCollider;

    public float suctionRadius = 20f;
    public float suctionForce = 20f;
    public float suctionDuration = 5f;
    public float attackCooldown = 10f;

    private bool isAttacking = false;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        transform.position = hidePosition.position;
        transform.rotation = hidePosition.rotation;
        Debug.Log("�Ʊ�!!");
    }

    private void Update()
    {
        if (!isAttacking)
        {
            StartCoroutine(SuctionAttack());
        }
    }

    IEnumerator SuctionAttack()
    {
        isAttacking = true;

        animator.SetBool("isAttack", true);

        // 1. ���� (Ʈ�� �̵�)
        Sequence appearSeq = DOTween.Sequence();
        appearSeq.Append(transform.DOMove(attackPosition.position, 1f).SetEase(Ease.InOutSine));
        appearSeq.Join(transform.DORotateQuaternion(attackPosition.rotation, 1f).SetEase(Ease.InOutSine));
        yield return appearSeq.WaitForCompletion();

        float timer = 0f;
        while (timer < suctionDuration)
        {
            timer += Time.deltaTime;

            // 1. ArmWeight ������Ʈ ã��
            string[] armTags = { "ArmWeight1", "ArmWeight2", "ArmWeight3", "ArmWeight4" };
            List<GameObject> suctionTargets = new List<GameObject>();

            foreach (string tag in armTags)
            {
                suctionTargets.AddRange(GameObject.FindGameObjectsWithTag(tag));
            }

            // 2. ���Ƶ��̱� ����� �ִ� ���
            if (suctionTargets.Count > 0)
            {
                foreach (GameObject target in suctionTargets)
                {
                    float dist = Vector2.Distance(target.transform.position, mouthPoint.position);
                    if (dist < suctionRadius)
                    {
                        Vector2 dir = (mouthPoint.position - target.transform.position).normalized;
                        target.transform.position += (Vector3)(dir * suctionForce * Time.deltaTime);
                    }
                }
            }
            // 3. �ƹ��͵� ������ �÷��̾ üũ
            else
            {
                GameObject player = GameManager.Instance.Player;
                float playerDist = Vector2.Distance(player.transform.position, mouthPoint.position);
                if (playerDist < suctionRadius)
                {
                    Vector2 dir = (mouthPoint.position - player.transform.position).normalized;
                    player.transform.position += (Vector3)(dir * suctionForce * Time.deltaTime);

                    if (Mathf.FloorToInt(timer * 5f) != Mathf.FloorToInt((timer - Time.deltaTime) * 5f))
                    {
                        int damage = Random.Range(1, 5);
                        AttackPlayer(damage);
                    }
                }
            }

            yield return null;
        }

        animator.SetBool("isAttack", false);

        // 4. �ẹ (Ʈ�� ����)
        Sequence disappearSeq = DOTween.Sequence();
        disappearSeq.Append(transform.DOMove(hidePosition.position, 1f).SetEase(Ease.InOutSine));
        disappearSeq.Join(transform.DORotateQuaternion(hidePosition.rotation, 1f).SetEase(Ease.InOutSine));
        yield return disappearSeq.WaitForCompletion();

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }


}
