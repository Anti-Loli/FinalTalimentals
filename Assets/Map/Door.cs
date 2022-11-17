using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Scene currentScene;
    bool inCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        inCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentScene.name == "InsideSchool")
                {
                    SceneManager.LoadScene("Cave");
                }
                else if (currentScene.name == "OutsideSchool")
                {
                    SceneManager.LoadScene("Credits");
                }
                else if (currentScene.name == "Cave")
                {
                    SceneManager.LoadScene("OutsideSchool");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inCollider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollider = false;
    }
}
