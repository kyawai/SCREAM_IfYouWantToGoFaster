using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private Transform[] _patrolPoints;
    private int _currentPoint = 0;
    private float _speed = 1f;

    public EnemyPatrol(Transform[] patrolPoints, float speed)
    {
        this._patrolPoints = patrolPoints;
        this._speed = speed;
    }

    public void Move(Transform enemy)
    {
        Debug.Log("MOVE");
        if (Vector3.Distance(enemy.position, _patrolPoints[_currentPoint].position) < 0.5f)
        {
            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
        }
        enemy.position = Vector3.MoveTowards(enemy.position, _patrolPoints[_currentPoint].position, _speed);
    }
}
