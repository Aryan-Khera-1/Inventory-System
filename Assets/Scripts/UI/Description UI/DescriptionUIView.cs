using System.Collections;
using System.Collections.Generic;
using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUIView : MonoBehaviour, IUIView
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI quantityValueToSet;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI currencyRequired;
    [SerializeField] private TextMeshProUGUI weightRequired;
    [SerializeField] private Slider quantitySlider;
    [SerializeField] private Image itemIcon;
    
    public TextMeshProUGUI DescriptionText => descriptionText;
    public TextMeshProUGUI QuantityValueToSet => quantityValueToSet;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI CurrencyRequired => currencyRequired;
    public TextMeshProUGUI WeightRequired => weightRequired;
    public Slider QuantitySlider => quantitySlider;
    public Image ItemIcon => itemIcon;
    
    public bool IsActive => gameObject.activeSelf;

    public void EnableView() => gameObject.SetActive(true);
    public void DisableView() => gameObject.SetActive(false);
}
