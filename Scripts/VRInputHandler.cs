using UnityEngine;

public class VRInputHandler : MonoBehaviour {

  [Header("Configuración de Inclinación")]
  [Tooltip("Cuánto hay que bajar la cabeza para ir a máxima velocidad (Grados)")]
  [SerializeField] private float _maxPhoneTilt = 20f;

  [Tooltip("El ángulo considerado 'Neutro'. Ponlo en 0 para que mirar al horizonte sea parar.")]
  [SerializeField] private float _neutralAngle = 0f;

  [Tooltip("Grados de margen para que el coche no se mueva si tiemblas un poco")]
  [SerializeField] private float _deadzone = 5f;

  public float SteerInput { get; private set; }
  public float ThrottleInput { get; private set; }
  public bool IsBraking { get; private set; }

  private Gyroscope _gyro;

  private void Start() {
    if (SystemInfo.supportsGyroscope) {
      _gyro = Input.gyro;
      _gyro.enabled = true;
    }
  }

  private void Update() {
    if (_gyro != null) {
      ProcesarGiroscopio();
    }

#if UNITY_EDITOR
    ProcesarTeclado();
#endif
  }

  private void ProcesarGiroscopio() {
    // 1. Obtener orientación corregida
    Quaternion q = _gyro.attitude;
    Quaternion rot = Quaternion.Euler(90, 0, -90) * new Quaternion(-q.x, -q.y, q.z, q.w);

    // 2. Calcular Pitch (Arriba/Abajo) para Acelerar
    float pitch = rot.eulerAngles.x;
    // Normalizar ángulo (-180 a 180) para entender si miras arriba o abajo
    if (pitch > 180) {
      pitch -= 360;
    }

    // Restar el ángulo neutro (si quieres calibrar)
    float anguloAceleracion = pitch - _neutralAngle;

    // APLICAR DEADZONE (Zona Muerta)
    // Si el ángulo está entre -5 y 5, nos quedamos quietos
    if (Mathf.Abs(anguloAceleracion) < _deadzone) {
      ThrottleInput = 0;
    } else {
      // Si pasamos la zona muerta, calculamos la potencia
      // Restamos la deadzone para que la aceleración empiece suave desde 0
      float input = (anguloAceleracion > 0)
          ? (anguloAceleracion - _deadzone)
          : (anguloAceleracion + _deadzone);

      ThrottleInput = Mathf.Clamp(input / _maxPhoneTilt, -1f, 1f);
    }

    // 3. Calcular Roll (Izquierda/Derecha) para Girar
    float roll = rot.eulerAngles.z;
    if (roll > 180) {
      roll -= 360;
    }

    // Zona muerta también para el giro
    if (Mathf.Abs(roll) < _deadzone) {
      SteerInput = 0;
    } else {
      float steerVal = (roll > 0) ? (roll - _deadzone) : (roll + _deadzone);
      // Invertimos (* -1) porque suele estar al revés en landscape
      SteerInput = Mathf.Clamp(steerVal / _maxPhoneTilt, -1f, 1f) * -1f;
    }

    // Freno simple: Si miramos muy arriba (reversa fuerte) o tocamos pantalla
    IsBraking = Input.touchCount > 0;
  }

  private void ProcesarTeclado() {
    if (Input.GetKey(KeyCode.W)) {
      ThrottleInput = 1;
    } else if (Input.GetKey(KeyCode.S)) {
      ThrottleInput = -1;
    }

    if (Input.GetKey(KeyCode.A)) {
      SteerInput = -1;
    } else if (Input.GetKey(KeyCode.D)) {
      SteerInput = 1;
    }

    if (Input.GetKey(KeyCode.Space)) {
      IsBraking = true;
    }
  }
}