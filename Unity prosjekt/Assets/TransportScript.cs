using UnityEngine;
using System.Collections;

public class TransportScript : MonoBehaviour
{

    public Transform StartPos;
    public Transform EndPos;

    public float Speed;

    public GameObject HookPrefab;
    public GameObject[] IceCubePrefabs;

    private void Spawn()
    {
        var t = (GameObject)Instantiate(HookPrefab, StartPos.position, Quaternion.identity);
        var c = (GameObject)Instantiate(IceCubePrefabs[Random.Range(0, IceCubePrefabs.Length - 1)], 
            StartPos.position - new Vector3(0, 2.5f, 0), Quaternion.identity);
        c.transform.parent = t.GetComponent<HookScript>().transform;
        c.transform.localPosition = Vector3.zero;
    }
}
