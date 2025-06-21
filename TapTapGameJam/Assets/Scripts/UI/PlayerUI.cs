using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image warning;         // 경고 느낌표 이미지
    [SerializeField] private float warningOffset = 0.1f; // 플레이어 기준 거리

    [SerializeField]
    private Slider slider;

    public void UpdateOxygenUI(float curOxyValue)
    {
        float sliderValue = curOxyValue / GameManager.Instance.maxOxygen;
        slider.value = sliderValue;
    }

    public void GiveWarning(GameObject target)
    {
        if (warning == null || target == null || GameManager.Instance?.Player == null)
            return;

        // 1. 위치 계산: 플레이어 → 타겟 방향으로 0.1f 만큼 떨어진 위치
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 targetPos = target.transform.position;

        Vector3 dir = (targetPos - playerPos).normalized;
        Vector3 warningPos = playerPos + dir * warningOffset;

        // 2. 위치 적용 (2D 환경, Overlay 모드이므로 바로 위치 설정)
        warning.rectTransform.position = warningPos;

        // 3. 불투명도 초기화
        Color color = warning.color;
        color.a = 1f;
        warning.color = color;

        // 4. 깜빡이는 연출 (점점 빠르게 → 마지막 투명화)
        Sequence seq = DOTween.Sequence();

        float totalDuration = 2f;
        float currentTime = 0f;
        float blinkInterval = 0.4f;

        while (currentTime + blinkInterval * 2f <= totalDuration)
        {
            seq.Append(warning.DOFade(0f, blinkInterval));
            seq.Append(warning.DOFade(1f, blinkInterval));
            currentTime += blinkInterval * 2f;
            blinkInterval *= 0.7f;
        }

        // 5. 마지막엔 서서히 사라지기
        seq.Append(warning.DOFade(0f, 0.3f));
    }
}
