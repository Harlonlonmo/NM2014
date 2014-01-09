using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour
{

    public Transform IceCubePosition;
    public Transform Target;
    public float speed;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    private float prevMag = float.MaxValue;

    void Update()
    {

        Vector3 rel = Target.position - transform.position;
        float mag = rel.magnitude;
        if (mag > prevMag)
        {
            DestroyImmediate(transform.gameObject);
            return;
        }
        prevMag = mag;
        transform.Translate(rel.normalized*speed*Time.deltaTime);

    }
}
