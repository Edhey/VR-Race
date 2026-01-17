using UnityEngine;
using UnityEngine.UI;

public class DashboardCompass : MonoBehaviour {
  [Header("Referencias UI")]
  [SerializeField] private RectTransform _needleRect;

  [Header("Configuración")]
  [SerializeField] private bool _useTrueNorth = true;
  [SerializeField] private float _smoothing = 5f;
  private CarLoader _carLoader;

  private void Start() {
    Input.compass.enabled = true;
    _carLoader = FindObjectsByType<CarLoader>(FindObjectsSortMode.None)[0];
    if (_useTrueNorth) {
      Input.location.Start();
    }
  }

  private void Update() {
    float northDirection = _useTrueNorth ? Input.compass.trueHeading : Input.compass.magneticHeading;
    float carY = _carLoader.CurrentCar.transform.eulerAngles.y;
    float targetAngle = northDirection - carY;
    Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
    _needleRect.localRotation = Quaternion.Slerp(_needleRect.localRotation, targetRotation, Time.deltaTime * _smoothing);
  }
}