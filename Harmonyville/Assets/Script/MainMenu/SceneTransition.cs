using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string destinationSceneName;

    private bool hasTouchedObject = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTouchedObject = true;
        }
    }

    private void Update()
    {
        if (hasTouchedObject && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(destinationSceneName);
        }
    }
}