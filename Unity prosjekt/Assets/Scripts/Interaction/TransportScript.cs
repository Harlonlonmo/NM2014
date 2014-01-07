using UnityEngine;
using System.Collections;

public class TransportScript : HitEfect
{

    public Transform StartPos;
    public Transform EndPos;

    public WaterTank WatetrDestination;

    public float Speed;

    public float TimeInterval;

    public GameObject HookPrefab;
    public GameObject[] IceCubePrefabs;

    public bool Active;


    private float timer = 0;

    private void Update()
    {
        if (Active)
        {
            if (timer >= TimeInterval)
            {
                timer -= TimeInterval;
                Spawn();
            }
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }

    private void Spawn()
    {
        var t = (GameObject)Instantiate(HookPrefab, StartPos.position, Quaternion.identity);
        t.transform.parent = transform;
        var c = (GameObject)Instantiate(IceCubePrefabs[Random.Range(0, IceCubePrefabs.Length - 1)],
            StartPos.position - new Vector3(0, 2.5f, 0), Quaternion.identity);
        HookScript hs = t.GetComponent<HookScript>();
        c.transform.parent = hs.IceCubePosition;
        c.transform.localPosition = Vector3.zero;

        hs.IceCubePosition.GetComponent<IcicleMelter>().waterDestination = WatetrDestination;

        hs.Target = EndPos;
        hs.speed = Speed;
    }

    public override void Activate()
    {
        Active = true;
    }
}
