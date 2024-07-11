using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;
    Bank bank;
    TargetLocator targetLocator;


    private void Start()
    {

        StartCoroutine(Build());
    }
    public bool CreateTower(Tower tower, Vector3 position)
    {
        //if()
        bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {

            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false;

    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {

            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child.transform)
            {
                child.gameObject.SetActive(false);

            }
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandChild in child.transform)
            {
                child.gameObject.SetActive(true);
            }

        }
    }
}
