using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image warning;         // ��� ����ǥ �̹���
    [SerializeField] private float warningOffset = 0.1f; // �÷��̾� ���� �Ÿ�

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

        // 1. ��ġ ���: �÷��̾� �� Ÿ�� �������� 0.1f ��ŭ ������ ��ġ
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 targetPos = target.transform.position;

        Vector3 dir = (targetPos - playerPos).normalized;
        Vector3 warningPos = playerPos + dir * warningOffset;

        // 2. ��ġ ���� (2D ȯ��, Overlay ����̹Ƿ� �ٷ� ��ġ ����)
        warning.rectTransform.position = warningPos;

        // 3. ������ �ʱ�ȭ
        Color color = warning.color;
        color.a = 1f;
        warning.color = color;

        // 4. �����̴� ���� (���� ������ �� ������ ����ȭ)
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

        // 5. �������� ������ �������
        seq.Append(warning.DOFade(0f, 0.3f));
    }
}
