using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Player; // ����� Player ������Ʈ �巡��
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
            Destroy(gameObject); // �ߺ� ����
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
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

        // ������ �ִϸ��̼�: 0 �� 1�� �ٿ ����
        spawned.transform.localScale = Vector3.zero;
        spawned.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBounce); // <- ƨ����� ��Ÿ��
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
            .SetUpdate(true); // Time.timeScale���� ����ǵ���
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
            .SetUpdate(true) // Time.timeScale���� ����ǵ���
            .OnComplete(() => {
                stageClear.ShowStageClearPanel(curStageNum);
            });
        audioManager.Clear();
        ClearStage();
    }

    // ���ݱ��� �ߴ��� �� �����
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

    // �����
    public void ReplayCurrentStage()
    {
        Debug.Log("�����");
        oxygenAmount = maxOxygen;
        Player.GetComponent<Player>().DestroyAllArmWeight();
        ResetStage();

        Player.transform.DOMove(new Vector3(0f, playerAndCameraInitPos, 0f), 1f)
            .SetEase(Ease.InOutSine);
        mainCamera.DOOrthoSize(camInitZoom, slowDuration)
            .SetEase(Ease.InOutSine);
    }

    // �Ѿ��
    public void MoveOnToNextStage()
    {
        Debug.Log("�Ѿ��");

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