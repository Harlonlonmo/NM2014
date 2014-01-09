using UnityEngine;
using System.Collections;

public class FreezeHandler : HitEfect
{
    private MeshRenderer _mesh;
    private bool _melt; 

    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;

        gameObject.layer = 4; 
    }

    public override void Activate()
    {
        _mesh.enabled = true;
        _melt = true; 
        gameObject.layer = LayerMask.NameToLayer("Ice");
        //Debug.Log("frozen");
    }

    void Update()
    {
    }

}
