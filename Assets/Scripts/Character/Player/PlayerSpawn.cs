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

        if (!string.IsNullOrEmpty(PlayerInfo.spawnPoint))
        {
            GameObject spawnPointObject = GameObject.Find(PlayerInfo.spawnPoint);
            if (spawnPointObject != null)
            {
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

    // Blackout the screen and fade out gradually to hide the assets loading, character spawn, etc.
    IEnumerator HandleBlackout()
    {
        if (blackout != null)
        {
            blackoutCanvasGroup = blackout.GetComponent<CanvasGroup>();
            if (blackoutCanvasGroup == null)
            {
                blackoutCanvasGroup = blackout.AddComponent<CanvasGroup>();
            }

            blackout.SetActive(true);
            blackoutCanvasGroup.alpha = 1f;
    
            yield return new WaitForSeconds(0.5f);

            // Smoothly fade out
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                blackoutCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                yield return null;
            }

            blackoutCanvasGroup.alpha = 0f;
            blackout.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Blackout GameObject is not assigned.");
        }
    }
}