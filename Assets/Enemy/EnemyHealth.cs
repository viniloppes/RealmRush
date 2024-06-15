using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5;

    [Tooltip("Add amount to maxHitPoint when enemy dies")]
    [SerializeField] int damageRamp = 1;
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
            maxHitPoint += damageRamp;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
