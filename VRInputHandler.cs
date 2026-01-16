using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputHandler : MonoBehaviour {

  public float SteerInput { get; private set; }
  public float ThrottleInput { get; private set; }
  public bool IsBraking { get; private set; }

  [Header("Ajustes de Cabeza")]
  [SerializeField] private float _maxLookDownAngle = 20f;
  [SerializeField] private float _maxLookUpAngle = 20f;
  [SerializeField] private float _maxSteerAngle = 45f;
  [SerializeField] private float _deadzone = 3f;

  [Header("Mejoras de Sensibilidad")]
  [Tooltip("Hace que el centro sea menos sensible. 1 = Lineal, 2 = Exponencial")]
  [SerializeField] private float _steeringCurve = 1.5f;
  [Tooltip("Suavizado para evitar temblores del cuello")]
  [SerializeField] private float _smoothTime = 0.15f;

  private Transform _camTransform;
  private Keyboard _keyboard;

  // Variables para el suavizado
  private float _currentSteerVelocity;
  private float _targetSteer;

  private void Start() {
    if (Camera.main != null)
      _camTransform = Camera.main.transform;
    _keyboard = Keyboard.current;
  }

  private void Update() {
    if (_camTransform != null)
      HandleHeadInput();

#if UNITY_EDITOR
    HandleEditorInput();
#endif
  }

  private void HandleHeadInput() {
    Vector3 headRotation = _camTransform.localEulerAngles;

    // --- 1. DIRECCIÓN (MEJORADA) ---
    float yaw = NormalizeAngle(headRotation.y);
    float rawSteer = 0f;

    if (Mathf.Abs(yaw) > _deadzone) {
      // Calculamos el porcentaje de giro (0 a 1)
      float t = (Mathf.Abs(yaw) - _deadzone) / (_maxSteerAngle - _deadzone);
      t = Mathf.Clamp01(t);

      // Aplicamos Curva Exponencial (Sensibilidad no lineal)
      // Esto hace que mirar un poco gire MUY poco, y mirar mucho gire rápido
      float curvedT = Mathf.Pow(t, _steeringCurve);

      rawSteer = curvedT * Mathf.Sign(yaw); // Restauramos el signo (izq/der)
    }

    // Aplicamos Suavizado (SmoothDamp) para quitar el "jitter"
    SteerInput = Mathf.SmoothDamp(SteerInput, rawSteer, ref _currentSteerVelocity, _smoothTime);


    // --- 2. ACELERACIÓN (Igual que antes) ---
    float pitch = NormalizeAngle(headRotation.x);
    if (Mathf.Abs(pitch) < _deadzone) {
      ThrottleInput = 0f;
    } else if (pitch > 0) {
      float val = (pitch - _deadzone) / (_maxLookDownAngle - _deadzone);
      ThrottleInput = Mathf.Clamp01(val);
    } else {
      float val = (Mathf.Abs(pitch) - _deadzone) / (_maxLookUpAngle - _deadzone);
      ThrottleInput = -Mathf.Clamp01(val);
    }
  }

  private float NormalizeAngle(float angle) {
    if (angle > 180f)
      angle -= 360f;
    return angle;
  }

  private void HandleEditorInput() {
    if (_keyboard == null)
      return;
    // ... (Tu código de editor igual) ...
    if (_keyboard.aKey.isPressed)
      _targetSteer = -1;
    else if (_keyboard.dKey.isPressed)
      _targetSteer = 1;
    else
      _targetSteer = 0;

    // Sobrescribimos para probar en editor
    if (_keyboard.aKey.isPressed || _keyboard.dKey.isPressed)
      SteerInput = _targetSteer;
  }
}