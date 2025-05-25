using UnityEngine;
using UnityEngine.Splines;

public class EnemySplinePatrol : MonoBehaviour, IEnemyMovement
{

    private SplineContainer _spline;
    private float time = 0f;
    private float speed = 1f;

    public EnemySplinePatrol(SplineContainer spline)
    {
        this._spline = spline;
    }
    public void Move(Transform enemy)
    {
        time = Mathf.Clamp01(time + Time.deltaTime * speed);
        enemy.position = _spline.EvaluatePosition(time);
    }
}
