using UnityEngine;

[System.Serializable]
public class Quit : Event
{

    public override void Activate()
    {
        Application.Quit();
    }
}