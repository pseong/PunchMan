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

    private List<Item> inventoryTabList;  // 선택한 탭에 따라 다르게 보여질 아이템 리스트.

    private const int HAT = 0, HEAD = 1, CLOTHES = 2, GLOVES = 3, PANTS = 4, SHOES = 5;

    public Transform content;

    private int selectedItem; // 선택된 아이템
    private int selectedTab; // 선택된 탭

    public WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public Scrollbar scrollbar;

    public SpriteRenderer hat;
    public SpriteRenderer head;
    public SpriteRenderer clothes;
    public SpriteRenderer glove_r;
    public SpriteRenderer glove_l;
    public SpriteRenderer shoe_r;
    public SpriteRenderer shoe_l;

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        PlayerStat.instance.inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        inventorySlots = new List<InventorySlot>();
        inventorySlots.Clear();

        selectedTab = 0;
        selectedItem = -1;

        PlayerStat.instance.inventoryItemList.Add(new Item(50001, "라라랄장갑", "공격력 +2", Item.ItemType.GLOVES, 2));

        PlayerStat.instance.inventoryItemList.Add(new Item(60001, "땡땡땡신발", "속도 +100", Item.ItemType.SHOES, 0, 0, 0, 0, 100));

        PlayerStat.instance.inventoryItemList.Add(new Item(70001, "민무늬1", "공격력 +2", Item.ItemType.CLOTHES, 2));
        PlayerStat.instance.inventoryItemList.Add(new Item(70002, "롤로로롤옷", "공격력 +2", Item.ItemType.CLOTHES, 2));

        PlayerStat.instance.inventoryItemList.Add(new Item(80001, "둥이머리", "공격력 +2", Item.ItemType.HEAD, 2));

        PlayerStat.instance.inventoryItemList.Add(new Item(90000, "지피지피신모자", "공격력 +2", Item.ItemType.HAT, 5));
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
        for (int i = 0; i < PlayerStat.instance.inventoryItemList.Count; i++)
        {
            if (selectedTab == (int)PlayerStat.instance.inventoryItemList[i].itemType)
                inventoryTabList.Add(PlayerStat.instance.inventoryItemList[i]);
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
            equipSlots[i].SetItem(PlayerStat.instance.equipItemList[i], i);
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
        if (index < 6) return PlayerStat.instance.equipItemList[index];
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

    public void ClickEquip()
    {
        if (selectedItem >= 6)
        {
            AudioManager.instance.Play(equip_sound);
            Item item = GetItem(selectedItem);
            int type = (int)item.itemType;

            if (PlayerStat.instance.equipItemList[type].itemType != Item.ItemType.AIR)
            {
                Item temp = PlayerStat.instance.equipItemList[type];
                PlayerStat.instance.equipItemList[type] = item;
                PlayerStat.instance.inventoryItemList.Remove(item);
                PlayerStat.instance.inventoryItemList.Add(temp);
            }
            else
            {
                PlayerStat.instance.equipItemList[type] = item;
                PlayerStat.instance.inventoryItemList.Remove(item);
            }
            ShowItem();
        }
    }

    public void ClickRelease()
    {
        if (selectedItem >= 0 && selectedItem < 6 && PlayerStat.instance.equipItemList[selectedItem].itemType != Item.ItemType.AIR)
        {
            AudioManager.instance.Play(equip_sound);
            PlayerStat.instance.inventoryItemList.Add(PlayerStat.instance.equipItemList[selectedItem]);
            PlayerStat.instance.equipItemList[selectedItem] = new Item(0, "", "", Item.ItemType.AIR);
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
            PlayerStat.instance.plus_atk += PlayerStat.instance.equipItemList[i].atk;
            PlayerStat.instance.plus_cric += PlayerStat.instance.equipItemList[i].cric;
            PlayerStat.instance.plus_crid += PlayerStat.instance.equipItemList[i].crid;
            PlayerStat.instance.plus_cdr += PlayerStat.instance.equipItemList[i].cdr;
            PlayerStat.instance.plus_speed += PlayerStat.instance.equipItemList[i].speed;
        }
        if (PlayerStat.instance.plus_cdr >= 50)
        {
            PlayerStat.instance.plus_cdr = 50;
        }
        if (PlayerStat.instance.plus_cric >= 80)
        {
            PlayerStat.instance.plus_cric = 80;
        }

        if (PlayerStat.instance.equipItemList[HAT].itemType != Item.ItemType.AIR) hat.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[HAT].itemID];
        else hat.sprite = null;

        if (PlayerStat.instance.equipItemList[HEAD].itemType != Item.ItemType.AIR) head.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[HEAD].itemID];
        else head.sprite = baseHead;

        if (PlayerStat.instance.equipItemList[CLOTHES].itemType != Item.ItemType.AIR) clothes.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[CLOTHES].itemID];
        else clothes.sprite = baseClothes;

        if (PlayerStat.instance.equipItemList[GLOVES].itemType != Item.ItemType.AIR) glove_l.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[GLOVES].itemID];
        else glove_l.sprite = baseGloves;

        if (PlayerStat.instance.equipItemList[GLOVES].itemType != Item.ItemType.AIR) glove_r.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[GLOVES].itemID];
        else glove_r.sprite = baseGloves;

        if (PlayerStat.instance.equipItemList[SHOES].itemType != Item.ItemType.AIR) shoe_l.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[SHOES].itemID];
        else shoe_l.sprite = baseShoes;

        if (PlayerStat.instance.equipItemList[SHOES].itemType != Item.ItemType.AIR) shoe_r.sprite = ItemResourceManager.instance.sprites[PlayerStat.instance.equipItemList[SHOES].itemID];
        else shoe_r.sprite = baseShoes;
    }
}