using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        HAT,
        HEAD,
        CLOTHES,
        GLOVES,
        SHOES
    }

    public int atk;
    public int cric;
    public int crid;
    public int cdr;
    public int speed;

    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType,
        int _atk = 0, int _cric = 0, int _crid = 0, int _cdr = 0, int _speed = 0, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;

        atk = _atk;
        cric = _cric; 
        crid = _crid;
        speed = _speed;
        cdr = _cdr;
    }
}
