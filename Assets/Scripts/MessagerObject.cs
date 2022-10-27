using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagerObject : MonoBehaviour
{

    public string message = "";
    public int type = 0;
 

    public void showMessageTextSystem(){
        if(message != ""){
            NotificationCenter.DefaultCenter ().PostNotification (this, "N_TextSystemController_ShowText", message+";"+type);
        }
        
    }
}
