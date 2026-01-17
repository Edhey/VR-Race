using UnityEngine;

public class VRInputHandler : MonoBehaviour {
  [Header("Calibration")]
  [SerializeField] private float _maxPhoneTilt = 25f;
  [SerializeField] private float _accelerationTiltOffset = 30f; // Los 30 grados de inclinación base

  public float SteerInput { get; private set; } // Rango -1 a 1
  public float ThrottleInput { get; private set; } // Rango -1 a 1
  public bool IsBraking { get; private set; }

  private Gyroscope _gyro;

  private void Start() {
    _gyro = Input.gyro;
    _gyro.enabled = true;
  }

  private void Update() {
    Quaternion quaternion = _gyro.attitude;
    // Conversión de coordenadas de Android/iOS a Unity
    quaternion = Quaternion.Euler(90, 0, -90) * new Quaternion(-quaternion.x, -quaternion.y, quaternion.z, quaternion.w);

    // 2. Calcular Giro (Steering)
    float rawTiltZ = quaternion.eulerAngles.z > 180 ? quaternion.eulerAngles.z - 360 : quaternion.eulerAngles.z;
    SteerInput = Mathf.Clamp(rawTiltZ / _maxPhoneTilt, -1f, 1f) * -1f; // Invertido según tu código

    // 3. Calcular Aceleración (Throttle)
    float rawTiltX = quaternion.eulerAngles.x - _accelerationTiltOffset;
    float adjustedX = rawTiltX > 180 ? rawTiltX - 360 : rawTiltX;
    ThrottleInput = Mathf.Clamp(adjustedX / _maxPhoneTilt, -1f, 1f);

#if UNITY_EDITOR  // Debug con teclado
    ThrottleInput = 0;
    if (Input.GetKey(KeyCode.W)) {
      ThrottleInput = 1;
    }

    if (Input.GetKey(KeyCode.S)) {
      ThrottleInput = -1;
    }
    SteerInput = 0;
    if (Input.GetKey(KeyCode.A)) {
      SteerInput = -1;
    }

    if (Input.GetKey(KeyCode.D)) {
      SteerInput = 1;
    }

    IsBraking = Input.GetKey(KeyCode.Space);
#endif
  }
}