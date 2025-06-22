using UnityEngine;
using DG.Tweening;
using TMPro;

public class WallLine : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI tooLightText;
    private float fadeDuration = 0.5f;
    private float visibleDuration = 0.5f;
    private Tween blinkTween;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // 기존 트윈 중지
        blinkTween?.Kill();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // 깜빡이며 나타나기
        blinkTween = canvasGroup.DOFade(1f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        blinkTween?.Kill();

        canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    public void DestroyWall()
    {
        tooLightText.text = "Heavy enough to go on";

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        Sequence seq = DOTween.Sequence();

        // Fade In
        seq.Append(canvasGroup.DOFade(1f, fadeDuration));
        seq.AppendCallback(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });

        // 유지 시간
        seq.AppendInterval(visibleDuration);

        // Fade Out
        seq.Append(canvasGroup.DOFade(0f, fadeDuration));
        seq.AppendCallback(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        });
    }

}
