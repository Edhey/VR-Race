using UnityEngine;

public class SceneNavigation : MonoBehaviour {
  public void RestartLevel() {
    UnityEngine.SceneManagement.SceneManager.LoadScene(
      UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
    );
  }

  public void LoadMainMenu() {
    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
  }

}
