using UnityEngine;

public class GunLaser : MonoBehaviour
{
    public bool _hasLaser;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _layerMask;
    public Transform laserPoint;

    private Quaternion _originalRot;

    private Transform _thisGun;
    private void Start()
    {
        _thisGun = this.gameObject.transform;
        if (_hasLaser)
        {
            _originalRot = _thisGun.localRotation;
            _lineRenderer.positionCount = 2;
            _lineRenderer.startWidth = 0.01f;
            _lineRenderer.endWidth = 0.01f;
        }
    }
    public void EnableLaser(bool activateLaser)
    {
        _hasLaser = activateLaser;
    }

    private void Update()
    {
        if (_hasLaser)
        {
            Ray ray = new Ray(laserPoint.position, laserPoint.forward);
            RaycastHit hit;
            Vector3 end;
            if (Physics.Raycast(ray, out hit, 100f, _layerMask))
            {
                end = hit.point;
            }
            else
            {
                end = laserPoint.position + laserPoint.forward * 100f;
            }
            _lineRenderer.SetPosition(0, laserPoint.position);

            _lineRenderer.SetPosition(1, end);
        }
    }
}
