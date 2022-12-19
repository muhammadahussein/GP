using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private int charLimit;
    [SerializeField] private RectTransform rectTransform;
    private static Tooltip _instance;

    private void Awake()
    {
        _instance = this;
        HideTooltip();
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
        rectTransform.pivot = new Vector2(mousePos.x / Screen.width , mousePos.y / Screen.height);
    }

    public static void ShowTooltip(string tooltipTitle, string tooltipDescription)
    {
        _instance.SetTooltip(tooltipTitle,tooltipDescription);
        _instance.gameObject.SetActive(true);
    }
    public static void HideTooltip()
    {
        _instance.gameObject.SetActive(false);
    }
    private void SetTooltip(string tooltipTitle, string tooltipDescription)
    {
        title.text = tooltipTitle;
        description.text = tooltipDescription;
        layoutElement.enabled = (title.text.Length > charLimit || description.text.Length > charLimit) ? true : false;
    }
}
