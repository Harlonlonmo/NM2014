using UnityEngine;
using System.Collections;

public abstract class Beam : MonoBehaviour
{

    public abstract void Render(Vector3 start, Vector3 end);

    public abstract void SetEnabled(bool enabled);
}
