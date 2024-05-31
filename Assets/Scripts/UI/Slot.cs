using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    //Image image;
    public GameObject _item;
    public SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer=GetComponent<SpriteRenderer>();
       
    }
    public GameObject item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                //image.sprite= item.GetComponent<PassiveItem>().Sprite;
                _renderer.sprite = item.GetComponent<PassiveItem>().Sprite;
                //image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                //image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
