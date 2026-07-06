using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class uiAbility : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    [SerializeField] public abilityInputManager inputManager;
    [SerializeField] private abilityHolder abilityHolder;
    [SerializeField] private GameObject imageObject;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] public Vector2 originalPosition;
    [SerializeField] private RectTransform combinerUIRectTransform;
    [SerializeField] private Vector2 combinerUIPosition;
    private GameObject combinerUIObject;
    [SerializeField] private combiner combiner;
    private abilityUIManager uiManager;

    public virtual void OnPointerUp(PointerEventData eventData){}

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        combiner = GameObject.Find("Combiner").GetComponent<combiner>();
        uiManager = GameObject.Find("AbilityUI").GetComponent<abilityUIManager>();
        combinerUIObject = GameObject.Find("CombinerUI");
        combinerUIPosition = combinerUIObject.GetComponent<RectTransform>().anchoredPosition;
        combinerUIRectTransform = combinerUIObject.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (uiManager.checkForRectOverlap(rectTransform, combinerUIRectTransform) && !combiner.isCombinerFull())
        {
            rectTransform.anchoredPosition = combinerUIPosition;
            abilityHolder.setCombinerInput();
        }
        else
        {
            //uiManager.moveToPlace(abilityHolder);
            abilityHolder.resetCombinerInput();
            combiner.freeAbility(abilityHolder);
            combiner.recheckPrep();
            //rectTransform.anchoredPosition = originalPosition;
        }
    }

    public Vector2 getCurrentUiPosition()
    {
        return rectTransform.anchoredPosition;
    }

    public bool forgiveDrag()
    {
        Vector2 difference = new Vector2(rectTransform.anchoredPosition.x - originalPosition.x, rectTransform.anchoredPosition.y - originalPosition.y);
        double xDifference = difference.x;
        double yDifference = difference.y;

        if (xDifference is >= -20 and <= 20 && yDifference is >= -20 and <= 20)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
