using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[ExecuteInEditMode]
public class ApplyWeaponComponents : WeaponBase
{
    private void Awake()
    {
        //check if the objects have weapon specific components, if not, add them 
        if (GetComponent<XRGrabInteractable>() == null) 
        { 
            gameObject.AddComponent<XRGrabInteractable>(); 

        }
        if (GetComponent<Outline>() == null)
        {
            if (gameObject.GetComponent<GunLaser>()._hasLaser == true) return;
            gameObject.AddComponent<Outline>();
            Outline outline = GetComponent<Outline>();

            outline.OutlineColor = new Color(0.7122642f, 1f, 0.9864757f, 1f);
            outline.OutlineWidth = 8f;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }
}
