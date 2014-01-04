using UnityEngine;
using System.Collections;

public class UnderwaterScript : MonoBehaviour
{

    //This script enables underwater effects. Attach to main camera.

    //Define variable
    public Transform underwaterLevel;

    //The scene's default fog settings
    private bool defaultFog = RenderSettings.fog;
    private Color defaultFogColor = RenderSettings.fogColor;
    private float defaultFogDensity = RenderSettings.fogDensity;
    private Material defaultSkybox = RenderSettings.skybox;
    private Material noSkybox;

    public Color UnderWaterColor = new Color(0, 0.4f, 0.7f, 0.6f);
    public float UnderWaterFogDensity = 0.04f;

    void Start()
    {
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
