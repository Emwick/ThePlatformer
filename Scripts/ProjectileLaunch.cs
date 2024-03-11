using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
   public GameObject projectilePrefab;
   public Transform launchPoint;

   public float shootTime;
   public float shootCounter;


   void Start()
   {
    shootCounter = shootTime;
   }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && shootCounter <= 0)
        {
            Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            shootCounter = shootTime;
        }

        shootCounter -= Time.deltaTime;
    }
}
