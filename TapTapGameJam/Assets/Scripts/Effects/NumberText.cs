using UnityEngine;
using TMPro;
using DG.Tweening;

public class NumberText : MonoBehaviour
{
    public float floatDuration = 1f;      // 전체 이펙트 시간
    public float moveYAmount = 1f;        // 얼마나 위로 떠오를지

    private TextMeshProUGUI textMesh; 
    private Vector3 originalScale;


    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;

        Color startColor = textMesh.color;
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        transform.localScale = Vector3.zero;

        // 스케일 애니메이션: 커졌다가 작아짐
        transform.DOScale(originalScale * 1.3f, 0.15f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                transform.DOScale(originalScale, 0.15f).SetEase(Ease.InOutSine);
            });

        // 위로 이동
        transform.DOMoveY(transform.position.y + moveYAmount, floatDuration).SetEase(Ease.OutQuad);

        // 페이드 아웃
        textMesh.DOFade(0f, floatDuration).SetEase(Ease.InQuad);

        // 다 끝나면 오브젝트 파괴
        Destroy(gameObject, floatDuration);
    }

    public void SetText(string value)
    {
        if (textMesh == null) textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = value;
    }
}
