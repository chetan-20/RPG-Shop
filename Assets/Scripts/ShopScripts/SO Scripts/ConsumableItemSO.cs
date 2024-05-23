using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material Items", menuName = "Items/Consumable Items")]
public class ConsumableItemSO : ScriptableObject
{
    public ShopItemsSO[] consumableItems;
}
