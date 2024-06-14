using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectTileParticle;
    [SerializeField] float range = 15f;
    Transform target;
    // Start is called before the first frame update
    //void Start()
    //{
    //    target = FindObjectOfType<Enemy>().transform;
    //}



    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        AimWeapon();
    }

    void FindClosestEnemy()
    {

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance( 
                transform.position,
                enemy.transform.position);
            if(targetDistance < maxDistance )
            {
                maxDistance = targetDistance;
                closestTarget = enemy.transform;
            }


        }
        target = closestTarget;

    }

    void AimWeapon()
    {
        weapon.LookAt(target);
        float distance = Vector3.Distance(transform.position,target.position);
        if(distance < range )
        {
            Atack(true);
        } else
        {
            Atack(false);

        }

    }

    void Atack(bool enable)
    {
        //projectTileParticle.enableEmission = enable;
        var emissionModule = projectTileParticle.emission;
        emissionModule.enabled = enable;

    }
}
