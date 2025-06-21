using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private GameObject areaHolder;

    [SerializeField] private List<GameObject> itemPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> areaPrefab = new List<GameObject>();

    public bool isSpawn = true;
    [SerializeField] private float itemCoolTime = 5f;
    [SerializeField] private float areaCoolTime = 15f;

    private float itemTimer = 0f;
    private float areaTimer = 0f;

    private void Update()
    {
        if (!isSpawn) return;

        itemTimer += Time.deltaTime;
        areaTimer += Time.deltaTime;

        if (itemTimer >= itemCoolTime)
        {
            for(int i=0; i<5; i++)
            {
                SpawnRandomFromList(itemPrefab, 1);

            }
            itemTimer = 0f;
        }

        if (areaTimer >= areaCoolTime)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnRandomFromList(areaPrefab, 2);
            }
            areaTimer = 0f;
        }
    }

    private void SpawnRandomFromList(List<GameObject> prefabList, int type)
    {
        if (prefabList == null || prefabList.Count == 0) return;

        int randomIndex = Random.Range(0, prefabList.Count);
        GameObject prefab = prefabList[randomIndex];

        switch (type)
        {
            case 1: GameManager.Instance.SpawnObjectInBounds(prefab, itemHolder.transform); break;
            case 2: GameManager.Instance.SpawnObjectInBounds(prefab, areaHolder.transform); break;
        }
        
    }

    public void DestroyAllInScene()
    {
        isSpawn = false;

        foreach (Transform child in itemHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in areaHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
