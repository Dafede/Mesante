using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextSystemController : MonoBehaviour
{
    public AudioClip keyStroke; 
    public AudioSource textSystemAudioSource;
    public TMP_Text textLabel;
    public TMP_Text textLabelSelect;
    public GameObject imageContainer;
    int cont = 0;
    bool nextMessage = false;
    int type = 0;
    public AudioClip buttonHover; 
    string currentText = "";
    private float typeWriterSpeed = 50f;
    private float typeSpeed = 0.03f;

    void Start(){
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_TextSystemController_ShowText");
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_DisableTextContainer");
        NotificationCenter.DefaultCenter ().AddObserver (this, "N_SpeedUpMessage");
    }

    public void N_DisableTextContainer(Notification notification){
        string dataFrom = (string)notification.data;
        int type = int.Parse(dataFrom);
        switch (type){
        case 1:
            imageContainer.transform.GetChild(1).gameObject.SetActive(false);
            break;
        case 2:
            imageContainer.transform.GetChild(2).gameObject.SetActive(false);
            break;
        default:
            break;
        }
        
    }

    public void N_SpeedUpMessage( ){
        typeSpeed = 0.08f;
    }
    public void N_TextSystemController_ShowText(Notification notification){
        
        string dataFrom = (string)notification.data;
        string[] nameParts = dataFrom.Split(';');
        string text = nameParts[0];
        int type = int.Parse(nameParts[1]);
        NotificationCenter.DefaultCenter ().PostNotification (this, "N_MessageInScreen", ""+type);
        switch (type){
        case 1:
            imageContainer.transform.GetChild(1).gameObject.SetActive(true); 
            textLabel.text = "";
            runTextToShow(text, textLabel);
            break;
        case 2:
            imageContainer.transform.GetChild(2).gameObject.SetActive(true); 
            textLabelSelect.text = "";
            runTextToShow(text, textLabelSelect);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            break;
        default:
            break;
        }

    }

    public void runTextToShow(string textToType, TMP_Text textLabelMain){
        //StartCoroutine(TypeText(textToType, textLabelMain));
        
        StartCoroutine(ShowTypeText(textToType, textLabelMain));
    }
    void playKeyStroke(){
        textSystemAudioSource.clip = keyStroke;
        textSystemAudioSource.pitch = 1 + Random.Range(-0.1f,0.1f);
        textSystemAudioSource.Play ();
    }

    public void YESclicked(){
        Debug.Log("YES");
        imageContainer.transform.GetChild(2).gameObject.SetActive(false);
        NotificationCenter.DefaultCenter ().PostNotification (this, "N_ButtonMessageSystemSelected","yes");
    }

    public void NOclicked(){
        Debug.Log("NO");
        imageContainer.transform.GetChild(2).gameObject.SetActive(false);
        NotificationCenter.DefaultCenter ().PostNotification (this, "N_ButtonMessageSystemSelected","no");
    }
    
    public void HoverButton(){
        textSystemAudioSource.clip = buttonHover;
        textSystemAudioSource.PlayOneShot(textSystemAudioSource.clip);
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabelMainFinal){
        float t = 0;
        int charIndex = 0;
        int charIndexAnt = -1;
        while(charIndex < textToType.Length-1){
            t += Time.deltaTime  * typeWriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex,0,textToType.Length);
            //textLabel.text = textToType.Substring(0,charIndex); // inicio y longitud... no puedo cambiar por inicio y fin
            if(charIndexAnt != charIndex){
                textLabelMainFinal.text += textToType.Substring(charIndex,1);
                charIndexAnt = charIndex;
                if(cont%2==0)playKeyStroke();
                cont++;
            }

            yield return null;
        }
        //textLabel.text = textToType;
        nextMessage = true;
        //nextCanvas.SetActive(true);
        cont=0;
    }


    private IEnumerator ShowTypeText(string textToType, TMP_Text textLabelMainFinal){
        for (int i=0; i<textToType.Length+1; i++){
            currentText = textToType.Substring(0,i);
            textLabelMainFinal.text = currentText;
            if(i%2==0)playKeyStroke();
            yield return new WaitForSeconds(typeSpeed);
        }
        nextMessage = true;
    }
}
