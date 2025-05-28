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


    public void SetItem(Sprite icon, int quantity)
    {
        var image = iconButton.image;

        if (icon != null)
        {
            image.sprite = icon;
            image.color = new Color(1, 1, 1, 1f);
        }
        //else
        //{
        //    image.sprite = null;
        //    image.color = new Color(1, 1, 1, 0f);
        //}

        quantityText.text = $"x{quantity}";
    }
}
