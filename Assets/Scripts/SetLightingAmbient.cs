using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLightingAmbient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = Color.black;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
