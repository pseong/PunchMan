using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Item item;
    public GameObject selected_item;
    private Inventory inventory;
    public int index;

    public void SetItem(Item item, int index)
    {
        this.item = item;
        this.index = index;
        image.sprite = ItemResourceManager.instance.sprites[item.itemID];

        inventory = GetComponentInParent<Inventory>();

        if (image.sprite.rect.width > image.sprite.rect.height)
        {
            float ratio = image.sprite.rect.height / image.sprite.rect.width;
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100 * ratio);
        }
        else
        {
            float ratio = image.sprite.rect.width / image.sprite.rect.height;
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * ratio, 100);
        }
    }

    public void SelectedItem()
    {
        inventory.SelectedItem(index);
    }
}
