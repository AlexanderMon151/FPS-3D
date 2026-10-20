using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Transform Player;
    public float MoveSpeed;

    void Move()
    {
        Vector3 targetPos = Player.position;
        targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, 
            targetPos, MoveSpeed * Time.deltaTime); ;
    }

    void Look()
    {
        Vector3 direction = Player.position - transform.position;
        direction.y = 0f;

        if(direction.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(direction);
    }


    void Update()
    {
        if (Player == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, 
            Player.position, MoveSpeed * Time.deltaTime);
        Move();
        Look();
    }
}
