using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    int volume =0;
    public AudioSource audioSource;
    public AudioSource musicSource;
    public AudioClip slide;
    public AudioClip countdown;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip click;

    // Para añadir nuevo sonido, añadir nueva variable AUdioCLip aqui, asignarla en el el inspector y en la funcion
    // playSound añadir un nuevo case; finalmente para reproducir sonido ejecutar con numero correspondiente del sonido:
    // NotificationCenter.DefaultCenter().PostNotification(this, "playSound", "5");

    //Para crear el MIXER
    // Pulsar sobre la barra de archivo la opcion de Window y luego Audio > Audio Mixer
    // SI no hemos creado ningun Mixer, estara vacio. Para crear uno nuevo le damos a el simbolo del +
    // y le damos un nombre "MainMixer"
    // Ahora seleccionamos el Mixer Master y en el Inspector boton derecho soble la label de Volume
    // y le damos a Expose (primera opcion (?))
    // Ahora e la ventana del Master del Mixer abra un desplegable "Exposed Parameters (1)" si clicamos
    // se abrira un desplegable con una pcion (el volumen) podremos darle clic derecho y cambiarle el nombre
    // por algo como "volume"
    // simplemente ahora asignar las variables publicas de este script

    //Finalmente asignar los audio sources que quieras que usen los MIXER
    
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "changedVolume");
        NotificationCenter.DefaultCenter().AddObserver(this, "playSound");  
        
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }    

    public void changedVolume(Notification notification){
        string dataString = notification.data.ToString();
        volume = int.Parse(dataString);
        changeVolumeToMixer(volume);
        SetVolume(volume);
    }

    public void playSound(Notification notification){
        string dataString = notification.data.ToString();
        switch (int.Parse(dataString)) {
            case 1:
                audioSource.clip = slide;
                break;
            case 2:
                audioSource.clip = countdown;
                break;
            case 3:
                audioSource.clip = correct;
                break;
            case 4:
                audioSource.clip = wrong;
                break;
            case 5:
                audioSource.clip = click;
                break;
            default:
                break;
        }
        audioSource.Play();
    }
    public void playMusic(Notification notification){
        string dataString = notification.data.ToString();
        switch (int.Parse(dataString)) {
            case 1:
                //musicSource.clip = music01;
                break;
            default:
                break;
        }
        musicSource.Play();
    }


    public void changeVolumeToMixer(int nVolume){
        switch (nVolume) {
            case 100:
                volume = 0;
               break;
            case 90:
                volume = -10;
                break;
            case 80:
                volume = -15;
                break;
            case 70:
                volume = -20;
                break;
            case 60:
                volume = -25;
                break;
            case 50:
                volume = -30;
                break;
            case 40:
                volume = -40;
                break;
            case 30:
                volume = -50;
                break;
            case 20:
                volume = -60;
                break;
            case 10:
                volume = -70;
                break;
            case 0:
                volume = -80;
                break;
            default:
                break;
        }
        
    }
    
}
