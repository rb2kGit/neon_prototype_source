using UnityEngine;
using TMPro;

public class uiCombiner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readyText;


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

}
