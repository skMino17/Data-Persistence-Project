using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager SM { get; private set; }

    private void Awake()
    {
        // start of new code
        if (SM != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        SM = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneId) => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
