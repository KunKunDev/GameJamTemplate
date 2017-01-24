using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Transition Stage;
    CloseCurtainOverride Override;

    bool BeganLoad = false;

    // Use this for initialization
    void Start()
    {
        Stage = Transition.GetOrCreateInstance();
        Override = gameObject.AddComponent<CloseCurtainOverride>();
        Override.RateMultiplier = 0.5f;
        Override.enabled = false;
    }

    public void NextScene(string sceneName)
    {
        if (!BeganLoad)
        {
            BeganLoad = true;
            StartCoroutine(LoadNextScene(sceneName));
        }
    }

    IEnumerator LoadNextScene(string sceneName)
    {
        Override.enabled = true;

        while (!Stage.CurtainIsClosed)
        {
            yield return null;
        }
        
        SceneManager.LoadSceneAsync(sceneName);
    }
}
