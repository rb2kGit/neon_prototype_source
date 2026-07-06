using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "abilityBase", menuName = "Scriptable Objects/abilityBase")]
public class abilityBase : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;
    [SerializeField] private int color;
    public int redCount;
    public int yellowCount;
    public int blueCount;
    public int tieredAbilityCode; 
    public GameObject playerObject;
    public GameObject firePoint;

    //T2 ability variables.
    public float abilityAmmo;
    public float maxAbilityAmmo;
    public int spriteCode;
    public int uiSpriteCode;


    public virtual void Activate(GameObject parent) { }
    public virtual void Deactivate(GameObject parent) { }
    public virtual void Fire(GameObject parent) { }
    public virtual void subtractAmmo(GameObject parent) { }

    public void Awake()
    {
        
    }

    public int getColor()
    {
        return color;
    }

    public float getAbilityAmmo()
    {
        return abilityAmmo;
    }

    public void resetAbilityAmmo()
    {
        abilityAmmo = maxAbilityAmmo;
    }

    public float getMaxCooldown()
    {
        return cooldownTime;
    }
}
