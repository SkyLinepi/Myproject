using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public void LoadScenName(string scaneName)
    {
        SceneManager.LoadScene(scaneName);
    }
    public void LoadNextScena()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadScenePlay()
    {
        StartCoroutine(LoadSceneAfterDelay(1.5f));
        
    }

    IEnumerator LoadSceneAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(1);
    }
    

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ออกเกมเเล้ว");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
