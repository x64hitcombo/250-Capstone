using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipTarget_inv : TooltipTarget, IPointerEnterHandler, IPointerExitHandler {

    public Item item;
    private ItemDataBaseList itemDatabase;

    public void Start()
    {
        item = GetComponent<ItemOnObject>().item;                   //we get the item (requires ItemOnObject componen
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tip.tipTitle.text = item.itemName;
        tip.sprite.GetComponent<Image>().sprite = item.itemIcon;
        tip.tipDesc.text = item.itemDesc;
        tip.tipTitle.color = titleColor;
        tip.ShowToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tip.HideToolTip();
    }
}
