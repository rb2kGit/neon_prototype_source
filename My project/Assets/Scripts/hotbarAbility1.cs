using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class hotbarAbility1 : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private abilityInputManager inputManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        inputManager.aInput1 = true;
    }
}
