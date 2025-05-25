using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyController enemy);
    void UpdateState(EnemyController enemy);
}

public class PatrolState : IEnemyState
{
    public void EnterState(EnemyController enemy) { Debug.Log("START Patrolling"); }
    public void UpdateState(EnemyController enemy) { enemy.GetMovement()?.Move(enemy.transform); }
}

public class AlertState : IEnemyState
{
    public void EnterState(EnemyController enemy) { Debug.Log("ENTER Alert"); }
    public void UpdateState(EnemyController enemy) { Debug.Log("Investigate noise/Update state"); }
}

public class AttackState : IEnemyState
{
    public void EnterState(EnemyController enemy) { Debug.Log("ENTER Attack"); }
    public void UpdateState(EnemyController enemy) { Debug.Log("ATTACK/Update state"); }
}
