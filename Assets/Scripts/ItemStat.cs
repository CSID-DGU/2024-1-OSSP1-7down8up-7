using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : BaseStat
{
    enum ItemType { 
        Passive=0,
        Active=1,
        Supply=2
    }

    [SerializeField]
    ItemType type;
    public void SetItemType(int type)
    {
        this.type = (ItemType)type;
    }
}
