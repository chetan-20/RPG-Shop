using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGen : MonoBehaviour
{
   public void GenerateCoin()
    {
        if (GameService.Instance.PlayerManager.playerMoney == 0)
        {
            GameService.Instance.PlayerManager.playerMoney = Random.Range(35,65);
        }
        GameService.Instance.PlayerManager.UpdateCredits();
    }
}
