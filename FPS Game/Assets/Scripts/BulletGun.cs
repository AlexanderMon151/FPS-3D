using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour
{
   public Transform BulletPoint;
    public GameObject BulletPrefab;

    public List<GameObject> Bullets;
    public bool TestShoot;

    private void Awake()
    {
        PopulateBullets();
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
