using UnityEngine;
using static FpsPlayer;

public class WeaponPickUp : MonoBehaviour
{

    public Weapon WhatWeaponAmI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            FpsPlayer p = other.GetComponent<FpsPlayer>();

            if (p == null)
                return;

            if (WhatWeaponAmI == Weapon.BulletGun)
            {
                p.BulletGunPickedUp = true;
                p.CurrentWeapon = WhatWeaponAmI;
                p.EquipWeapon();
                Destroy(gameObject);
                return;
            }
            if (WhatWeaponAmI == Weapon.LaserGun)
            {
                p.LaserGunPickedUp = true;
                p.CurrentWeapon = WhatWeaponAmI;
                p.EquipWeapon();
                Destroy(gameObject);
                return;
            }
            
        }
    }
}
