using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class RaycastCamera : MonoBehaviour{
    
    RaycastHit hit;
    public Camera camera;
    public Image pointer;
    MessagerObject messageObjectComponent;
    MessagerObject messageObjectComponent2;
    KeyLightToogle lightScriptComponent;
    KeyLightToogle lightScriptComponent2;
    PickableController pickableComponent;
    PickableController pickableComponent2;
    DoorController doorComponent;
    DoorController doorComponent2;
    Ray ray;
    Vector3 forward;
    Transform objectHit;
    private GameObject heldObj;
    public Transform holdParent;
    float moveForce = 50;
    bool picked = false;
    GameObject gameObjectPicked;
    public FirstPersonController fpc;
    int typeMessageInScreen = 0;

    bool resetMessage = false;

    void Start(){
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_DropObject");
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_MessageInScreen");
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_ButtonMessageSystemSelected");
    }

void Update(){
            if(resetMessage) resetMessage = false;
            //
            //Here the list of interactable objects to reset
            //
            if(messageObjectComponent2 != null){
                pointer.color = Color.white;
                pointer.rectTransform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                messageObjectComponent2 = null;
            }
            if(lightScriptComponent2 != null){
                lightScriptComponent.OutlineOFF();
                lightScriptComponent2 = null;
            }
            if(pickableComponent2 != null){
                pickableComponent.OutlineOFF();
                pickableComponent2 = null;
            }
            if(doorComponent2 != null){
                doorComponent.OutlineOFF();
                doorComponent2 = null;
            }
            //Only if the message is TYPE = 1
            if (Input.GetMouseButtonDown(0) && typeMessageInScreen == 1){
                NotificationCenter.DefaultCenter ().PostNotification (this, "N_DisableTextContainer", ""+typeMessageInScreen);
                fpc.enabled = true;
                typeMessageInScreen = 0;
                resetMessage = true;
            }
            //
            //Components of the raycast
            //
            ray = camera.ScreenPointToRay(Input.mousePosition);
            forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.green);
            int layer_mask = LayerMask.GetMask("Cast Raycast");
            //
            // *** RAYCAST ***
            // Here the list of interactable objects with the raycast
            //
            
            if (Physics.Raycast(ray, out hit, 8,layer_mask) ) {
                objectHit = hit.transform;
                if(Input.GetMouseButtonDown(0)){
                    Debug.Log("objectHit.tag: "+objectHit.tag);
                }

                //
                //Outline Interactions list
                //
                if(objectHit.tag == "ObjectMessage"){
                    messageObjectComponent = objectHit.gameObject.GetComponent<MessagerObject>();
                    if(messageObjectComponent != null){
                        pointer.color = Color.red;
                        pointer.rectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    } 
                    messageObjectComponent2 = messageObjectComponent;
                }
                if(objectHit.tag == "Light"){
                    lightScriptComponent = objectHit.gameObject.GetComponent<KeyLightToogle>();
                    if(lightScriptComponent != null) lightScriptComponent.OutlineON();
                    lightScriptComponent2 = lightScriptComponent;
                }
                if(objectHit.tag == "Door"){
                    doorComponent = objectHit.gameObject.GetComponent<DoorController>();
                    if(doorComponent != null) doorComponent.OutlineON();
                    doorComponent2 = doorComponent;
                }
                if(objectHit.tag == "Pickable" ){
                    if(!picked){
                        pickableComponent = objectHit.gameObject.GetComponent<PickableController>();
                        if(pickableComponent != null)pickableComponent.OutlineON();
                        pickableComponent2 = pickableComponent;
                    }else{
                        pickableComponent = objectHit.gameObject.GetComponent<PickableController>();
                        if(pickableComponent != null)pickableComponent.OutlineONWithoutText();
                        pickableComponent2 = pickableComponent;                        
                    }

                }
                //
                //Other interactions
                //
                if(objectHit.tag == "ObjectMessage" && Input.GetMouseButtonDown(0) && typeMessageInScreen == 2){
                        //NotificationCenter.DefaultCenter ().PostNotification (this, "N_SpeedUpMessage");
                }
                if(objectHit.tag == "ObjectMessage" && Input.GetMouseButtonDown(0) && typeMessageInScreen == 0 && !resetMessage){

                    if(objectHit.gameObject.GetComponent<MessagerObject>() != null){
                        if(fpc.enabled == true){
                            objectHit.gameObject.GetComponent<MessagerObject>().showMessageTextSystem();
                            fpc.enabled = false;
                            
                        }
                    } 
                }

                if(objectHit.tag == "Light" && Input.GetMouseButtonDown(0)){
                    objectHit.gameObject.GetComponent<KeyLightToogle>().ToogleLight();
                }
                if(objectHit.tag == "Door" && Input.GetMouseButtonDown(0)){
                    objectHit.gameObject.GetComponent<DoorController>().ToogleDoor();
                }
                if(objectHit.tag == "Pickable" && Input.GetMouseButtonDown(0)){
                    if(heldObj == null){
                        PickupObject(objectHit.gameObject);
                    }else{
                        DropObject();
                    }                    
                }
            }

}

  public void N_ButtonMessageSystemSelected(Notification notification){
        
        string dataFrom = (string)notification.data;
        fpc.enabled = true;
        typeMessageInScreen = 0;

  }

public void N_MessageInScreen(Notification notification){
    string dataFrom = (string)notification.data;
    typeMessageInScreen = int.Parse(dataFrom);
    Debug.Log("TYPE IN RAYCAST: "+typeMessageInScreen);
}
    void MoveObjectHold(){
        if(Vector3.Distance(heldObj.transform.position, holdParent.position)>0.1f){
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void DropObject(){
        if(picked){
            Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
            heldRig.useGravity = true;
            heldObj.transform.parent = null;
            heldObj = null;
            picked = false;
            gameObjectPicked = null;
            heldRig.constraints = RigidbodyConstraints.None;
        }
    }

    void N_DropObject(){
        DropObject();
    }

    void PickupObject (GameObject pickObj){
        if(pickObj.GetComponent<Rigidbody>()){
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            BoxCollider objColl = pickObj.GetComponent<BoxCollider>();
            gameObjectPicked = pickObj;
            objRig.useGravity = false;
            objRig.transform.position = holdParent.transform.position;
            objRig.transform.parent = holdParent;
            heldObj = pickObj;
            picked = true;
            objRig.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    IEnumerator WaitAndDropPickable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(picked){DropObject();}
    }
}