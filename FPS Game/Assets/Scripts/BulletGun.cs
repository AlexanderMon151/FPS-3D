using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour
{
   public Transform BulletPoint;
    public GameObject BulletPrefab;
    public List<GameObject> Bullets;
    public bool TestShoot;

    public float ShootCoolDown = 0.2f;
    public float ShootDuration = 0.1f;

    private float NextShootTime, ShootHideTIme;

    private void Awake()
    {
        PopulateBullets();
        gameObject.SetActive(false);
    }

    void PopulateBullets()
    {
        Bullets = new List<GameObject>();

        for (int i = 0; i < 100; i++)
        {
            var t = Instantiate(BulletPrefab, BulletPoint.position, Quaternion.identity);
            Bullets.Add(t);
            t.SetActive(false);
        }
    }
    public void Shoot()
    {

        if (!isActiveAndEnabled || Time.time < NextShootTime)
            return;

        NextShootTime = Time.time + ShootCoolDown;


        foreach (GameObject bullet in Bullets)
        {
        if (!bullet.activeInHierarchy)
        {
        bullet.transform.position = BulletPoint.position;
        bullet.SetActive(true);
        Debug.Log("BANG BANG");
        break;
        }

        }
    }
}
