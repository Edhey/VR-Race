using UnityEngine;

public class HUDFollower : MonoBehaviour {
  [Header("Objetivo")]
  [SerializeField] private Transform _headTarget;

  [Header("Configuración Iron Man")]
  [SerializeField] private float _distance = 1.5f;
  [SerializeField] private float _smoothSpeed = 10f;
  [SerializeField] private Vector3 _offset = new Vector3(0, -0.2f, 0);

  /// <summary>
  /// Actualiza la posición y rotación del HUD para seguir la cabeza del 
  /// usuario con un efecto suave.
  /// </summary>
  void LateUpdate() {
    if (_headTarget == null) {
      return;
    }

    Vector3 targetPosition = _headTarget.position + (_headTarget.forward * _distance) + _offset;
    Quaternion targetRotation = _headTarget.rotation;
    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _smoothSpeed);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _smoothSpeed);
  }
}