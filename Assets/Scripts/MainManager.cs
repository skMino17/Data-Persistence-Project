using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Brick BrickPrefab;
    public Rigidbody Ball;
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public int LineCount = 6;
    private int m_Points;
    private bool m_Started = false;
    private bool m_GameOver = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        ScoreText.text = $"{GameManager.GM.GetName()} Score : {m_Points}";

        HighScoreText.text = GameManager.GM.GetHighScoreString();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity, GameObject.Find("Bricks").transform);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.SM.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{GameManager.GM.GetName()} Score : {m_Points}";

        if (m_Points > GameManager.GM.GetHighScore())
        {
            GameManager.GM.SetHighScore(m_Points);
            HighScoreText.text = GameManager.GM.GetHighScoreString();
        }
    }

    public void GameOver()
    {
        GameManager.GM.SaveHighScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
