using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Gun")]
public class GunSO : ScriptableObject
{
    public int ammo;
    public int maxAmmo;
    public float recoilAmount;
    public float bulletForce;
    public AudioClip shootingSound;
    public enum GunType { pistol, sniper, shotgun, smg, assultrifle };
    public GunType gunType;

}
