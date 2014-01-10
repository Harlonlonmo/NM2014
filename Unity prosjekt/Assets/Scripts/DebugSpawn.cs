using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class DebugSpawn : MonoBehaviour
{

    public LevelSpawnHandler handler;
    public bool active = false;
    public int i;

    // Update is called once per frame
    void Update()
    {
        if (handler && active)
        {
            Debug.Log("Goto: " + i);
            if (i < handler.SpawnPoints.Length)
            {
                transform.position = handler.SpawnPoints[i].position;
                transform.rotation = handler.SpawnPoints[i].rotation;
            }
        }
    }
}
