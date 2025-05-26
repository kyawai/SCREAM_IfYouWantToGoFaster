using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyController enemy);
    void UpdateState(EnemyController enemy);

    void ExitState(EnemyController enemy);
}

public class PatrolState : IEnemyState
{
    public void EnterState(EnemyController enemy)
    {
        if (enemy.GetMovement() == null)
        {
            Debug.LogError($"Enemy {enemy.name} has no movement script assigned!");
            return;
        }
    }

    public void UpdateState(EnemyController enemy) { enemy.GetMovement().Move(); }

    public void ExitState(EnemyController enemy)
    {

    }
}

public class AlertState : IEnemyState
{
    public void EnterState(EnemyController enemy) { enemy.GetEnemyAlertController().EnemyDetected(); }

    public void UpdateState(EnemyController enemy) { }

    public void ExitState(EnemyController enemy) { enemy.GetEnemyAlertController().EnemyDetectionLost(); }
}

public class AttackState : IEnemyState
{
    public void EnterState(EnemyController enemy) { Debug.Log("ENTER Attack"); }

    public void UpdateState(EnemyController enemy) { Debug.Log("ATTACK/Update state"); }
    public void ExitState(EnemyController enemy)
    {

    }
}
