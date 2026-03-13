using UnityEditor;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager SM { get; private set; }

    private void Awake()
    {
        if (SM != null)
        {
            Destroy(gameObject);
            return;
        }

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
