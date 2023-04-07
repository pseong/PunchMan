using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DataBaseManager>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private static DataBaseManager m_instance;

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public string[] var_name;
    public string[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();
    void Start()
    {
        itemList.Add(new Item(10001, "빨간 포션", "체력을 50 회복시켜주는 기적의 물약", Item.ItemType.HAT));
        itemList.Add(new Item(10002, "파란 포션", "마나를 15 회복시켜주는 기적의 물약", Item.ItemType.HAT));
        itemList.Add(new Item(10003, "농축 빨간 포션", "체력을 350 회복시켜주는 기적의 농축 물약", Item.ItemType.CLOTHES));
        itemList.Add(new Item(10004, "농축 파란 포션", "마나를 80 회복시켜주는 기적의 농축 물약", Item.ItemType.GLOVES));
        itemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 포션이 나온다. 낮은 확률로 꽝", Item.ItemType.CLOTHES));
        itemList.Add(new Item(20001, "짧은 검", "기본적인 용사의 검", Item.ItemType.SHOES, 5));
        itemList.Add(new Item(21001, "사파이어 반지", "1분에 마나 1을 회복시켜주는 마법 반지", Item.ItemType.HEAD));
    }
}
