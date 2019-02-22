using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipTarget_inv : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public TooltipUI tip;
    public Item item;

    public Color titleColor = new Color(1, 1, 1, 1f);

    private ItemDataBaseList itemDatabase;

    private void Start()
    {
        tip = GameObject.Find("Tooltip").GetComponent<TooltipUI>();
        item = GetComponent<ItemOnObject>().item;                   //we get the item (requires ItemOnObject component)

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tip.tipValue.text = "-";
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
