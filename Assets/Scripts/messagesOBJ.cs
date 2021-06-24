using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/messagesOBJ", order = 1)]
public class messagesOBJ : ScriptableObject
{
    //the messages to display
    public List<string> messages;

    //if clikcing through all messages should go to next scene
    public bool nextSceneOnClear;
}