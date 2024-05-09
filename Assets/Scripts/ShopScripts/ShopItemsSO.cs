using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewShopItem",menuName ="ShopItem")]
public class ShopItemsSO : ScriptableObject
{
 
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemDescription;
    [SerializeField] private int buyingPrice;
    [SerializeField] private float weight;    
    [SerializeField] private int quantity;
    [SerializeField] private rarity itemRarity;
    [SerializeField] private type itemType;
    private enum rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    private enum type
    {
        None,
        Material,
        Weapon,
        Consumable,
        Treause
    }
}
