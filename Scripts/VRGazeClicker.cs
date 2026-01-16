using UnityEngine;
using UnityEngine.UI;

public class VRGazeClicker : MonoBehaviour {
  [Header("Configuración")]
  public Image reticleImage;
  public float clickDuration = 2.0f;
  public float maxDistance = 50f;

  private float _timer;
  private Button _currentButton;
  private bool _isLookingAtButton;

  void Update() {
    // Lanzar un rayo desde el centro de la cámara
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    bool hitSomething = Physics.Raycast(ray, out hit, maxDistance);
    Button hitButton = hitSomething ? hit.collider.GetComponent<Button>() : null;

    if (hitButton != null) {
      if (_currentButton != hitButton) {
        _currentButton = hitButton;
        _timer = 0f;
      }

      _timer += Time.deltaTime;

      if (reticleImage) {
        reticleImage.fillAmount = _timer / clickDuration;
      }

      if (_timer >= clickDuration) {
        hitButton.onClick.Invoke();
        _timer = 0f;
        _currentButton = null; // Forzar volver a mirar para otro clic
      }
    } else {
      _currentButton = null;
      _timer = 0f;
      if (reticleImage) {
        reticleImage.fillAmount = 0f;
      }
    }
  }
}