using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopItemsSO[] materialItemsSO;
    [SerializeField] private ShopItemsSO[] weaponItemsSO;
    [SerializeField] private ShopItemsSO[] consumablesItemsSO;
    [SerializeField] private ShopItemsSO[] treasureItemsSO;
    [SerializeField] private ItemsTemplate[] itemsTemplate;
    [SerializeField] private ScrollRect scrollRect;   
    private void Start()
    {
        DisableAllItems();
        LoadMaterialItems();
    }
    public void LoadMaterialItems()=> ConnectSOtoUI(materialItemsSO);    
    public void LoadWeaponItems()=> ConnectSOtoUI(weaponItemsSO);   
    public void LoadConsumablesItems()=> ConnectSOtoUI(consumablesItemsSO);   
    public void LoadTreasureItems()=> ConnectSOtoUI(treasureItemsSO);   
    private void ResetSCrollRect()=> scrollRect.normalizedPosition = new Vector2(0, 1);

    private void ConnectSOtoUI(ShopItemsSO[] shopItemsSO)
    {
        DisableAllItems();
        for(int i=0; i<shopItemsSO.Length; i++)
        {
            itemsTemplate[i].gameObject.SetActive(true);
            itemsTemplate[i].priceText.text = shopItemsSO[i].buyingPrice.ToString();
            itemsTemplate[i].iconImage.sprite = shopItemsSO[i].icon;
            itemsTemplate[i].descriptionText.text = shopItemsSO[i].itemDescription;
            itemsTemplate[i].itemweightText.text = shopItemsSO[i].weight.ToString();
            itemsTemplate[i].QuantityText.text = shopItemsSO[i].quantity.ToString();
            itemsTemplate[i].rarityText.text = shopItemsSO[i].itemRarity.ToString();
            itemsTemplate[i].itemType = shopItemsSO[i].itemType;
        }
        ResetSCrollRect();       
    }
   private void DisableAllItems()
    {
        for(int i =0; i < itemsTemplate.Length; i++)
        {
            itemsTemplate[i].gameObject.SetActive(false);
        }
    }
}
