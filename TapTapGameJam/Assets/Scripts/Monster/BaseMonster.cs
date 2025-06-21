using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseMonster : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float maxHP = 100f;
    protected float currentHP;

    [Header("Damage Effect")]
    public GameObject damageEffectPrefab; // TextMeshPro 텍스트 이펙트 프리팹

    public float oxygenDamage;
    public bool canDealDamage = true;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void AttackPlayer(float amount)
    {
        // 최솟값 0 유지
        GameManager.Instance.oxygenAmount -= oxygenDamage;
        // UI 반영
        GameManager.Instance.Player.GetComponent<Player>().UI.UpdateOxygenUI(GameManager.Instance.oxygenAmount);
        // 0보다 작아지면 게임오버
        if (GameManager.Instance.oxygenAmount <= 0) { GameManager.Instance.GameOver(); return; }
        ShowDamageEffect(GameManager.Instance.Player.transform.position, amount);
    }

    protected virtual void ShowDamageEffect(Vector3 position, float amount)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1f), 0f);
        GameObject textObj = Instantiate(damageEffectPrefab, position + randomOffset, Quaternion.identity, GameManager.Instance.Player.GetComponent<Player>().UI.transform);
        var textMesh = textObj.GetComponent<TextMeshProUGUI>();
        textMesh.text = amount.ToString();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDealDamage && collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer(oxygenDamage);
        }
    }
}
