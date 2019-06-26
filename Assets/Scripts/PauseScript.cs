using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public GameObject pauseScreen, gameOver, continuar;
    public FadeScript fadeScript;

    private bool isPaused, isGameOver;

    // Start is called before the first frame update
    void Start() {
        gameOver.SetActive(false);
        pauseScreen.SetActive(false);
        isPaused = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Cancel") && !isGameOver) {
            pauseController();
        }
    }

    private void pauseController() {
        if (!isPaused) {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        } else if (isPaused) {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void callGameOver(){
        isGameOver = true;
        pauseScreen.SetActive(true);
        gameOver.SetActive(true);
        continuar.SetActive(false);
        Time.timeScale = 0;
    }

    public void continues(){
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void restart(){
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 0;
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    public void menu(){
        SceneManager.LoadScene("Menu");
    }

    public void quit(){
        Application.Quit();
    }    
}
