using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<Inventory>();
                DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    private static Inventory m_instance;

    private DataBaseManager theDataBase;

    public Text descriptionTxt; // 부연설명.

    public Transform tf; // slot 부모객체.

    public GameObject[] selectedTabs; //선택된 탭의 약간 흐릿하게하는 패널

    public string touch_sound;
    public string equip_sound;

    public GameObject slotPreFab;

    public List<InventorySlot> slots;

    private List<Item> inventoryItemList; //플레이어가 소지한 아이템 리스트.
    private List<Item> inventoryTabList;  // 선택한 탭에 따라 다르게 보여질 아이템 리스트.

    private Item[] equipItemList;
    private const int HAT = 0, HEAD = 1, CLOTHES = 2, GLOVES = 3, SHOES = 4;

    public Transform content;

    private int selectedItem; // 선택된 아이템.
    private int selectedTab; // 선택된 탭.

    public WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public Scrollbar scrollbar;

    private PlayerStat playerStat;

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = new List<InventorySlot>();
        slots.Clear();
        equipItemList = new Item[5];
        equipItemList[0] = new Item(0, "", "", Item.ItemType.HAT);
        equipItemList[1] = new Item(0, "", "", Item.ItemType.HAT);
        equipItemList[2] = new Item(0, "", "", Item.ItemType.HAT);
        equipItemList[3] = new Item(0, "", "", Item.ItemType.HAT);
        equipItemList[4] = new Item(0, "", "", Item.ItemType.HAT);

        selectedTab = 0;
        selectedItem = -1;

        //inventoryItemList.Add(new Item(10001, "빨간 포션", "체력을 50 회복시켜주는 기적의 물약", Item.ItemType.HAT));
        //inventoryItemList.Add(new Item(10002, "파란 포션", "마나를 15 회복시켜주는 기적의 물약", Item.ItemType.HAT));
        //inventoryItemList.Add(new Item(10003, "농축 빨간 포션", "체력을 350 회복시켜주는 기적의 농축 물약", Item.ItemType.CLOTHES));
        //inventoryItemList.Add(new Item(10004, "농축 파란 포션", "마나를 80 회복시켜주는 기적의 농축 물약", Item.ItemType.CLOTHES));
        //inventoryItemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 포션이 나온다. 낮은 확률로 꽝", Item.ItemType.SHOES,0,0,0,0,100));
        //inventoryItemList.Add(new Item(20001, "짧은 검", "기본적인 용사의 검", Item.ItemType.GLOVES, 5));
        //inventoryItemList.Add(new Item(21001, "사파이어 반지", "1분에 마나 1을 회복시켜주는 마법 반지", Item.ItemType.HEAD));
        inventoryItemList.Add(new Item(50001, "라라랄장갑", "공격력 +2", Item.ItemType.GLOVES, 2));
        inventoryItemList.Add(new Item(60000, "땡땡땡신발", "속도 +100", Item.ItemType.SHOES, 0, 0, 0, 0, 100));
        inventoryItemList.Add(new Item(70000, "롤로로롤옷", "공격력 +2", Item.ItemType.CLOTHES, 2));
        inventoryItemList.Add(new Item(80000, "말마다랃라머리", "공격력 +2", Item.ItemType.HEAD, 2));
        for (int i = 0; i < 10; ++i)
        {
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
            inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
        }
    }

    void Start()
    {
        theDataBase = FindObjectOfType<DataBaseManager>();
        playerStat = FindObjectOfType<PlayerStat>();
    }

    public void ClickTab0()
    {
        if (selectedTab != 0)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedTab = 0;
        ShowItem();
    }

    public void ClickTab1()
    {
        if (selectedTab != 1)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedTab = 1;
        ShowItem();
    }

    public void ClickTab2()
    {
        if (selectedTab != 2)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedTab = 2;
        ShowItem();
    }

    public void ClickTab3()
    {
        if (selectedTab != 3)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedTab = 3;
        ShowItem();
    }

    public void ClickTab4()
    {
        if (selectedTab != 4)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedTab = 4;
        ShowItem();
    }

    public void OpenIventory()
    {
        ShowItem();
    }

    public void ReSizeContent()
    {
        RectTransform rt = content.GetComponent<RectTransform>();
        if (inventoryTabList.Count <= 16)
            rt.sizeDelta = new Vector2(0, 572);
        else
            rt.sizeDelta = new Vector2(0, 572 + 139 * ((int)((inventoryTabList.Count - 17) / 4) + 1));
    }

    public void CloseIventory()
    {
        AudioManager.instance.Play(touch_sound);

        EquipEffect();
        LobbyManager.instance.ResetStat();

        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = -1;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Count; ++i)
            Destroy(slots[i].gameObject);
        slots.Clear();
    }

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        SelectedItem(-1);
        Color color = selectedTabs[selectedTab].GetComponentInChildren<Image>().color;
        color.a = 0;
        selectedTabs[0].GetComponent<Image>().color = color;
        selectedTabs[1].GetComponent<Image>().color = color;
        selectedTabs[2].GetComponent<Image>().color = color;
        selectedTabs[3].GetComponent<Image>().color = color;
        selectedTabs[4].GetComponent<Image>().color = color;

        color.a = 0.5f;
        switch (selectedTab)
        {
            case 0:
                selectedTabs[0].GetComponentInChildren<Image>().color = color;
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.HAT == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                selectedTabs[1].GetComponentInChildren<Image>().color = color;
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.HEAD == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                selectedTabs[2].GetComponentInChildren<Image>().color = color;
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.CLOTHES == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 3:
                selectedTabs[3].GetComponentInChildren<Image>().color = color;
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.GLOVES == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 4:
                selectedTabs[4].GetComponent<Image>().color = color;
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.SHOES == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        } // 탭에 따른 아이템 분류. 그것을 인벤토리 탭 리스트에 추가

        for (int i = 0; i < inventoryTabList.Count; ++i)
        {
            GameObject slotClone = Instantiate(slotPreFab);
            slotClone.transform.SetParent(tf);
            slotClone.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            slots.Add(slotClone.GetComponent<InventorySlot>());  // 항상 주의하자.......부모에게 붙일 때 먼가 값이 자동으로 변경이 된다...
            //항상 부모로 붙여놓고 add 해야한다.

            slots[i].equipTxt.text = "";
            slots[i].AddItem(inventoryTabList[i], i);
            if (inventoryTabList[i].itemID == equipItemList[selectedTab].itemID)
            {
                slots[i].equipTxt.text = "장착";
            }
        }
        descriptionTxt.text = "";// 인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가
        ReSizeContent();

        //SelectedItem();
    } // 아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 인벤토리 슬롯에 출력)

    public void SelectedItem(int index)
    {
        if (selectedItem != index)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedItem = index;
        StopAllCoroutines();
        if (selectedItem >= 0)
        {
            Color color = slots[0].selected_item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; ++i)
                slots[i].selected_item.GetComponent<Image>().color = color; // 컬러 한번에 바꿀 수는 없나????
            descriptionTxt.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
    }

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (true)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void GetItem(int _itemID, int _count = 1)
    {
        // 유물 비슷한 시스템 해볼까....

        /*
        for (int i = 0; i < theDataBase.itemList.Count; i++) // 데이터 베이스 아이템 검색
        {
            if (_itemID == theDataBase.itemList[i].itemID) // 데이터 베이스 아이템 발견
            {
                //if (theDataBase.itemList[i].itemType == Item.ItemType.Use) // 아이템이 소모품이라면
                //{
                for (int j = 0; j < inventoryItemList.Count; ++j) // 소지품에 같은 아이템 소지 유무 확인
                {
                    //같은게 있으면 장비의 레벨을 업그레이드 시켜준다?
                    if (inventoryItemList[j].itemID == _itemID) // 소지품에 같은 아이템 있다면 개수만 증가
                    {
                        inventoryItemList[j].itemCount += _count;
                    }
                    return;
                }
                //}
            }
            inventoryItemList.Add(theDataBase.itemList[i]); // 소지품에 같은 소모품 없거나 장비라면 해당 아이템 추가
            inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
            return;
        }
        Debug.LogError("잘못된 아이템 입니다.");*/
    }

    public void ClickEquip()
    {
        if (selectedItem > -1)
        {
            AudioManager.instance.Play(equip_sound);
            equipItemList[selectedTab] = inventoryTabList[selectedItem];
            for (int i = 0; i < inventoryTabList.Count; ++i)
            {
                slots[i].equipTxt.text = "";
            }
            slots[selectedItem].equipTxt.text = "장착";
        }
    }

    public void ClickRelease()
    {
        if (selectedItem > -1)
        {
            if (equipItemList[selectedTab].itemID == inventoryTabList[selectedItem].itemID)
            {
                AudioManager.instance.Play(equip_sound);
                equipItemList[selectedTab] = new Item(0, "", "", Item.ItemType.HAT);
                slots[selectedItem].equipTxt.text = "";
            }
        }
    }

    private void EquipEffect()
    {
        playerStat.plus_atk = 0;
        playerStat.plus_cric = 0;
        playerStat.plus_crid = 0;
        playerStat.plus_cdr = 0;
        playerStat.plus_speed = 0;

        for (int i = 0; i < 5; ++i) {
            playerStat.plus_atk += equipItemList[i].atk;
            playerStat.plus_cric += equipItemList[i].cric;
            playerStat.plus_crid += equipItemList[i].crid;
            playerStat.plus_cdr += equipItemList[i].cdr;
            playerStat.plus_speed += equipItemList[i].speed;
        }
        if(playerStat.plus_cdr >= 50)
        {
            playerStat.plus_cdr = 50;
        }
        if(playerStat.plus_cric >= 80)
        {
            playerStat.plus_cric = 80;
        }

        playerStat.hat.sprite = equipItemList[HAT].itemIcon;
        playerStat.head.sprite = equipItemList[HEAD].itemIcon;
        playerStat.clothes.sprite = equipItemList[CLOTHES].itemIcon;
        playerStat.glove_l.sprite = equipItemList[GLOVES].itemIcon;
        playerStat.glove_r.sprite = equipItemList[GLOVES].itemIcon;
        playerStat.shoe_l.sprite = equipItemList[SHOES].itemIcon;
        playerStat.shoe_r.sprite = equipItemList[SHOES].itemIcon;
        
    }
}
