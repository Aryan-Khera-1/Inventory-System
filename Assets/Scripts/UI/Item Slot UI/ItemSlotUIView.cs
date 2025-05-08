using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemSlotUIView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button iconButton;

    public TextMeshProUGUI QuantityText => quantityText;
    public Button IconButton => iconButton;
}
