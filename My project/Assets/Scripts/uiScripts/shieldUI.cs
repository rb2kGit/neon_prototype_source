using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class shieldUI : MonoBehaviour
{
    [SerializeField] private Image[] shieldImages;
    [SerializeField] private GameObject shieldUIObject;

    //Start is called before the first frame.
    void Start()
    {
        shieldImages = new Image[shieldUIObject.transform.childCount];

        for (int i = 0; i <= shieldUIObject.transform.childCount; i++)
        {
            shieldImages[i] = shieldUIObject.transform.GetChild(i).GetComponent<Image>();
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        playerPosition = camera.ScreenToWorldPoint(playerTransform.position);

        thisPosition = camera.ScreenToWorldPoint(thisTransform.position);
        
        thisTransform.position = playerPosition;
    }*/

    public void setShieldImageFill(int index)
    {
        shieldImages[index].fillAmount = 0.5f;
    }

    public void resetShieldFillImage(int index)
    {
        shieldImages[index].fillAmount = 1f;
    }

    public void disbaleShieldImage(int index)
    {
        shieldImages[index].enabled = false;
    }
}
