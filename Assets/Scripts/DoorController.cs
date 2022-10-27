using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Quaternion tofinish;
    Quaternion basicState;
    string currentStatusDoorText = "OPEN";
    bool statusDoor = false;
    AudioSource doorAudioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public float openAngle = 60f;

    void Start(){
        doorAudioSource = this.GetComponent<AudioSource>();
        basicState = transform.rotation;
    }
    void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation, tofinish, 10 * Time.deltaTime);
    }

   public void OutlineON(){
        gameObject.transform.GetChild(0).gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        NotificationCenter.DefaultCenter ().PostNotification (this, "N_SetActionText", currentStatusDoorText);
    }
    public void OutlineONWithoutText(){
        gameObject.transform.GetChild(0).gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
    }

    public void OutlineOFF(){
        gameObject.transform.GetChild(0).gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        NotificationCenter.DefaultCenter ().PostNotification (this, "N_SetActionText", "");
    }

    public void playSoundDoorOpen(){
        doorAudioSource.clip = doorOpenSound;
        doorAudioSource.pitch = 1 + Random.Range(-0.15f,0.15f);
        doorAudioSource.PlayOneShot(doorAudioSource.clip);
    }
    public void playSoundDoorClose(){
        doorAudioSource.clip = doorCloseSound;
        doorAudioSource.pitch = 1 + Random.Range(-0.15f,0.15f);
        doorAudioSource.PlayOneShot(doorAudioSource.clip);
    }

    public void ToogleDoor(){
        if(!statusDoor){
            currentStatusDoorText = "CLOSE";
            statusDoor = true;
            /*
            tofinish = transform.rotation;
            tofinish.w = tofinish.y -0.3f;
            */
            tofinish = Quaternion.Euler(new Vector3 (transform.rotation.x,openAngle,transform.rotation.z));
            playSoundDoorOpen();
        }else{
            statusDoor = false;
            currentStatusDoorText = "OPEN";
            tofinish = basicState;
            playSoundDoorClose();
        }
    }
}
