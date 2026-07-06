using UnityEngine;
using UnityEngine.EventSystems;

public class uiAbility3 : uiAbility
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (getCurrentUiPosition() == originalPosition || forgiveDrag() )
        {
            inputManager.aInput3 = true;
        }
    }
}
