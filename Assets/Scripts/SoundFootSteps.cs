using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFootSteps : MonoBehaviour
{

    public AudioClip[] steps;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            audioSource.mute = false;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)){
            audioSource.mute = true;
        }
    }
    public void playSoundStep(){
		audioSource.clip = steps[Random.Range (0, steps.Length)];
        audioSource.pitch = 1 + Random.Range(-0.1f,0.1f);
        audioSource.Play ();
    }
}
