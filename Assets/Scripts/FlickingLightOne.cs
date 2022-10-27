using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickingLightOne : MonoBehaviour
{
    public Light mLight;
    public Light secondLight;
    float minIntensity;
    float maxIntensity;
    public AudioSource lightSound;
    private float minimunIntensity = 0.4f;

    void Start(){
        maxIntensity = mLight.intensity;
        minIntensity = minimunIntensity;      
       StartCoroutine (Flashing ());
    }

    IEnumerator Flashing(){
		while (true) {
                if(mLight.intensity == minIntensity){
                    yield return new WaitForSeconds (Random.Range(0.1f,0.5f));
                }else{
                    yield return new WaitForSeconds (Random.Range(3,5));
                }
                if(mLight.intensity == minIntensity){
                    mLight.intensity = maxIntensity;
                    secondLight.intensity = maxIntensity;
                    if(lightSound!=null)lightSound.volume = maxIntensity;
                }else{
                    mLight.intensity = minIntensity;
                    secondLight.intensity = minIntensity;
                    if(lightSound!=null)lightSound.volume = minIntensity;
                }
            
		}
	}
}
