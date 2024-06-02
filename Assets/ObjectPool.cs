using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] float waitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaceEnemy());
    }

    IEnumerator PlaceEnemy()
    {
        while (true)
        {
            Instantiate(gameObject, transform);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
