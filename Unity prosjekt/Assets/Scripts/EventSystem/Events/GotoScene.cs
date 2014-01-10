using UnityEngine;

[System.Serializable]
public class GotoScene : Event
{
    public string LevelName;

    public override void Activate()
    {
        TransportInfo.levelName = LevelName;
        Application.LoadLevel("Loading");
    }
}
