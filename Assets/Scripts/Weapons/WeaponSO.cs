using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/BaseWeapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public int damage;

}
