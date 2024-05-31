using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        //items=Player.GetComponent<PlayerStat>().items;
    }
#endif

    void Awake()
    {
        FreshSlot();
        items = Player.GetComponent<PlayerStat>().items;
    }
    public void FreshSlot()
    {
      
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            
            slots[i].item = null;
            slots[i].gameObject.SetActive(false);
        }
    }

    public void AddItem(GameObject _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }
}
