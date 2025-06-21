using UnityEngine;
using TMPro;
using DG.Tweening;

public class NumberText : MonoBehaviour
{
    public float floatDuration = 1f;      // ��ü ����Ʈ �ð�
    public float moveYAmount = 1f;        // �󸶳� ���� ��������

    private TextMeshProUGUI textMesh; 
    private Vector3 originalScale;


    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;

        Color startColor = textMesh.color;
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        transform.localScale = Vector3.zero;

        // ������ �ִϸ��̼�: Ŀ���ٰ� �۾���
        transform.DOScale(originalScale * 1.3f, 0.15f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                transform.DOScale(originalScale, 0.15f).SetEase(Ease.InOutSine);
            });

        // ���� �̵�
        transform.DOMoveY(transform.position.y + moveYAmount, floatDuration).SetEase(Ease.OutQuad);

        // ���̵� �ƿ�
        textMesh.DOFade(0f, floatDuration).SetEase(Ease.InQuad);

        // �� ������ ������Ʈ �ı�
        Destroy(gameObject, floatDuration);
    }

    public void SetText(string value)
    {
        if (textMesh == null) textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = value;
    }
}
