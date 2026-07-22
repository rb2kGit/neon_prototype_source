using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiCombiner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readyText;
    [SerializeField] private Image combinerAbilityImage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        disableReadyText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateReadyText(){
        readyText.enabled = true;
    }

    public void disableReadyText(){
        readyText.enabled = false;
    }

    public void setAbilityImage(Image image)
    {
        Debug.Log("Change Image");
        combinerAbilityImage = image;
    }

}
