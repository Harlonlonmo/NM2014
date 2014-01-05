using UnityEngine;
using System.Collections;

public class UnderwaterScript : MonoBehaviour
{

    //This script enables underwater effects. Attach to main camera.

    //Define variable
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

    void Update()
    {
        if (transform.position.y < underwaterLevel.position.y)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = UnderWaterColor;
            RenderSettings.fogDensity = UnderWaterFogDensity;
            RenderSettings.skybox = noSkybox;
        }
        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
        }
    }
}
