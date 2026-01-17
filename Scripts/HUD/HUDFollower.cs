using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HUDFollower : MonoBehaviour {
  [Range(0.1f, 1.0f)]
  public float FillPercent = 0.8f;


  [Header("Configuración HUD")]
  [SerializeField] private float _distance = 1.5f;
  [SerializeField] private float _smoothSpeed = 10f;
  [SerializeField] private Vector3 _offset = new(0, -0.2f, 0);
  private CarLoader _carLoader;
  private Camera _headTarget;

  private void Start() {
    _carLoader = FindObjectsByType<CarLoader>(FindObjectsSortMode.None)[0];
    if (_carLoader == null || _carLoader.CurrentCar == null) {
      Debug.LogWarning("HUDFollower: CarLoader or CurrentCar reference is missing!");
      return;
    }
    _headTarget = _carLoader.CurrentCar.GetComponentInChildren<Camera>();
    if (_headTarget == null) {
      Debug.LogWarning("HUDFollower: No Camera found on CurrentCar!");
    }
  }

  /// <summary>
  /// Actualiza la posición y rotación del HUD para seguir la cabeza del 
  /// usuario con un efecto suave.
  /// </summary>
  private void LateUpdate() {
    if (_headTarget == null) {
      return;
    }

    Transform cam = _headTarget.transform;

    Vector3 targetPos = cam.TransformPoint(new Vector3(_offset.x, _offset.y, _distance));

    float rotationSmooth = _smoothSpeed * 1.5f;

    transform.SetPositionAndRotation(
      Vector3.Lerp(
        transform.position,
         targetPos,
          Time.deltaTime * _smoothSpeed
          ),
          Quaternion.Slerp(
            transform.rotation,
            cam.rotation,
            Time.deltaTime * rotationSmooth
            )
      );
  }

}