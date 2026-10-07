using Unity.VisualScripting;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Transform Muzzle;
    public LayerMask HitMask = Physics.DefaultRaycastLayers;
    public float Range = 100f;
    public float FireCoolDown = 0.2f;
    public float BeamDuration = 0.1f;

    private LineRenderer Beam;
    private float NextFireTime, BeamHideTime;

    private void Awake()
    {
        Beam = GetComponent<LineRenderer>();
        Beam.useWorldSpace = true;
        Beam.positionCount = 2;
        Beam.widthMultiplier = 0.03f;
        Beam.enabled = false;
        gameObject.SetActive(false);
    }
    public void Fire()
    {
        if (!isActiveAndEnabled || Time.time < NextFireTime)
            return;

        NextFireTime = Time.time + FireCoolDown;
        Vector3 origin = Muzzle.position;
        Vector3 direction = Muzzle.forward;
        Vector3 endPoint = origin + direction * Range;

        if (Physics.Raycast(origin, direction, out RaycastHit hit,
            Range, HitMask, QueryTriggerInteraction.Ignore))
        {
            endPoint = hit.point;
            Debug.Log($"LASER HIT: {hit.collider.name}", hit.collider);
        }

        Beam.SetPosition(9, origin);
        Beam.SetPosition(1, endPoint);
        Beam.enabled = true;
        BeamHideTime = Time.time + BeamDuration;
    }

    public void Aim()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 target = Physics.Raycast(ray, out RaycastHit hit, 1000f)
            ? hit.point
            : ray.GetPoint(1000f);

        transform.LookAt(target);
    }

    public void Update()
        {
            if (Beam.enabled && Time.time >= BeamHideTime)
                Beam.enabled = false;
        }

        private void OnDisable()
        {
            Beam.enabled = false;
        }
}

