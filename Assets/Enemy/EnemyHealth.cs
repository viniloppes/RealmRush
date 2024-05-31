using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5;
    int currentHealth = 0;
    // Start is called before the first frame update
    void Start()
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
            Destroy(gameObject);
        }
    }
}
