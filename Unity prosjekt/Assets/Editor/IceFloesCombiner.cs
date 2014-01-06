using UnityEditor;
using UnityEngine;
using System.Collections;

public class IceFloesCombiner : EditorWindow {

    [MenuItem("Window/IceFloasScript")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        IceFloesCombiner window = (IceFloesCombiner)GetWindow(typeof(IceFloesCombiner));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 25), "Combine iceFloasScripts"))
        {
            combineAll();
        }
        if (GUI.Button(new Rect(0, 30, 200, 25), "Rename numerical"))
        {
            string n = Selection.gameObjects[0].name;
            int i = 0;
            foreach (GameObject t in Selection.gameObjects)
            {
                t.name = n+i++;
            }
        }
    }

    private void combineAll()
    {
        foreach (Transform t in Selection.transforms)
        {
            CombineOne(t);
        }
    }
    private void CombineOne(Transform t)
    {
        var f = t.GetComponent<FreezeHandler>();
        var th = t.GetComponent<HitHandler>();

        if (f != null && th != null)
        {
            th.freezeEfects[0] = f;
        }
    }
}
