// using UnityEngine;
// using UnityEngine.SceneManagement; // Include this namespace to work with scenes

// public class LoadSceneOnCollision : MonoBehaviour
// {
//     public string sceneToLoad = "NameOfTheSceneToLoad"; // Set this in the Inspector or through code

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         // If collided with player
//         PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
//         if (playerHealth)
//         {         
//         // Loads the specified scene when this object collides with another collider
//         SceneManager.LoadScene(sceneToLoad);
//         }
//     }
// }
