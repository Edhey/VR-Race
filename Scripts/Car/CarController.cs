using System;
using Assets.Scripts.Voice;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VRInputHandler))]
public class CarController : MonoBehaviour {
  public static event Action<float, float, int> OnCarTelemetryUpdated;

  [Header("Velocidad máxima")]
  [FormerlySerializedAs("maxSpeed")]
  [SerializeField] private float _maxSpeed = 200f;

  [Header("Stability Settings")]
  [Tooltip("Lower this value (e.g., -0.5 or -1.0) to keep the car from flipping.")]
  [SerializeField] private Vector3 _centerOfMassOffset = new(0, -0.9f, 0);

  [Header("Car Settings")]
  [SerializeField] private float _maxMotorTorque = 1000f;
  [SerializeField] private float _maxSteeringAngle = 30f;
  [SerializeField] private float _brakeTorque = 3000f;
  [SerializeField] private float _boostForce = 20000f; // Aumentado para que se note
  [SerializeField] private float _groundCheckDistance = 1.5f;
  [SerializeField] private float _jumpForce = 80000f; // Aumentado para levantar peso

  [Header("Dependencies")]
  [SerializeField] private VRInputHandler _inputHandler;
  [SerializeField] private WheelCollider _wheelFL, _wheelFR, _wheelRL, _wheelRR;

  private Rigidbody _rb;

  private void Start() {
    _rb = GetComponent<Rigidbody>();

    // Estabilidad
    _rb.centerOfMass = _centerOfMassOffset;

    if (_inputHandler == null) {
      _inputHandler = GetComponent<VRInputHandler>();
    }

    VoiceManager voiceMgr = FindFirstObjectByType<VoiceManager>();
    if (voiceMgr != null) {
      voiceMgr.RegisterReceiver(gameObject);
      Debug.Log("[CarController] Registrado correctamente en VoiceManager.");
    } else {
      Debug.LogWarning("[CarController] No se encontró VoiceManager en la escena.");
    }
  }

  private void FixedUpdate() {
    HandleMotor();
    HandleSteering();
    EmitTelemetry();
  }

  private void HandleMotor() {
    float currentTorque = _inputHandler.ThrottleInput * _maxMotorTorque;

    if (_inputHandler.IsBraking) {
      ApplyBrakes(_brakeTorque);
    } else {
      ApplyBrakes(0);
      // Tracción 4x4 (AWD) para estabilidad en VR
      _wheelFL.motorTorque = currentTorque;
      _wheelFR.motorTorque = currentTorque;
      _wheelRL.motorTorque = currentTorque;
      _wheelRR.motorTorque = currentTorque;
    }
  }

  private void HandleSteering() {
    float steer = _inputHandler.SteerInput * _maxSteeringAngle;
    _wheelFL.steerAngle = steer;
    _wheelFR.steerAngle = steer;
  }

  private void ApplyBrakes(float force) {
    _wheelFL.brakeTorque = force;
    _wheelFR.brakeTorque = force;
    _wheelRL.brakeTorque = force;
    _wheelRR.brakeTorque = force;

    if (force > 0) {
      _wheelFL.motorTorque = 0;
      _wheelFR.motorTorque = 0;
      _wheelRL.motorTorque = 0;
      _wheelRR.motorTorque = 0;
    }
  }

  private void EmitTelemetry() {
    float speedKmh = _rb.linearVelocity.magnitude * 3.6f;

    float fakeRPM = Mathf.Lerp(1000, 8000, speedKmh / _maxSpeed);
    int gear = (Vector3.Dot(transform.forward, _rb.linearVelocity) > 0) ? 1 : -1;

    // Disparamos el evento estático
    OnCarTelemetryUpdated?.Invoke(speedKmh, fakeRPM, gear);
  }

  private void ResetDrag() => _rb.linearDamping = 0.05f;

  // --- ACCIONES DE VOZ ---
  public void Boost() {
    ResetDrag();
    Debug.Log("[CarController] ¡NITRO ACTIVADO!");
    // Usamos transform.forward para impulsar hacia donde mira el morro
    _rb.AddForce(transform.forward * _boostForce, ForceMode.Impulse);
  }

  public void Jump() {
    Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
    Debug.DrawRay(rayOrigin, Vector3.down * _groundCheckDistance, Color.red, 2f);

    if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _groundCheckDistance)) {
      if (hit.collider.transform.root != transform.root) {
        Debug.Log($"[CarController] Saltando sobre: {hit.collider.name}");
        ResetDrag();
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
      }
    } else {
      Debug.LogWarning("[CarController] Intento de salto fallido: No toco suelo.");
    }
  }

  public bool IsEmittingParticles() {
    if (_inputHandler == null) {
      return false;
    }
    return _inputHandler.IsBraking || Mathf.Abs(_inputHandler.SteerInput) > 0.8f;
  }
}