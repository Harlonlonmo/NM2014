using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(SimpleMesh))]
public class SimpleMeshInspector : Editor
{

    private bool editing = false;

    private MeshFilter meshFilter;

    Tool LastTool = Tool.None;

    void OnEnable()
    {
        if (editing)
        {
            BeginEditing();
        }
        meshFilter = ((SimpleMesh)target).GetComponent<MeshFilter>();
    }

    private void BeginEditing()
    {
        LastTool = Tools.current;

        Tools.current = Tool.None;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Edit Mesh"))
        {
            editing ^= true;
            if (editing)
            {
                BeginEditing();
            }
            else
            {
                StopEditing();
            }
        }
        if (GUILayout.Button("Create new instance"))
        {
            if (CreateTargetFolder("Assets/SavedMeshes"))
            {
                AssetDatabase.CreateAsset(meshFilter.mesh, "Assets/SavedMeshes/" + meshFilter.name + "test.asset");
            }
        }
    }
    private static bool CreateTargetFolder(string folder)
    {
        try
        {
            System.IO.Directory.CreateDirectory(folder);
        }
        catch
        {
            EditorUtility.DisplayDialog("Error!", "Failed to create target folder!", "");
            return false;
        }

        return true;
    }

    public void OnSceneGUI()
    {
        if (editing)
        {
            Mesh mesh = meshFilter.sharedMesh;
            Vector3[] verts = mesh.vertices;
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = Handles.PositionHandle(verts[i] + ((SimpleMesh)target).transform.position, Quaternion.identity) - ((SimpleMesh)target).transform.position;
            }
            
            mesh.vertices = verts;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }
    }

    void OnDisable()
    {
        if (editing)
        {
            StopEditing();
        }
    }

    private void StopEditing()
    {
        Tools.current = LastTool;
    }
}
