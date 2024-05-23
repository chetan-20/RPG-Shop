
using UnityEngine;

[CreateAssetMenu(fileName="NewShopItem",menuName ="ShopItem")]
public class ShopItemsSO : ScriptableObject
{ 
    [SerializeField] public Sprite icon;
    [SerializeField] public string itemDescription;
    [SerializeField] public int buyingPrice;
    [SerializeField] public float weight;    
    [SerializeField] public int quantity;
    [SerializeField] public rarity itemRarity;
    [SerializeField] public type itemType;
    [SerializeField] public int uniqueID;

    public enum rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    public enum type
    {       
        Material,
        Weapon,
        Consumable,
        Treause
    }
}
