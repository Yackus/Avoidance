using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/thoughtsOBJ", order = 1)]
public class thoughtsOBJ : ScriptableObject
{
    //the messages to display
    public List<string> thoughts;

    //if clearing all thoughts is required to progress
    public bool nextSceneOnClear;

    public enum SpawnModes
    {
        random,
        fromEdges,
        onRings
    }

    public SpawnModes spawnMode;

    public int toSpawn;

    public bool canMoveThoughts;

    public bool spawnWall;
    public float timeTillWall;
}