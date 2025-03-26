using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string[] sceneNames; 
    private int currentSceneIndex = 0; 

    void Start()
    {
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeToNextScene();
        }
    }

    void ChangeToNextScene()
    {
       
        currentSceneIndex = (currentSceneIndex + 1) % sceneNames.Length;
        SceneManager.LoadScene(sceneNames[currentSceneIndex]);
    }
}
