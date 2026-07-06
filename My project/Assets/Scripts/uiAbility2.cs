using UnityEngine;
using UnityEngine.EventSystems;

public class uiAbility2 : uiAbility
{
    public override void OnPointerUp(PointerEventData eventData)
    {   
            base.OnPointerUp(eventData);

        if (getCurrentUiPosition() == originalPosition || forgiveDrag() )
        {
            inputManager.aInput2 = true;
        }
    }
}
