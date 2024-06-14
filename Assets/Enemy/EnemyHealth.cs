using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5;
    int currentHealth = 0;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHitPoint;
        enemy = GetComponent<Enemy>();

    }
    private void OnEnable()
    {
        currentHealth = maxHitPoint;

    }

    private void OnParticleCollision(GameObject other)
    {
        ProccessHit();
    }

    private void ProccessHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            enemy.RewardGold();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
