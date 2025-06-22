using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Player; // 여기다 Player 오브젝트 드래그
    public Camera mainCamera;
    public Spawner spawner;
    public MainUI mainUI;
    public StageResetter resetter;
    public AudioManager audioManager;

    public float oxygenAmount;
    public float maxOxygen;
    public float oxygenDropRate;

    [SerializeField]
    private GameOverUI gameOver;
    [SerializeField]
    private StageClearUI stageClear;

    public float slowDuration = 1.5f;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    public int curStageNum;
    public int curPlayerWeight = 0;
    public List<int> goalWeight = new List<int>() { 20, 30, 50, 100 };

    [SerializeField]
    private GameObject BGGroup;
    private List<float> BGPos = new List<float>() { -45, -15, 15, 45 };

    private float playerAndCameraInitPos = 7f;
    private float camInitZoom = 7f;

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
        curStageNum = 0;
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
        audioManager.Fail();
        ClearStage();
    }

    public void StageClear()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, slowDuration)
            .SetEase(Ease.InQuad)
            .SetUpdate(true) // Time.timeScale에도 적용되도록
            .OnComplete(() => {
                stageClear.ShowStageClearPanel(curStageNum);
            });
        audioManager.Clear();
        ClearStage();
    }

    // 지금까지 했던거 다 지우기
    public void ClearStage()
    {
        spawner.DestroyAllInScene();
        resetter.ClearStageMonsters(curStageNum);
    }

    public void ResetStage()
    {
        spawner.isSpawn = true;
        Time.timeScale = 1f;
        curPlayerWeight = 0;

        mainUI.Init();
        resetter.Reset();
    }

    // 재시작
    public void ReplayCurrentStage()
    {
        Debug.Log("재시작");
        oxygenAmount = maxOxygen;
        Player.GetComponent<Player>().DestroyAllArmWeight();
        ResetStage();

        Player.transform.DOMove(new Vector3(0f, playerAndCameraInitPos, 0f), 1f)
            .SetEase(Ease.InOutSine);
        mainCamera.DOOrthoSize(camInitZoom, slowDuration)
            .SetEase(Ease.InOutSine);
    }

    // 넘어가기
    public void MoveOnToNextStage()
    {
        Debug.Log("넘어가기");

        curStageNum++;

        mainCamera.GetComponent<MainCamera>().isFollowingPlayer = false;
        mainCamera.gameObject.transform.position = new Vector3(0f, playerAndCameraInitPos, -10f);
        Player.transform.position = new Vector3(0f, playerAndCameraInitPos, 0f);
        mainUI.gameObject.SetActive(false);

        Sequence seq = DOTween.Sequence();

        seq.Append(mainCamera.DOOrthoSize(10f, slowDuration)
            .SetEase(Ease.InOutSine))
            .SetUpdate(true);

        seq.Append(BGGroup.transform.DOMoveY(BGPos[curStageNum], 2f)
            .SetEase(Ease.InOutSine))
            .SetUpdate(true);

        seq.Append(mainCamera.DOOrthoSize(camInitZoom, slowDuration)
            .SetEase(Ease.InOutSine))
            .SetUpdate(true);

        seq.OnComplete(() =>
        {
            mainCamera.GetComponent<MainCamera>().isFollowingPlayer = true;
            mainUI.gameObject.SetActive(true);
            ResetStage();
        });
        if(curStageNum>=2)audioManager.DiveIntoDeepWaters();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Start");
    }
}