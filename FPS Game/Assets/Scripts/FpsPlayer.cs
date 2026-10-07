using UnityEngine;
using UnityEngine.InputSystem;

public class FpsPlayer : MonoBehaviour
{

    public float MouseSense, Pitch, Yaw, MoveSpeed;
    public GameObject Camera;
    public Transform View;
    public Weapon CurrentWeapon = Weapon.None;
    public BulletGun BG;
    public LaserGun LG;
    public Rigidbody RB;
    public float JumpHeight = 1.5f;
    public GameObject InventoryPanel;
    public bool LaserGunPickedUp, BulletGunPickedUp;
    void Look()
    {
        Yaw += Input.GetAxis("Mouse X") * MouseSense;
        Pitch += Input.GetAxis("Mouse Y") * MouseSense;
        Pitch = Mathf.Clamp(Pitch, -85f, 85f);
        View.localRotation = Quaternion.Euler(-Pitch, Yaw, 0f);
    }
    void Move()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) input.z += 1f;
        if (Input.GetKey(KeyCode.S)) input.z -= 1f;
        if (Input.GetKey(KeyCode.D)) input.x += 1f;
        if (Input.GetKey(KeyCode.A)) input.x -= 1f;

        input = Vector3.ClampMagnitude(input, 1f);
        Vector3 direction = Quaternion.Euler(0f, Yaw, 0f) * input;
        transform.position += direction * MoveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            if (CurrentWeapon == Weapon.BulletGun)
                BG.Shoot();
            if (CurrentWeapon == Weapon.LaserGun)
                LG.Fire();
            else
                return;
        }
    }

    void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (RB.linearVelocity.y > 0.1f)
            return;

        if (!RB.SweepTest(Vector3.down, out RaycastHit hit, 0.1f, 
            QueryTriggerInteraction.Ignore) || hit.normal.y < 0.65f)
            return;

        Vector3 velocity = RB.linearVelocity;
        velocity.y = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * JumpHeight);
        RB.linearVelocity = velocity;
    }
    void ToggleInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        InventoryPanel.SetActive(InventoryPanel.activeInHierarchy);
    }

    void ToggleWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!BulletGunPickedUp && !LaserGunPickedUp)
                return;
            if (BulletGunPickedUp && LaserGunPickedUp)
            {
                if (CurrentWeapon == Weapon.BulletGun)
                {
                    CurrentWeapon = Weapon.LaserGun;
                    return;
                }
                if(CurrentWeapon == Weapon.LaserGun)
                {
                    CurrentWeapon = Weapon.BulletGun;
                    return;
                }
            }
            EquipWeapon();
        }
    }

    public void EquipWeapon()
    {
        if(CurrentWeapon == Weapon.BulletGun)
        {
            LG.gameObject.SetActive(false);
            BG.gameObject.SetActive(true);
            return;
        }
        if(CurrentWeapon == Weapon.LaserGun)
        {
            LG.gameObject.SetActive(true);
            BG.gameObject.SetActive(false);
            return;

        }
    }
    void  Controls()
    {
        Look();
        Shoot();
        Move();
        Jump();
        ToggleInventory();
        ToggleWeapon();
        EquipWeapon();
    }

    private void Update()
    {
        Controls();
    }

    public enum Weapon
    {
        BulletGun, LaserGun, None
    }
}

