using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] GameObject blackout;

    CanvasGroup blackoutCanvasGroup;

    void Awake()
    {
        StartCoroutine(HandleBlackout());

        // Check if PlayerInfo.spawnPoint is set and not empty
        if (!string.IsNullOrEmpty(PlayerInfo.spawnPoint))
        {
            // Try to find the spawn point GameObject
            GameObject spawnPointObject = GameObject.Find(PlayerInfo.spawnPoint);

            // Check if the GameObject was found
            if (spawnPointObject != null)
            {
                // Set the position of the current GameObject to the position of the spawn point
                transform.position = spawnPointObject.transform.position;
            }
            else
            {
                Debug.LogWarning("Spawn point GameObject not found: " + PlayerInfo.spawnPoint);
            }
        }
        else
        {
            Debug.LogWarning("PlayerInfo.spawnPoint is not set or is empty.");
        }
    }

IEnumerator HandleBlackout()
    {
        if (blackout != null)
        {
            // Ensure the blackout GameObject has a CanvasGroup component
            blackoutCanvasGroup = blackout.GetComponent<CanvasGroup>();
            if (blackoutCanvasGroup == null)
            {
                blackoutCanvasGroup = blackout.AddComponent<CanvasGroup>();
            }

            // Enable the blackout GameObject
            blackout.SetActive(true);

            // Set the initial alpha to 1 (fully visible)
            blackoutCanvasGroup.alpha = 1f;

            // Wait for 2 seconds before starting the fade out
            yield return new WaitForSeconds(0.5f);

            // Smoothly fade out
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                blackoutCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                yield return null;
            }

            // Ensure the alpha is set to 0 (fully transparent)
            blackoutCanvasGroup.alpha = 0f;

            // Disable the blackout GameObject after fading out
            blackout.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Blackout GameObject is not assigned.");
        }
    }
}