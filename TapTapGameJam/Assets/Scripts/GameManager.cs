using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Player; // ����� Player ������Ʈ �巡��
    public Camera mainCamera;

    public float oxygenAmount;
    public float maxOxygen;
    public float oxygenDropRate;

    [SerializeField]
    private GameOver gameOver;

    public float slowDuration = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �ߺ� ����
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
    }

    private void Start()
    {
        oxygenAmount = maxOxygen;
    }

    public void GameOver()
    {
        Debug.Log("game over");
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, slowDuration)
            .SetEase(Ease.InQuad)
            .SetUpdate(true); // Time.timeScale���� ����ǵ���
        mainCamera.DOOrthoSize(2.5f, slowDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => {
                gameOver.ShowGameOverPanel();
            });
    }
}