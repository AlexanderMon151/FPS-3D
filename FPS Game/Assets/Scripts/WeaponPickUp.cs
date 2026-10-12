using UnityEngine;
using static FpsPlayer;

public class WeaponPickUp : MonoBehaviour
{

    public Weapon WhatWeaponAmI;

    private void OnTriggerEnter(Collider other)
    {
        FpsPlayer p = other.GetComponentInParent<FpsPlayer>();
        if (p != null)
        {

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
