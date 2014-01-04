using UnityEngine;
using System.Collections;


[RequireComponent(typeof(LineRenderer))]
public class LineBeam : Beam
{

    public float stepSize = 0.1f;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public override void Render(Vector3 start, Vector3 end)
    {
        
        Vector3 rel = end - start;
        Vector3 norm = rel.normalized;
        int verts = (int) (rel.magnitude/stepSize)+1;
        line.SetVertexCount(verts);
        for (int i = 0; i < verts; i++)
        {
            line.SetPosition(i, start + i*norm);
        }
        line.SetPosition(verts-1, end);
    }

    public override void SetEnabled(bool enabled)
    {
        line.enabled = enabled;
    }
}
