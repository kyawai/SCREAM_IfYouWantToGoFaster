using UnityEngine;
using UnityEngine.Splines;

public class EnemySplineMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private EnemySO _enemySO;
    private SplineAnimate _splineAnimate;
    private EnemyController _enemyController;


    public void Start()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
        _enemyController = GetComponent<EnemyController>();

        _splineAnimate.Completed += SplineAnimate_Completed;
    }

    private void SplineAnimate_Completed()
    {
        gameObject.transform.LookAt(GameObject.FindWithTag("Player").transform);
    }

    public void Move()
    {
        _splineAnimate.MaxSpeed = _enemySO.speed;
        _splineAnimate.Play();
    }

}
