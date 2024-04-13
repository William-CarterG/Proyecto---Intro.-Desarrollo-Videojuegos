using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMiniGame : MonoBehaviour
{
    public string sceneToLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
