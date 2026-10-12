using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
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
    }
    public void Fire()
    {
        if (!isActiveAndEnabled || Time.time < NextFireTime)
            return;

        NextFireTime = Time.time + FireCoolDown;
        Vector3 origin = Muzzle.position;
        // Aim at the crosshair while keeping the beam attached to the muzzle.
        UnityEngine.Camera aimCamera = Camera.main;
        Ray aimRay = aimCamera != null
            ? aimCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))
            : new Ray(origin, Muzzle.forward);
        Vector3 target = aimRay.GetPoint(Range);
        if (TryHit(aimRay, Range, out RaycastHit aimHit))
            target = aimHit.point;
        Vector3 direction = (target - origin).normalized;
        Vector3 endPoint = origin + direction * Range;

        if (TryHit(new Ray(origin, direction), Range, out RaycastHit hit))
        {
            endPoint = hit.point;
            Debug.Log($"LASER HIT: {hit.collider.name}", hit.collider);
        }

        Beam.SetPosition(0, origin);
        Beam.SetPosition(1, endPoint);
        Beam.enabled = true;
        BeamHideTime = Time.time + BeamDuration;
    }

    private bool TryHit(Ray ray, float distance, out RaycastHit nearest)
    {
        nearest = default;
        bool found = false;
        foreach (RaycastHit hit in Physics.RaycastAll(ray, distance, HitMask,
                     QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.IsChildOf(transform.root))
                continue;
            if (!found || hit.distance < nearest.distance)
            {
                nearest = hit;
                found = true;
            }
        }
        return found;
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
            if (Beam != null)
                Beam.enabled = false;
        }
}

