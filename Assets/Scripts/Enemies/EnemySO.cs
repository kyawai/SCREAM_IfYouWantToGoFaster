using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "AI/EnemyData")]
public class EnemySO : ScriptableObject
{
    public float speed;
    public float sightRange;
    public float audioDetectionRange;

    public enum EnemyType { normal,blind,tank,boss};
    public EnemyType enemyType;
}
