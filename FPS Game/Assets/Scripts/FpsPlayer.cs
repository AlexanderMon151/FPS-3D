using UnityEngine;
using UnityEngine.InputSystem;

public class FpsPlayer : MonoBehaviour
{

    public float MouseSense, Pitch, Yaw, MoveSpeed;
    public GameObject Camera;
    public Transform View;
    public Weapon CurrentWeapon = Weapon.BulletGun;
    public BulletGun BG;
    void Look()
    {
        Yaw += Input.GetAxis("Mouse X") * MouseSense;
        Pitch += Input.GetAxis("Mouse Y") * MouseSense;
        Pitch = Mathf.Clamp(Pitch, -85f, 85f);
        View.localRotation = Quaternion.Euler(-Pitch, Yaw, 0f);
    }

    void Move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 
            0f, Input.GetAxisRaw("Vertical"));

        input = Vector3.ClampMagnitude(input, 1f);
        Quaternion heading = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        transform.position += heading * input * MoveSpeed * Time.deltaTime;
    }

    void Move2()
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
            {
                BG.Shoot();
            }
            else
            {

            }
        }
    }

    private void Update()
    {
        Look();
        Shoot();
        Move2();
    }

    public enum Weapon
    {
        BulletGun, LaserGun
    }
}

