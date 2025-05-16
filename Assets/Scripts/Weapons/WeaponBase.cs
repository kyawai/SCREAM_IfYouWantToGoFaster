using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public abstract class WeaponBase : MonoBehaviour
{
    //public WeaponStatsSO weaponStats;
    protected Rigidbody rb;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    
}
