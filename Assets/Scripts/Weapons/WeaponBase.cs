using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public abstract class WeaponBase : MonoBehaviour
{
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    
}
