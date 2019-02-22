using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public TooltipUI tip;

    public Sprite newTipIcon;
    public string newTipValue;
    public Color valueColor = new Color(1, 1, 1, 1f);
    public string newTipTitle;
    public Color titleColor = new Color(1, 1, 1, 1f);
    public string newTipDesc;

    private void Start()
    {
        //tip = GameObject.Find("Tooltip").GetComponent<TooltipUI>();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tip.sprite.GetComponent<Image>().sprite)
        {
            tip.sprite.GetComponent<Image>().sprite = newTipIcon;
            tip.tipValue.text = "";
        }
        else
        {
            tip.sprite.GetComponent<Image>().sprite = null;
            tip.tipValue.text = newTipValue;
        }
        tip.tipTitle.text = newTipTitle;
        tip.tipDesc.text = newTipDesc;
        tip.tipTitle.color = titleColor;
        tip.ShowToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tip.HideToolTip();
    }
}
