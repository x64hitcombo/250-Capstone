using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTarget_Survival : TooltipTarget, IPointerEnterHandler, IPointerExitHandler {

    public enum SurvStats 
    {
        Thirst,
        Hunger,
        Exposure,
        Fatigue
    };
    public SurvStats activeStat = SurvStats.Thirst;

    public void Start()
    {
        tip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<TooltipUI>();
    }

    public new void OnPointerEnter(PointerEventData eventData)
    {
        tip.sprite.SetActive(false);
        if (activeStat == SurvStats.Thirst)
        {
            tip.targetStat = TooltipUI.Stats.Thirst;
        }
        else if (activeStat == SurvStats.Hunger)
        {
            tip.targetStat = TooltipUI.Stats.Hunger;
        }
        else if (activeStat == SurvStats.Exposure)
        {
            tip.targetStat = TooltipUI.Stats.Exposure;
        }
        else if (activeStat == SurvStats.Fatigue)
        {
            tip.targetStat = TooltipUI.Stats.Fatigue;
        }
        tip.tipTitle.text = newTipTitle;
        tip.tipDesc.text = newTipDesc;
        tip.tipTitle.color = titleColor;
        tip.tipValue.color = valueColor;
        tip.ShowToolTip();
    }

    public new void OnPointerExit(PointerEventData eventData)
    {
        tip.sprite.SetActive(true);
        tip.targetStat = TooltipUI.Stats.Null;
        tip.HideToolTip();
    }
}
