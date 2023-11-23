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

    public Sprite baseHead;
    public Sprite baseClothes;
    public Sprite baseGloves;
    public Sprite basePants;
    public Sprite baseShoes;

    private static Inventory m_instance;

    private ItemResourceManager itemResourceManager;

    public Text descriptionTxt; // 부연설명.

    public Transform tf; // slot 부모객체.

    public GameObject[] selectedTabs; // 선택된 탭

    public string touch_sound;
    public string equip_sound;

    public GameObject slotPreFab;

    public List<InventorySlot> inventorySlots; // 인벤토리 슬롯
    public InventorySlot[] equipSlots; // 장착 슬롯

    private List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트.
    private List<Item> inventoryTabList;  // 선택한 탭에 따라 다르게 보여질 아이템 리스트.
    private Item[] equipItemList; // 장착한 아이템

    private const int HAT = 0, HEAD = 1, CLOTHES = 2, GLOVES = 3, PANTS = 4, SHOES = 5;

    public Transform content;

    private int selectedItem; // 선택된 아이템
    private int selectedTab; // 선택된 탭

    public WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public Scrollbar scrollbar;

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        inventorySlots = new List<InventorySlot>();
        inventorySlots.Clear();
        equipItemList = new Item[6];
        equipItemList[0] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[1] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[2] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[3] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[4] = new Item(0, "", "", Item.ItemType.AIR);
        equipItemList[5] = new Item(0, "", "", Item.ItemType.AIR);

        selectedTab = 0;
        selectedItem = -1;

        inventoryItemList.Add(new Item(50001, "라라랄장갑", "공격력 +2", Item.ItemType.GLOVES, 2));

        inventoryItemList.Add(new Item(60001, "땡땡땡신발", "속도 +100", Item.ItemType.SHOES, 0, 0, 0, 0, 100));

        inventoryItemList.Add(new Item(70001, "민무늬1", "공격력 +2", Item.ItemType.CLOTHES, 2));
        inventoryItemList.Add(new Item(70002, "롤로로롤옷", "공격력 +2", Item.ItemType.CLOTHES, 2));

        inventoryItemList.Add(new Item(80001, "둥이머리", "공격력 +2", Item.ItemType.HEAD, 2));

        inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
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

    public void ClickTab5()
    {
        if (selectedTab != 5)
        {
            AudioManager.instance.Play(touch_sound);
        }
        selectedTab = 5;
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
        for (int i = 0; i < inventorySlots.Count; ++i)
            Destroy(inventorySlots[i].gameObject);
        inventorySlots.Clear();
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
        selectedTabs[5].GetComponent<Image>().color = color;

        color.a = 0.5f;

        // 탭에 따른 아이템 분류. 그것을 인벤토리 탭 리스트에 추가
        selectedTabs[selectedTab].GetComponentInChildren<Image>().color = color;
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (selectedTab == (int)inventoryItemList[i].itemType)
                inventoryTabList.Add(inventoryItemList[i]);
        }

        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            GameObject slotClone = Instantiate(slotPreFab);
            slotClone.transform.SetParent(tf);
            slotClone.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            inventorySlots.Add(slotClone.GetComponent<InventorySlot>());  // 항상 주의하자.......부모에게 붙일 때 먼가 값이 자동으로 변경이 된다...
            //항상 부모로 붙여놓고 add 해야한다.

            inventorySlots[i].SetItem(inventoryTabList[i], i + 6);
        }
        descriptionTxt.text = "";// 인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가
        ReSizeContent();


        for (int i = 0; i < 6; i++)
        {
            equipSlots[i].SetItem(equipItemList[i], i);
        }

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
            Color color = GetSlot(0).selected_item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count + 6; i++)
                GetSlot(i).selected_item.GetComponent<Image>().color = color; // 컬러 한번에 바꿀 수는 없나????
            descriptionTxt.text = GetItem(selectedItem).itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
    }

    Item GetItem(int index)
    {
        if (index < 6) return equipItemList[index];
        else return inventoryTabList[index - 6];
    }

    InventorySlot GetSlot(int index)
    {
        if (index < 6) return equipSlots[index];
        else return inventorySlots[index - 6];
    }

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (true)
        {
            Color color = GetSlot(0).GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                GetSlot(selectedItem).selected_item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                GetSlot(selectedItem).selected_item.GetComponent<Image>().color = color;
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
        if (selectedItem >= 6)
        {
            AudioManager.instance.Play(equip_sound);
            Item item = GetItem(selectedItem);
            int type = (int)item.itemType;

            if (equipItemList[type].itemType != Item.ItemType.AIR)
            {
                Item temp = equipItemList[type];
                equipItemList[type] = item;
                inventoryItemList.Remove(item);
                inventoryItemList.Add(temp);
            }
            else
            {
                equipItemList[type] = item;
                inventoryItemList.Remove(item);
            }
            ShowItem();
        }
    }

    public void ClickRelease()
    {
        if (selectedItem >= 0 && selectedItem < 6 && equipItemList[selectedItem].itemType != Item.ItemType.AIR)
        {
            AudioManager.instance.Play(equip_sound);
            inventoryItemList.Add(equipItemList[selectedItem]);
            equipItemList[selectedItem] = new Item(0, "", "", Item.ItemType.AIR);
        }
        ShowItem();
    }

    private void EquipEffect()
    {
        PlayerStat.instance.plus_atk = 0;
        PlayerStat.instance.plus_cric = 0;
        PlayerStat.instance.plus_crid = 0;
        PlayerStat.instance.plus_cdr = 0;
        PlayerStat.instance.plus_speed = 0;

        for (int i = 0; i < 6; ++i)
        {
            PlayerStat.instance.plus_atk += equipItemList[i].atk;
            PlayerStat.instance.plus_cric += equipItemList[i].cric;
            PlayerStat.instance.plus_crid += equipItemList[i].crid;
            PlayerStat.instance.plus_cdr += equipItemList[i].cdr;
            PlayerStat.instance.plus_speed += equipItemList[i].speed;
        }
        if (PlayerStat.instance.plus_cdr >= 50)
        {
            PlayerStat.instance.plus_cdr = 50;
        }
        if (PlayerStat.instance.plus_cric >= 80)
        {
            PlayerStat.instance.plus_cric = 80;
        }

        if (equipItemList[HAT].itemType != Item.ItemType.AIR) PlayerStat.instance.hat.sprite = ItemResourceManager.instance.sprites[equipItemList[HAT].itemID];
        else PlayerStat.instance.hat.sprite = null;

        if (equipItemList[HEAD].itemType != Item.ItemType.AIR) PlayerStat.instance.head.sprite = ItemResourceManager.instance.sprites[equipItemList[HEAD].itemID];
        else PlayerStat.instance.head.sprite = baseHead;

        if (equipItemList[CLOTHES].itemType != Item.ItemType.AIR) PlayerStat.instance.clothes.sprite = ItemResourceManager.instance.sprites[equipItemList[CLOTHES].itemID];
        else PlayerStat.instance.clothes.sprite = baseClothes;

        if (equipItemList[GLOVES].itemType != Item.ItemType.AIR) PlayerStat.instance.glove_l.sprite = ItemResourceManager.instance.sprites[equipItemList[GLOVES].itemID];
        else PlayerStat.instance.glove_l.sprite = baseGloves;

        if (equipItemList[GLOVES].itemType != Item.ItemType.AIR) PlayerStat.instance.glove_r.sprite = ItemResourceManager.instance.sprites[equipItemList[GLOVES].itemID];
        else PlayerStat.instance.glove_r.sprite = baseGloves;

        if (equipItemList[SHOES].itemType != Item.ItemType.AIR) PlayerStat.instance.shoe_l.sprite = ItemResourceManager.instance.sprites[equipItemList[SHOES].itemID];
        else PlayerStat.instance.shoe_l.sprite = baseShoes;

        if (equipItemList[SHOES].itemType != Item.ItemType.AIR) PlayerStat.instance.shoe_r.sprite = ItemResourceManager.instance.sprites[equipItemList[SHOES].itemID];
        else PlayerStat.instance.shoe_r.sprite = baseShoes;
    }
}
