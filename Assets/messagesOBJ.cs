using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/messagesOBJ", order = 1)]
public class messagesOBJ : ScriptableObject
{
    public List<string> messages;

    public bool nextSceneOnClear;
}