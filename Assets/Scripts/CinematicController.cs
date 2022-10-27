using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CinematicController : MonoBehaviour
{
    public AudioClip keyStroke; 
    public AudioSource cinematicAudioSource;
    Animator m_Animator;
    public TMP_Text textLabel_Intro;
    private float typeWriterSpeed = 20f;
    public GameObject nextCanvas;
    bool nextMessage = false;
    int cont = 0;
    int numAnim = 1;
    string starter_text_EN1 = "After a series of macabre murders, the police find a clue that gives the identity of the murderer.\n";
    string starter_text_EN2 = "\nAs part of the homicide force, you are sent to investigate the murderer's address to find any clue of his whereabouts.\n";
    string starter_text_EN3 = "\nTime runs against you to avoid more victims and to finally be able to stop the murderer.";
    string starter_text_ES1 = "Tras una serie de macabros asesinatos, la policia encuentra una pista que da con la identidad del asesino.\n";
    string starter_text_ES2 = "\nComo parte de del cuerpo de homicidios, eres enviado a investigar el domicilio del asesino para encontrar alguna pista de su paradero.\n";
    string starter_text_ES3 = "\nEl tiempo corre en tu contra para evitar más victimas y poder por fin detener al homicida.";


    public GameObject activate01;
    public GameObject activate02;
    public GameObject activate03;
    public GameObject activate04;
    public GameObject activate05;



    string currentText = "";
    private float typeSpeed = 0.03f;


    void Start(){
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool("Cinematic_FadeIn", false);
        m_Animator.SetBool("Cinematic_IN", true);        
    }

    public void cinematic_IN_finished(){
        m_Animator.SetBool("Cinematic_IN", false);
        Cinematic_Starter_ShowTextIntro01();// SIN CINEMATIC
    }

    void Update() {
        if(Input.GetMouseButtonDown(0) && !nextMessage){
            typeWriterSpeed = 40f;
            typeSpeed = 0.08f;
        }
            if(Input.GetMouseButtonDown(0) && nextMessage){
                typeWriterSpeed = 20f;
                typeSpeed = 0.03f;
                nextMessage = false;
                nextCanvas.SetActive(false);
                
                switch (numAnim){
                case 1:
                    Cinematic_Starter_ShowTextIntro02();
                    numAnim++;
                    break;
                case 2:
                    Cinematic_Starter_ShowTextIntro03();
                    numAnim++;
                    break;
                case 3:
                    m_Animator.SetBool("Cinematic_FadeIn", true);
                    break;
                default:
                    break;
                }

            }

    }

    public void cinematicFadeInFinish(){
          gameObject.SetActive(false);
                   activate02.SetActive(true);
    }
    public void cinematicFadeIn(){
                    activate01.SetActive(true);
                    //activate02.SetActive(true);
                    activate03.SetActive(true);
                    activate04.SetActive(true);
                    activate05.SetActive(true);
                   
    }

    void playKeyStroke(){
        cinematicAudioSource.clip = keyStroke;
        cinematicAudioSource.pitch = 1 + Random.Range(-0.1f,0.1f);
        cinematicAudioSource.Play ();
    }


    //Called from the Animator
    public void Cinematic_Starter_ShowTextIntro01(){
        
        CinematicController_ShowText(starter_text_ES1,textLabel_Intro);
   
    }
    //Called from the Animator
    public void Cinematic_Starter_ShowTextIntro02(){

        CinematicController_ShowText(starter_text_ES2,textLabel_Intro);

      
    }
    //Called from the Animator
    public void Cinematic_Starter_ShowTextIntro03(){
        CinematicController_ShowText(starter_text_ES3,textLabel_Intro);
    }

    public void CinematicController_ShowText(string textMessage, TMP_Text textLabel){
        StartCoroutine(TypeText(textMessage, textLabel));
        //StartCoroutine(ShowTypeText(textMessage, textLabel));
    }


    private IEnumerator TypeText(string textToType, TMP_Text textLabel){
        float t = 0;
        int charIndex = 0;
        int charIndexAnt = -1;
        while(charIndex < textToType.Length-1){
            t += Time.deltaTime  * typeWriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex,0,textToType.Length);
            //textLabel.text = textToType.Substring(0,charIndex); // inicio y longitud... no puedo cambiar por inicio y fin
            if(charIndexAnt != charIndex){
                textLabel.text += textToType.Substring(charIndex,1);
                charIndexAnt = charIndex;
                if(cont%2==0)playKeyStroke();
                cont++;
            }

            yield return null;
        }
        //textLabel.text = textToType;
        nextMessage = true;
        nextCanvas.SetActive(true);
        cont=0;
    }


    private IEnumerator ShowTypeText(string textToType, TMP_Text textLabelMainFinal){
        for (int i=0; i<textToType.Length; i++){
            currentText = textToType.Substring(0,i);
            textLabelMainFinal.text = currentText;
            if(i%2==0)playKeyStroke();
            yield return new WaitForSeconds(typeSpeed);
        }
        nextMessage = true;
    }

}
