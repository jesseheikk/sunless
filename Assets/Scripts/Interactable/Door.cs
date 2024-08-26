using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string spawnPoint;
    [SerializeField] bool isLocked = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Interact"))
        {
            if (isLocked)
            {
                StartDialog();
            }
            else
            {
                if (IsSceneInBuildSettings(sceneToLoad))
                {
                    EndDialog();
                    PlayerInfo.spawnPoint = spawnPoint;
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    Debug.LogWarning("Scene " + sceneToLoad + " not found in build settings.");
                }
            }
        }
    }

    bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneNameFromPath == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
