using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class MainMenuController : MonoBehaviour
{
    public AudioClip[] steps;
    public AudioSource mainMenuAudioSource;
    public AudioClip buttonHover; 
    public AudioClip openDoor; 
    public AudioClip buttonSelect; 
    Animator m_Animator;
    public GameObject activateCinematicController;
    public FirstPersonController fpc;
    public GameObject canvasMainMenu;


    void Start(){
        m_Animator = gameObject.GetComponent<Animator>();
        //*************m_Animator.SetBool("playButton", false);
        m_Animator.SetBool("openDoor", false);
        m_Animator.SetBool("menuFadeOut", false);
        fpc.setJumpBob(0, 0);
    }

    public void HoverButton(){
        mainMenuAudioSource.clip = buttonHover;
        mainMenuAudioSource.PlayOneShot(mainMenuAudioSource.clip);
    }

    public void PlayButtonPressed(){
        //m_Animator.SetBool("playButton", true);
        mainMenuAudioSource.clip = buttonSelect;
        mainMenuAudioSource.PlayOneShot(mainMenuAudioSource.clip);
        fpc.enabled = true;
        m_Animator.SetBool("menuFadeOut", true);
        //fpc.setJumpBob(0.2f, 0.1f);
    }

    public void PlayDoorOpenSound(){
        mainMenuAudioSource.clip = openDoor;
        mainMenuAudioSource.PlayOneShot(mainMenuAudioSource.clip);

    }
    public void FinishedPlayAnimation(){
        activateCinematicController.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OptionsButtonPressed(){

    }

    public void activateFPC(){
        fpc.enabled = true;
    }
    public void deactivateFPC(){
        fpc.enabled = false;
    }

    public void menuFadeOutFinish(){
        fpc.setGoOn();
        canvasMainMenu.SetActive(false);
        m_Animator.SetBool("menuFadeOut", false);
    }

    public void playStepSound(){
        mainMenuAudioSource.clip = steps[Random.Range (0, steps.Length)];
        mainMenuAudioSource.pitch = 1 + Random.Range(-0.1f,0.1f);
        mainMenuAudioSource.Play ();
    }

    public void QuitButtonPressed(){
        Application.Quit();
    }

    public void testButton(){
        
    }

    private void OnTriggerEnter(Collider other){
        fpc.setFullStop();
        m_Animator.SetBool("openDoor", true);
    }
}
