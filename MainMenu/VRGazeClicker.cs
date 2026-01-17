using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VRGazeClicker : MonoBehaviour {
  [Header("Configuración")]
  [FormerlySerializedAs("reticleImage")]
  [SerializeField] private Image _reticleImage;
  [FormerlySerializedAs("clickDuration")]
  [SerializeField] private float _clickDuration = 2.0f;
  [FormerlySerializedAs("maxDistance")]
  [SerializeField] private float _maxDistance = 50f;

  private float _timer;
  private Button _currentButton;

  private void Update() {
    // Lanzar un rayo desde el centro de la cámara
    Ray ray = new(transform.position, transform.forward);

    bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, _maxDistance);
    Button hitButton = hitSomething ? hit.collider.GetComponent<Button>() : null;

    if (hitButton != null) {
      if (_currentButton != hitButton) {
        _currentButton = hitButton;
        _timer = 0f;
      }

      _timer += Time.deltaTime;

      if (_reticleImage) {
        _reticleImage.fillAmount = _timer / _clickDuration;
      }

      if (_timer >= _clickDuration) {
        hitButton.onClick.Invoke();
        _timer = 0f;
        _currentButton = null; // Forzar volver a mirar para otro clic
      }
    } else {
      _currentButton = null;
      _timer = 0f;
      if (_reticleImage) {
        _reticleImage.fillAmount = 0f;
      }
    }
  }
}