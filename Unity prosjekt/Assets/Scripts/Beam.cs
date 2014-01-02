using UnityEngine;
using System.Collections;


[RequireComponent(typeof(LineRenderer))]
public class Beam : MonoBehaviour
{

    public float stepSize = 0.1f;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void render(Vector3 start, Vector3 end)
    {
        Vector3 rel = end - start;
        Vector3 norm = rel.normalized;
        int i = 0;
        for (float d = 0; d < rel.magnitude; d += stepSize)
        {
            line.SetPosition(i++, start + d*norm);
        }
    }
}
