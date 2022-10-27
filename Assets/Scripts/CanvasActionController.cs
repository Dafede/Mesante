using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasActionController : MonoBehaviour
{
    public TMP_Text textAction;

    // Start is called before the first frame update
    void Start()
    {
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_SetActionText");
    }


    public void N_SetActionText(Notification notification)
    {
        string text = (string)notification.data;
        textAction.text = text;
    }
}
