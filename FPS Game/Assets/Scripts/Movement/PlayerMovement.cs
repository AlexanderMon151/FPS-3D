using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] 
public class PlayerMovement : MonoBehaviour
{

    public float Speed = 5f, JumpSpeed = 7f, gravity = -10f;
    private float ySpeed;
    public CharacterController CC;
    public PlayerInventoryInput Inputs;
    public PotionEffects Effects;

    public void Awake() => CC = GetComponent<CharacterController>(); 
    public void Update()
    {
        Inputs.SelectedItem();
        Inputs.UseItem();
        Move();
    }

    void Move()
    {
        var k = Keyboard.current;

        if (k == null)
            return;

        float x = (k.dKey.isPressed ? 1 : 0 - (k.aKey.isPressed ? 1 : 0));
        float z = (k.wKey.isPressed ? 1 : 0 - (k.sKey.isPressed ? 1 : 0));
        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f) * Speed * (Effects != null ? Effects.SpeedMultiplier : 1f);

        if (CC.isGrounded && ySpeed < 0f)
            ySpeed = -2f;
        if (CC.isGrounded && k.spaceKey.wasPressedThisFrame)
            ySpeed = JumpSpeed * (Effects != null ? Effects.JumpMultiplier : 1f);

        ySpeed += gravity * Time.deltaTime;
        move.y = ySpeed;
        CC.Move(move * Time.deltaTime);
    }
}
