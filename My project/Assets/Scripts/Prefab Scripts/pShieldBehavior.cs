using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class pShieldBehavior : MonoBehaviour
{
    private playerController playerControllerScript;
    public Vector3 position;
    public Vector3 playerPosition;
    public float spinSpeed;
    public float lifeSpan;

    private List<GameObject> droneList;
    [SerializeField] private GameObject child1;
    [SerializeField] private GameObject child2;
    [SerializeField] private GameObject child3;
    [SerializeField] private GameObject child4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        droneList = new List<GameObject>() {child1, child2, child3, child4};
        playerControllerScript = GameObject.Find("Player").GetComponent<playerController>();
        playerPosition = GameObject.Find("Player").transform.position;
        this.transform.position = new Vector3(playerPosition.x, playerPosition.y, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (droneList.Count <= 0 )
        {
            playerControllerScript.setDrones();
            Destroy(gameObject);
        }

        playerPosition = GameObject.Find("Player").transform.position;

        this.transform.position = new Vector3(playerPosition.x, playerPosition.y, this.transform.position.z);
        this.transform.RotateAround(playerPosition, Vector3.forward, spinSpeed * Time.deltaTime);
    }

    public void removeMeFromList(GameObject childObject)
    {
        droneList.Remove(childObject);
    }   
}
