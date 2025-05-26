using UnityEngine;
using UnityEngine.Events;

public class EnemyAlertController : MonoBehaviour 
{
    [SerializeField] private float _viewRadius = 100f;
    [SerializeField] private float _viewAngle = 60f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _obstacleMask;
    public GameObject explenationPoint;


    public UnityEvent<Transform> OnPlayerDetected;
    public UnityEvent OnPlayerLost;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        DetectTargets();
    }
    private void DetectTargets()
    {
        bool playerDetected = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _layerMask);

        foreach (Collider target in targetsInViewRadius)
        {
            Vector3 headPosition = target.transform.position;
            Vector3 directionToTarget = (headPosition - transform.position).normalized;

            float angleBetween = Vector3.Angle(transform.forward, directionToTarget);

            Debug.DrawRay(transform.position, directionToTarget * _viewRadius, Color.red, 0.5f);

            if (angleBetween < _viewAngle / 2f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, _viewRadius, _layerMask))
                {
                    if (hit.collider.tag == "Player")
                    {
                       Transform playerHead = hit.transform;
                       playerDetected = true;
                       OnPlayerDetected?.Invoke(playerHead);
                       return;
                    }
                }
            }
        }
        if (!playerDetected)
        {
            OnPlayerLost?.Invoke();
        }

    }

    public void EnemyDetected()
    {
        if (!explenationPoint.activeSelf)
        {
            explenationPoint.SetActive(true);
            gameObject.transform.LookAt(_player);
        }
    }

    public void EnemyDetectionLost()
    {
        if (explenationPoint.activeSelf)
        {
            explenationPoint.SetActive(false);
        }
    }
}
