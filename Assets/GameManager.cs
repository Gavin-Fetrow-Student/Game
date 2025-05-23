using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private float restartDelay = 5f;

    private bool isGameOver = false;

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        Debug.Log("Game Over!");
        // StartCoroutine(RestartGame());

    }
}
