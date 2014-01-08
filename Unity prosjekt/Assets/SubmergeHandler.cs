using UnityEngine;
using System.Collections;

public class SubmergeHandler : MonoBehaviour {

    public Transform underwaterLevel;

    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;

    public Color UnderWaterColor = new Color(0, 0.4f, 0.7f, 0.6f);
    public float UnderWaterFogDensity = 0.04f;

    void Start()
    {
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultSkybox = RenderSettings.skybox;
        //Set the background color
        camera.backgroundColor = new Color(0, 0.4f, 0.7f, 1);
    }

	// This method get's called when the object gets submerged in water (enter underwater trigger zone) 
    public void Submerge()
    {
        
    }

    public void Emerge()
    {
        
    }
}
