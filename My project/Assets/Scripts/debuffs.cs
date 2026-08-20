using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class debuffs: ScriptableObject
{
    private int myColor;
    public bool active;
    [SerializeField] public float maxDuration;
    public float triggerTime;
    public float triggerTimer;
    public float remainingDuration;
    private int type;

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

    public bool getDebuffStatus()
    {
        return active;
    }

    public virtual void applyMe(GameObject enemyObject){}
    public virtual void triggerMe(){}
    public virtual void refreshMe(){}
    public virtual void removeMe(){}
    public virtual void timer(){}
}
