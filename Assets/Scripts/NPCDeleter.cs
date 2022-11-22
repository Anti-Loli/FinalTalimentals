using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDeleter : MonoBehaviour
{
    public GameObject[] despawn;
    public GameObject[] spawn;
    public Image spawnCover;
    public Image despawnCover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < despawn.Length; i++)
        {
            despawn[i].SetActive(false);
        }

        for (int i = 0; i < spawn.Length; i++)
        {
            spawn[i].SetActive(true);
        }
    }
}
