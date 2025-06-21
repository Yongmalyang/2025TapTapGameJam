using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Player; // 여기다 Player 오브젝트 드래그
    public Camera mainCamera;
    public Spawner spawner;

    public float oxygenAmount;
    public float maxOxygen;
    public float oxygenDropRate;

    [SerializeField]
    private GameOver gameOver;

    public float slowDuration = 1.5f;

    [SerializeField]
    private Vector2 minBounds;
    [SerializeField]
    private Vector2 maxBounds;

    public int curStageNum;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
    }

    private void Start()
    {
        oxygenAmount = maxOxygen;
    }

    public void SpawnObjectInBounds(GameObject prefabToSpawn, Transform parent)
    {
        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = Random.Range(minBounds.y, maxBounds.y);
        Vector3 spawnPos = new Vector3(x, y, 0f);

        GameObject spawned = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, parent);
        Debug.Log(prefabToSpawn.ToString() + " spawned here: "+spawnPos);

        // 스케일 애니메이션: 0 → 1로 바운스 등장
        spawned.transform.localScale = Vector3.zero;
        spawned.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBounce); // <- 튕기듯이 나타남
    }

    public bool IsInsideBounds(GameObject obj)
    {
        Vector3 pos = obj.transform.position;

        return pos.x >= minBounds.x && pos.x <= maxBounds.x &&
               pos.y >= minBounds.y && pos.y <= maxBounds.y;
    }


    public void GameOver()
    {
        Debug.Log("game over");
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, slowDuration)
            .SetEase(Ease.InQuad)
            .SetUpdate(true); // Time.timeScale에도 적용되도록
        mainCamera.DOOrthoSize(1f, slowDuration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true)
            .OnComplete(() => {
                gameOver.ShowGameOverPanel();
            });
    }
}