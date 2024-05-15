using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ItemsTemplate : MonoBehaviour
{
    [SerializeField] internal Button buyButton;
    [SerializeField] internal Image iconImage;
    [SerializeField] internal TMP_Text priceText;
    [SerializeField] internal TMP_Text QuantityText;
    [SerializeField] internal TMP_Text descriptionText;
    [SerializeField] internal TMP_Text rarityText;
    [SerializeField] internal TMP_Text itemweightText;
    [SerializeField] internal GameObject quantityPanel;
    [SerializeField] internal TMP_Text selectQuantityText;
    [SerializeField] internal Button increaseQuantityButton;
    [SerializeField] internal Button decreaseQuantityButton;
    [SerializeField] internal Button purhcaseButton;
    [SerializeField] internal Button cancelButton;
    internal ShopItemsSO itemSO;
    internal int playerItemQuantity;
}
