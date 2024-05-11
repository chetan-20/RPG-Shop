using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemsTemplate : MonoBehaviour
{
    [SerializeField] internal Button buyButton;
    [SerializeField] internal Image iconImage;
    [SerializeField] internal TMP_Text priceText;
    [SerializeField] internal TMP_Text QuantityText;
    [SerializeField] internal TMP_Text descriptionText;
    [SerializeField] internal TMP_Text rarityText;
    [SerializeField] internal TMP_Text itemweightText;
    internal ShopItemsSO.type itemType;   
}
