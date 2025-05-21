using UnityEngine;

[CreateAssetMenu(fileName = "abilityBase", menuName = "Scriptable Objects/abilityBase")]
public class abilityBase : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent){}
    
}
