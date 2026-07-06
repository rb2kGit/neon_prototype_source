//using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;

public class abilityUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveToCombiner(abilityHolder ability, uiCombiner combiner)
    {
        //Assign the RecTransform of the combiner and the ability.
        RectTransform combinerUILocation = combiner.GetComponent<RectTransform>();
        RectTransform abilityUILocation = ability.abilityUI.GetComponent<RectTransform>();

        //Create a point from the worldspace for the combiner.
        Vector3 worldPos = combinerUILocation.TransformPoint(combinerUILocation.rect.center);
        //Create a point from the localspace of the ability calculated from the world point of the combiner. 
        Vector3 localPos = abilityUILocation.parent.InverseTransformPoint(worldPos);
        //Assign to the ability the new localspace point calculated from the world space of the combiner.
        ability.abilityUI.GetComponent<RectTransform>().anchoredPosition = localPos;
        //ability.abilityUI.GetComponent<RectTransform>().anchoredPosition = combiner.GetComponent<RectTransform>().anchoredPosition;
    }

    public void moveToPlace(abilityHolder ability)
    {
        ability.abilityUI.GetComponent<RectTransform>().anchoredPosition = ability.abilityUI.originalPosition;
    }

    //Function to check if the RectTransform of 2 UI objects overlaps. Returns a boolean.
    public bool checkForRectOverlap(RectTransform rect1, RectTransform rect2)
    {
        //Check if both RectTransforms contain values.
        if (rect1 == null || rect2 == null)
        {
            return false;
        }

        //Create arrays to hold the world corner points of the transforms.
        Vector3[] corners1 = new Vector3[4];
        Vector3[] corners2 = new Vector3[4];

        //Get the world corner points and put them into the array.
        rect1.GetWorldCorners(corners1);
        rect2.GetWorldCorners(corners2);

        //Create 2 rectangle shapes where the corners match the corners from the RectTransforms.
        Rect rectA = new Rect(corners1[0], corners1[2] - corners1[0]);
        Rect rectB = new Rect(corners2[0], corners2[2] - corners2[0]);

        return rectA.Overlaps(rectB); //Compare and return the result of checking if the rectangle shapes overlap.

    }
}
