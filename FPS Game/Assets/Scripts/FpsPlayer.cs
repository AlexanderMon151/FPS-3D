using UnityEngine;

public class FpsPlayer : MonoBehaviour
{
    #region Variabler
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
    private bool IsCrouching;
    private Vector3 standingCameraPosition;

    public float crouchAmount = 0.5f;

    #endregion
    private void Start()
    {
        standingCameraPosition = Camera.transform.localPosition;

        if (InventoryPanel != null)
            InventoryPanel.SetActive(false);
        EquipWeapon();
    }
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
        if (Input.GetKeyDown(KeyCode.I) && InventoryPanel != null)
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
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
        if (BG != null)
            BG.gameObject.SetActive(CurrentWeapon == Weapon.BulletGun);
        if (LG != null)
            LG.gameObject.SetActive(CurrentWeapon == Weapon.LaserGun);
    }

    public void PlayerCrouch()
    {
        if (!Input.GetKeyDown(KeyCode.C))
            return;

        IsCrouching = !IsCrouching;

        if(IsCrouching)
        {
            Camera.transform.localPosition =
                standingCameraPosition - Vector3.up * crouchAmount;
            return;
        }
        else
        {
            Camera.transform.localPosition = standingCameraPosition;
            return;
        }
    }
    void  Controls()
    {
        ToggleInventory();
        if (InventoryPanel != null && InventoryPanel.activeSelf)
            return;
        Look();
        Shoot();
        Move();
        Jump();
        ToggleWeapon();
        EquipWeapon();
        PlayerCrouch();
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

