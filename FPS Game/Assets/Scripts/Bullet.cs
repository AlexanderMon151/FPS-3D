using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float Timer;
    public float TimeLimit;

    private void OnEnable()
    {
        Timer = 0;
        Aim();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.transform.CompareTag("Player"))
        {
            Debug.Log("You Hit something!");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            Debug.Log("You Hit something!");
            gameObject.SetActive(false);
        }
    }
    public void Aim()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 target = Physics.Raycast(ray, out RaycastHit hit, 1000f)
            ? hit.point
            : ray.GetPoint(1000f);

        transform.LookAt(target);
    }

    void DeactivateTimer()
    {
        Timer += Time.deltaTime;

        if (Timer >= TimeLimit)
            gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Speed *  Time.deltaTime);
        DeactivateTimer();
    }
}
