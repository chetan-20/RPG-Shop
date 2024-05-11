using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopItemsSO[] shopItemsSO;
    [SerializeField] private ItemsTemplate[] itemsTemplate;


    private void Start()
    {
        ConnectSOtoUI();
    }

    private void ConnectSOtoUI()
    {
        for(int i=0; i<shopItemsSO.Length; i++)
        {
            itemsTemplate[i].priceText.text = shopItemsSO[i].buyingPrice.ToString();
            itemsTemplate[i].iconImage.sprite = shopItemsSO[i].icon;
            itemsTemplate[i].descriptionText.text = shopItemsSO[i].itemDescription;
            itemsTemplate[i].itemweightText.text = shopItemsSO[i].weight.ToString();
            itemsTemplate[i].QuantityText.text = shopItemsSO[i].quantity.ToString();
            itemsTemplate[i].rarityText.text = shopItemsSO[i].itemRarity.ToString();
            itemsTemplate[i].itemType = shopItemsSO[i].itemType;
        }
    }
   
}
