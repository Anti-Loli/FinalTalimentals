using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    Scene currentScene;
    bool inCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        inCollider = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentScene.name == "OutsideSchool")
        {
            SceneManager.LoadScene("Credits");
        }
    }

    
}
