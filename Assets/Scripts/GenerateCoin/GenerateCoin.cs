
using UnityEngine;
using UnityEngine.UI;

public class GenerateCoid : MonoBehaviour
{
    [SerializeField] private Button generateCoinButton;

    private void Start()
    {
        generateCoinButton.onClick.AddListener(GenerateCoin);
    }


    private void GenerateCoin()
    {
        if (GameService.Instance.PlayerManager.playerMoney == 0 && GameService.Instance.PlayerManager.playerInventory.Count == 0)
        {
            GameService.Instance.PlayerManager.playerMoney += UnityEngine.Random.Range(65, 75);
            GameService.Instance.SoundManager.PlaySound(Sounds.GenerateMoneySound);
            GameService.Instance.PlayerManager.UpdateCredits();
        }
        else
        {
            GameService.Instance.SoundManager.PlaySound(Sounds.CantBuyorSellSound);
            GameService.Instance.PopUpManager.ShowPopupMessage("Cant Generate More Coin");
        }

    }
}
