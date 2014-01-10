using UnityEngine;
using System.Collections;

public class SubmergeHandler : MonoBehaviour {

    public bool OnlyFrost;

    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;
    private FrostEffect frost;
    private bool freeze; 

    public Camera TargetCamera; 
    public Color UnderWaterColor = new Color(0, 0.4f, 0.7f, 0.6f);
    public float UnderWaterFogDensity = 0.04f;
    public float MaxFreeze;
    public float FreezeSpeed; 

    void Start()
    {
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultSkybox = RenderSettings.skybox;
        //Set the background color
        //TargetCamera.backgroundColor = new Color(0, 0.4f, 0.7f, 1);           //TODO fix

        frost = TargetCamera.GetComponent<FrostEffect>(); 
    }

	// This method get's called when the object gets submerged in water (enter underwater trigger zone) 
    public void Submerge()
    {
        freeze = true;

        if (OnlyFrost) return;

        // Water Effect
        RenderSettings.fog = true;
        RenderSettings.fogColor = UnderWaterColor;
        RenderSettings.fogDensity = UnderWaterFogDensity;
        RenderSettings.skybox = noSkybox;
        
    }

    void Update()
    {
        if (!freeze)
        {
            if (frost.FrostAmount > 0) frost.FrostAmount -= Time.deltaTime*FreezeSpeed;
        }
        else if (frost.FrostAmount < MaxFreeze) frost.FrostAmount += Time.deltaTime*FreezeSpeed;
    }

    public void Emerge()
    {
        freeze = false; 

        if (OnlyFrost) return;

        // Water Effect
        RenderSettings.fog = defaultFog;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
        RenderSettings.skybox = defaultSkybox;
    }
}
