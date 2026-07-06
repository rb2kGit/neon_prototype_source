using UnityEngine;

public class playerSpriteManager : MonoBehaviour
{
    //Player Transformer.
    [SerializeField] private playerTransformer playerTransformer;

    //Player Sprites
    [SerializeField] private Sprite[] playerSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite getPlayerSprite(int positionCode)
    {
        return playerSprites[positionCode];
    }

    public Sprite getDefaultSprite()
    {
        return playerSprites[0];
    }
}
