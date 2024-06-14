using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int goldReward = 25;
    [SerializeField] int goldSteal = 25;
    // Start is called before the first frame update
    Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if(bank == null) { return;}
        bank.Deposit(goldReward);
    }
    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldSteal);
    }
}
