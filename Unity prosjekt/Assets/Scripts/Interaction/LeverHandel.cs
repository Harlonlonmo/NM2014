using UnityEngine;

public class LeverHandel : MonoBehaviour
{

    public float activatedRotation;
    public float deacttivatedRotation;

    public bool state;

    public float rotationSpeed;

    public void Update()
    {
        float goal = (state ? activatedRotation : deacttivatedRotation);
        if (transform.localRotation.eulerAngles.y != goal)
        {
            float rel = goal - transform.localRotation.eulerAngles.y;
            int direction = (int)Mathf.Sign(rel);
            transform.localRotation = Quaternion.Euler(
                transform.localRotation.eulerAngles +
                new Vector3(0, Mathf.Min(rotationSpeed*Time.deltaTime,Mathf.Abs(rel))*direction, 0));
        }
    }
}