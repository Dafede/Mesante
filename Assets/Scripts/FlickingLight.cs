using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickingLight : MonoBehaviour
{
    public Light mLight;
    public Light mLight2;
    float minIntensity = 0.4f;
    float maxIntensity = 1;
    public AudioSource lightSound;

    void Start(){
       maxIntensity = mLight.intensity;
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
                mLight2.intensity = maxIntensity;
                if(lightSound!=null)lightSound.volume = maxIntensity;
            }else{
                mLight.intensity = minIntensity;
                mLight2.intensity = minIntensity;
                if(lightSound!=null)lightSound.volume = minIntensity;
            }
			//mLight.intensity = Random.Range(minIntensity,maxIntensity);
		}
	}
}
