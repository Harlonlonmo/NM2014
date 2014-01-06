using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class MeshEditor : EditorWindow
{
    [MenuItem("Window/MeshEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MeshEditor window = (MeshEditor)GetWindow(typeof(MeshEditor));
    }

    private bool selection = false;

    private bool SharedMesh = true;

    private MeshFilter f = null;
    void OnSelectionChange()
    {
        GameObject g = Selection.activeGameObject;
        if (g == null)
        {
            selection = false;
            f = null;
        }
        else
        {
            f = g.transform.GetComponent<MeshFilter>();
            selection = f != null;
        }
        Repaint();
    }

    void OnGUI()
    {
        GUI.enabled = selection;
        SharedMesh = GUI.Toggle(new Rect(0, 0, 200, 20), SharedMesh, "SharedMesh");
        if (GUI.Button(new Rect(0, 30, 200, 25), "FlipNormals"))
        {
            FlipNormals(SharedMesh ? f.sharedMesh : f.mesh);
        }
        if (GUI.Button(new Rect(0, 60, 200, 25), "Make new instance of mesh"))
        {
            CopyMesh(f);
        }
        GUI.enabled = true;
    }

    private void FlipNormals(Mesh mesh)
    {
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        mesh.normals = normals;

        for (int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            mesh.SetTriangles(triangles, m);
        }
    }

    private void CopyMesh(MeshFilter mesh)
    {
        MeshExporter.SimpleExport(mesh);
    }

    
}

