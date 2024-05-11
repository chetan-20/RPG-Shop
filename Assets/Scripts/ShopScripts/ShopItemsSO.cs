using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="NewShopItem",menuName ="ShopItem")]
public class ShopItemsSO : ScriptableObject
{
 
    [SerializeField] internal Sprite icon;
    [SerializeField] internal string itemDescription;
    [SerializeField] internal int buyingPrice;
    [SerializeField] internal float weight;    
    [SerializeField] internal int quantity;
    [SerializeField] internal rarity itemRarity;
    [SerializeField] internal type itemType;
    internal enum rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    internal enum type
    {       
        Material,
        Weapon,
        Consumable,
        Treause
    }
}
