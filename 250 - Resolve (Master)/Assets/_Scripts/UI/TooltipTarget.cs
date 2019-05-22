using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public enum InfoType
    {
        invItem,
        Survival,
        other
    };
    public InfoType Type;

    public enum SurvStats
    {
        Thirst,
        Hunger,
        Exposure,
        Fatigue
    };
    public SurvStats Stats;

    public string newTipTitle;
    public Color titleColor = new Color(1, 1, 1, 1f);
    public string newTipValue;
    public Color valueColor = new Color(1, 1, 1, 1f);
    public string newTipDesc;
    public Sprite newTipIcon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.target = this.GetComponent<TooltipTarget>();
        TooltipManager.Instance.setTooltip();
        TooltipManager.Instance.mShowTip();
        Debug.Log("Yeet");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.target = null;
        TooltipManager.Instance.mHideTip();

    }
}