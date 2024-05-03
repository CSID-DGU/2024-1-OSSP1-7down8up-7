using UnityEngine;


public class Player_head : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[4];
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (inputX == 1 && inputY >= 0)
            spriteRenderer.sprite = sprites[1];
        else if (inputX == -1 && inputY >= 0)
            spriteRenderer.sprite = sprites[2];
        else if (inputY == 1)
            spriteRenderer.sprite = sprites[3];
        else
            spriteRenderer.sprite = sprites[0];
    }
}

