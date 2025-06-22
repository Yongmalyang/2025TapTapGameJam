using UnityEngine;
using TMPro;
using DG.Tweening;

public class NumberText : MonoBehaviour
{
    public float floatDuration = 1f;      // ��ü ����Ʈ �ð�
    public float moveYAmount = 1f;        // �󸶳� ���� ��������

    private TextMeshProUGUI textMesh; 
    private Vector3 originalScale;

    public bool isDamage;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;

        if (isDamage) GameManager.Instance.audioManager.HitByFish();

        Color startColor = textMesh.color;
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        transform.localScale = Vector3.zero;
        
        // ������ �ִϸ��̼�: Ŀ���ٰ� �۾���
        transform.DOScale(originalScale * 1.3f, 0.15f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() => {
                transform.DOScale(originalScale, 0.15f)
                    .SetEase(Ease.InOutSine)
                    .SetUpdate(true);
            });

        // ���� �̵�
        transform.DOMoveY(transform.position.y + moveYAmount, floatDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true);

        // ���̵� �ƿ�
        textMesh.DOFade(0f, floatDuration)
            .SetEase(Ease.InQuad)
            .SetUpdate(true);

        // �� ������ ������Ʈ �ı�
        Destroy(gameObject, floatDuration);
    }
}
