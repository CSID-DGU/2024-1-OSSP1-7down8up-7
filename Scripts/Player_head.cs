using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_head : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float inputX = Input.GetAxisRaw("Horizontal"); // 수평이동
        float inputY = Input.GetAxisRaw("Vertical"); // 수직이동

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
