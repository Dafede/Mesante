using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{

    public void OutlineON(){
        if(gameObject.transform.childCount==0){
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        }
        if(gameObject.transform.childCount==1){
            gameObject.transform.GetChild(0).gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        }
        

        NotificationCenter.DefaultCenter ().PostNotification (this, "N_SetActionText", "PICK");
    }
    public void OutlineONWithoutText(){
        if(gameObject.transform.childCount==0){        
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        }
        if(gameObject.transform.childCount==1){        
            gameObject.transform.GetChild(0).gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        }
    }

    public void OutlineOFF(){
        if(gameObject.transform.childCount==0){
            gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        }
        if(gameObject.transform.childCount==1){
            gameObject.transform.GetChild(0).gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        }
        NotificationCenter.DefaultCenter ().PostNotification (this, "N_SetActionText", "");
    }

    private void OnTriggerExit(Collider collider){
        if(collider.tag == "Insider"){
            NotificationCenter.DefaultCenter ().PostNotification (this, "N_DropObject");
        }
    }

}
