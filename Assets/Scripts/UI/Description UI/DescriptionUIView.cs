using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI quantityValueToSet;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Slider quantitySlider;
    [SerializeField] private Image itemIicon;
}
