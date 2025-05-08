using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button shopButton;

    public TextMeshProUGUI CurrencyText => currencyText;
    public TextMeshProUGUI WeightText => weightText;
    public Button InventoryButton => inventoryButton;
    public Button ShopButton => shopButton;
}