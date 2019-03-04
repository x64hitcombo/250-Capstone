using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TooltipManager : MonoBehaviour {

    public TooltipUI tip;

    public TooltipTarget target;

    //Here goes the singleton delcaration of the manager
    public static TooltipManager _instance;
    public static TooltipManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TooltipManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("TooltipManager");
                    _instance = container.AddComponent<TooltipManager>();
                }
            }

            return _instance;
        }
    }

    public void Start()
    {
        target = null;
    }

    public void Update()
    {
        if (target != null)
        {
            if (tip.isActiveAndEnabled && !target.isActiveAndEnabled)
            {
                mHideTip();
            }
        }
        else
        {
            mHideTip();
        }
    }

    public void setTooltip()
    {
        if (target != null)
        {

            if (target.Type == TooltipTarget.InfoType.invItem)
            {
                Item item = target.gameObject.GetComponent<ItemOnObject>().item;
                tip.sprite.SetActive(true);
                tip.targetStat = null;
                tip.tipTitle.text = item.itemName;
                tip.sprite.GetComponent<Image>().sprite = item.itemIcon;
                tip.tipDesc.text = item.itemDesc;
                tip.tipTitle.color = target.titleColor;
            }



            else if (target.Type == TooltipTarget.InfoType.Survival)
            {

                tip.targetStat = target.Stats.ToString();
                tip.sprite.SetActive(false);
                tip.tipTitle.text = target.newTipTitle;
                tip.tipDesc.text = target.newTipDesc;
                tip.tipTitle.color = target.titleColor;
                tip.tipValue.color = target.valueColor;
            }

                mShowTip();
            }
        }

    public void mShowTip()
    {
        tip.ShowToolTip();
    }


    public void mHideTip()
    {
        tip.HideToolTip();
    }
}