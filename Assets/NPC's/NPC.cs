using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC files archive")]
public class NPC : ScriptableObject 
{
    public string name;//name of the NPC
    [TextArea(3, 15)]
    public string[] dialogue;//array that stores dialogue of NPC
    [TextArea(3, 15)]
    public string[] playerDialogue;//array that stores dialogue options for player
}
