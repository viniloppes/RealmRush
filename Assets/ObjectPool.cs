using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0.1f, 30f)] float waitTime = 1f;
    [SerializeField][Range(0, 50)] int poolSize = 5;

    GameObject[] arrayPool;

    private void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaceEnemy());
    }

    void PopulatePool()
    {
        arrayPool = new GameObject[poolSize];
        Debug.Log(arrayPool.Length);

        for (int i = 0; i < arrayPool.Length; i++)
        {
            arrayPool[i] = Instantiate(enemyPrefab, transform);
            Debug.Log(i + " - " +arrayPool[i]);
            arrayPool[i].SetActive(false);
        }
    }
    void EnableObjectInPool()
    {
        for (int i = 0; i < arrayPool.Length; i++)
        {
            if (arrayPool[i].activeInHierarchy == false)
            {
                arrayPool[i].SetActive(true);
                return;
            }
        }
    }
    IEnumerator PlaceEnemy()
    {
        while (true)
        {
            //Instantiate(gameObject, transform);
            EnableObjectInPool();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
