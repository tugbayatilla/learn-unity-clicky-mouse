using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private TargetStore targetStore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    private float spawnRate = 1f;
    private int score = 0;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameOver = true;
    }

    private IEnumerator SpawnTarget()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            var randomIndex = Random.Range(0, targets.Count);
            Instantiate(targets[randomIndex]);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        targetStore = GameObject.Find("TargetStore").GetComponent<TargetStore>();
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        scoreText.text = $"Score {score}";
        targetStore.TargetDestroyed += (t) =>
        {
            score += t.pointValue;
            scoreText.text = $"Score {score}";

            if (t.CompareTag("Bad"))
            {
                GameOver();
            }
        };

        titleScreen.gameObject.SetActive(false);
    }


}
