using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] internal TMP_Text popUpMsgText;
    [SerializeField] internal GameObject popUpMsgGameobject;
    [SerializeField] internal SoundManager soundManager;
   
    internal float delay = 3f;

    public static GameService Instance { get { return instance; } }
    private static GameService instance;
    
    public ShopManager ShopManager { get { return shopManager; } }  
    public PlayerManager PlayerManager { get { return playerManager; } }  
    public SoundManager SoundManager { get { return soundManager; } }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        popUpMsgGameobject.SetActive(false);
    }
    private void Update()
    {
        onEscapePress();
    }
    public IEnumerator DisableAfterDelay()
    {
       yield return new WaitForSeconds(delay);
       popUpMsgGameobject.SetActive(false);
    }
    public void ShowPopupMessage(string message)
    {
       popUpMsgGameobject.SetActive(true);
       popUpMsgText.text = message;
       StartCoroutine(DisableAfterDelay());
    }
    private void onEscapePress()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();           
        }      
    }

}
