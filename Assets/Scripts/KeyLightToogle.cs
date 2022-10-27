using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyLightToogle : MonoBehaviour
{
    public GameObject[] myLight;
    public AudioClip audioON;
    public AudioClip audioOFF;
    public AudioSource audio;
    public TMP_Text textLight;
    public Material materialLampON;
    public Material materialLampOFF;
    Material[][] materials;
    public GameObject[] LampElementEmitter;
	public int type = 0;
	Material[] auxMaterials;

    void Start(){
        //audio = GetComponent<AudioSource>();
        textLight.text = "";
		materials = new Material[LampElementEmitter.Length][];
		if(!myLight[0].activeSelf){
			for(int i=0; i<LampElementEmitter.Length;i++){
			if(type == 0){
				auxMaterials = LampElementEmitter[i].GetComponent<MeshRenderer>().materials;
				materials[i] = auxMaterials;
				materials[i][0] = materialLampOFF;
				LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
			}
			if(type == 1){
				auxMaterials = LampElementEmitter[i].GetComponent<MeshRenderer>().materials;
				materials[i] = auxMaterials;
				materials[i][1] = materialLampOFF;
				LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
			}	
		}
		}else{
			for(int i=0; i<LampElementEmitter.Length;i++){
			if(type == 0){
				auxMaterials = LampElementEmitter[i].GetComponent<MeshRenderer>().materials;
				materials[i] = auxMaterials;
				materials[i][0] = materialLampON;
				LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
			}
			if(type == 1){
				 
				auxMaterials = LampElementEmitter[i].GetComponent<MeshRenderer>().materials;
				materials[i] = auxMaterials;
				materials[i][1] = materialLampON;
				LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
			}	
		}
		}

        
    }

    public void OutlineON(){
        gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = false;
        setTextON();
    }

    public void OutlineOFF(){
        gameObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        setTextOFF();
    }

    void setTextON(){
		for(int i=0; i<myLight.Length;i++){
			if (myLight[i].activeSelf){
				textLight.text = "OFF";
			}else{
				textLight.text = "ON";
			}
		}

    }
    void setTextOFF(){
            textLight.text = "";
    }

    public void ToogleLight(){
			if (myLight[0].activeSelf){
				audio.PlayOneShot(audioOFF, 0.7f);
				textLight.text = "OFF";
			}else{
				audio.PlayOneShot(audioON, 0.7f);
				textLight.text = "ON";
			}
		for(int i=0; i<myLight.Length;i++){
			
			if (myLight[i].activeSelf){
				myLight[i].SetActive(false);
				if(type == 0){
					materials[i][0] = materialLampOFF;
					LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
				}
				if(type == 1){
					materials[i][1] = materialLampOFF;
					LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
				}
			}else{
				myLight[i].SetActive(true);
				if(type == 0){
					materials[i][0] = materialLampON;
					LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
				}
				if(type == 1){
					materials[i][1] = materialLampON;
					LampElementEmitter[i].GetComponent<MeshRenderer>().materials = materials[i];
				}				
			}
		}
    }
}
