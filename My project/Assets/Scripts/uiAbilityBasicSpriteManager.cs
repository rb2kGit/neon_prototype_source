//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class uiAbilityBasicSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite[] uiAbilityImages;
    [SerializeField] private GameObject basicAbilityImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetAbilityImage()
    {
        basicAbilityImage.GetComponent<Image>().sprite = uiAbilityImages[0];
    }

    public void swapBasicAbilityImage(int imageCode)
    {
        basicAbilityImage.GetComponent<Image>().sprite = uiAbilityImages[imageCode];
    }

    public void basicAbilityImageFill(float fill)
    {
        basicAbilityImage.GetComponent<Image>().fillAmount = fill;
    }
}
