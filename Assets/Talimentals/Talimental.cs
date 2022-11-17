using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talimental file", menuName = "Talimental files archive")]
public class Talimental : ScriptableObject
{
    public string name;//name of the talimental
    public int level;
    public int health;
    public int attack;
    public int defence;
    public int speed;
    public string element;
    [TextArea(3, 15)]
    public string[] moves;
    [TextArea(3, 15)]
    public string[] moveElements;

}
