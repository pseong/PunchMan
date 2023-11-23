using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public ItemType itemType;

    public enum ItemType
    {
        HAT,
        HEAD,
        CLOTHES,
        GLOVES,
        PANTS,
        SHOES,
        AIR
    }

    public int atk;
    public int cric;
    public int crid;
    public int cdr;
    public int speed;

    public Item(int itemID, string itemName, string itemDes, ItemType itemType,
        int atk = 0, int cric = 0, int crid = 0, int cdr = 0, int speed = 0, int itemCount = 1)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDes;
        this.itemType = itemType;
        this.itemCount = itemCount;

        this.atk = atk;
        this.cric = cric;
        this.crid = crid;
        this.speed = speed;
        this.cdr = cdr;
    }
}
