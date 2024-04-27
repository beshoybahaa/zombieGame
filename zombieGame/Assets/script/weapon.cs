using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float shootingDelay=2f;
    public float spreadIntensity=2;
    public Camera playerCamera;
    public bool isShooting, readyToShoot;
    public bool allowReset=true;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;


    private void Awake()
    {
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // make the ready to shoot in awak func equal true
        isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (isShooting && readyToShoot)
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // instantite the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        bullet.transform.forward = shootingDirection;

        // shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        // destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset){
            Invoke("ResetShot",shootingDelay);
            allowReset = false;
        }

    }

    private void ResetShot(){
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit)){
            // hitting something
            targetPoint = hit.point;
        }else{
            //shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x,y,0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifeTime)
    {
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bullet);
    }
}
