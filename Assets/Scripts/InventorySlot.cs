using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Item item;
    public GameObject selected_item;
    private Inventory inventory;
    public int index;

    public void SetItem(Item _item, int _index)
    {
        icon.sprite = _item.itemIcon;
        item = _item;
        index = _index;
        inventory = GetComponentInParent<Inventory>();

        if (icon.sprite.rect.width > icon.sprite.rect.height)
        {
            float ratio = icon.sprite.rect.height / icon.sprite.rect.width;
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100 * ratio);
        }
        else
        {
            float ratio = icon.sprite.rect.width / icon.sprite.rect.height;
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * ratio, 100);
        }
    }

    public void SelectedItem()
    {
        inventory.SelectedItem(index);
    }
}
