using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_InputField InputName;
    public TMP_Text HighScoreText;

    public void SetCurPlayerName() => GameManager.GM.SetName(InputName.text);

    public void StartNewGame(int sceneID) => SceneManager.SM.LoadScene(sceneID);

    public void ExitGame() => SceneManager.SM.ExitGame();
    void Start()
    {
        HighScoreText.text = GameManager.GM.GetHighScoreString();
    }
}
