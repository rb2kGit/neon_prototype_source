using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class debuffs : MonoBehaviour
{
    private int myColor;
    [SerializeField] private float maxDuration;
    private int type;
    private float remainingDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDebuffType(int typeNumber)
    {
        type = typeNumber;
    }

    public int getDebuffType()
    {
        return type;
    }

    public virtual void applyMe(GameObject targetObject){}
    public virtual void refreshMe(GameObject targetObject){}
    public virtual void removeMe(GameObject targetObject){}
}
