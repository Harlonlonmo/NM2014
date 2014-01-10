﻿using UnityEngine;
using System.Collections;

public static class TransportInfo
{
    public static int spawnPoint = 0;

    public static void Reset()
    {
        spawnPoint = 0;
    }
}

public class LevelSpawnHandler : MonoBehaviour
{

    public Transform Player;

    public Transform[] SpawnPoints;

    void Awake()
    {
        Player.position = SpawnPoints[TransportInfo.spawnPoint].position;
        Player.rotation = SpawnPoints[TransportInfo.spawnPoint].rotation;
    }
}
