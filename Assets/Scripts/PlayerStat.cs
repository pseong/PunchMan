using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PlayerStat>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private static PlayerStat m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        equipItemList = new Item[6];
        equipItemList[0] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[1] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[2] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[3] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[4] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[5] = new Item(0, "", "", Item.ItemType.AIR);

        inventoryItemList = new List<Item>();
    }

    public int[] MEXP = new int[] { 0, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000 };

    public int gold;

    public int atk;
    public int cric;
    public int crid;
    public int cdr;
    public float speed;

    public int plus_atk;
    public int plus_cric;
    public int plus_crid;
    public int plus_cdr;
    public float plus_speed;

    public int lv;
    public int exp;

    public List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트
    public Item[] equipItemList; // 장착한 아이템

    public void AddExp(int value)
    {
        exp += value;
        if (exp > MEXP[lv])
        {
            if (lv < MEXP.Length - 1)
            {
                exp = exp - MEXP[lv];
                lv++;
            }
            else
            {
                exp = MEXP[lv];
            }
        }
    }
}