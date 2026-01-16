using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputHandler : MonoBehaviour {

  public float SteerInput { get; private set; }
  public float ThrottleInput { get; private set; }
  public bool IsBraking { get; private set; }

  [Header("Settings")]
  [Tooltip("How far down you have to look to reach 100% throttle (Degrees)")]
  [SerializeField] private float _maxLookDownAngle = 20f;
  [Tooltip("How far up you have to look to reach 100% reverse (Degrees)")]
  [SerializeField] private float _maxLookUpAngle = 20f;

  [Tooltip("How far left/right you have to look to turn fully (Degrees)")]
  [SerializeField] private float _maxSteerAngle = 45f;

  [Tooltip("Deadzone to prevent drifting when looking straight")]
  [SerializeField] private float _deadzone = 5f;

  private Transform _camTransform;
  private Keyboard _keyboard;

  private void Start() {
    if (Camera.main != null) {
      _camTransform = Camera.main.transform;
    } else {
      Debug.LogError("VRInputHandler: No Main Camera found! Tag your XR camera as 'MainCamera'.");
    }
    _keyboard = Keyboard.current;
  }

  private void Update() {
    if (_camTransform != null) {
      HandleHeadInput();
    }

#if UNITY_EDITOR
    HandleEditorInput();
#endif
  }

  private void HandleHeadInput() {
    Vector3 headRotation = _camTransform.localEulerAngles;
    float yaw = NormalizeAngle(headRotation.y);
    if (Mathf.Abs(yaw) < _deadzone) {
      SteerInput = 0f;
    } else {
      SteerInput = Mathf.Clamp(yaw / _maxSteerAngle, -1f, 1f);
    }
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
    if (angle > 180f) {
      angle -= 360f;
    }
    return angle;
  }

  private void HandleEditorInput() {
    if (_keyboard == null) {
      return;
    }
    if (_keyboard.wKey.isPressed || _keyboard.sKey.isPressed ||
        _keyboard.aKey.isPressed || _keyboard.dKey.isPressed) {
      ThrottleInput = 0;
      if (_keyboard.wKey.isPressed) {
        ThrottleInput += 1;
      }
      if (_keyboard.sKey.isPressed) {
        ThrottleInput += -1;
      }
      SteerInput = 0;
      if (_keyboard.aKey.isPressed) {
        SteerInput += -1;
      }
      if (_keyboard.dKey.isPressed) {
        SteerInput += 1;
      }
      IsBraking = _keyboard.spaceKey.isPressed;
    }
  }
}