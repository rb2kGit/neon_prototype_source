using UnityEngine;

public class playerSprite : ScriptableObject
{
   [SerializeField] private int spriteAbilityCode;

   public int getAbilityCode()
    {
        return spriteAbilityCode;
    }
}
