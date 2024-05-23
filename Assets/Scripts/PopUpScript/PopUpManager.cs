using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] internal TMP_Text popUpMsgText;
    [SerializeField] internal GameObject popUpMsgGameobject;

    internal float popUpMsgDelay = 3f;

    private void Start()
    {
        popUpMsgGameobject.SetActive(false);
    }
   
    public IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(popUpMsgDelay);
        popUpMsgGameobject.SetActive(false);
    }
    public void ShowPopupMessage(string message)
    {
        popUpMsgGameobject.SetActive(true);
        popUpMsgText.text = message;
        StartCoroutine(DisableAfterDelay());
    }
   
}
