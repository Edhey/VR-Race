using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class WindAudio : MonoBehaviour {

  [Header("Configuración")]
  [FormerlySerializedAs("maxSpeed")]
  [SerializeField] private float _maxSpeed = 200f;
  [FormerlySerializedAs("maxVolume")]
  [SerializeField] private float _maxVolume = 1.0f;

  [Header("Pitch")]
  [FormerlySerializedAs("basePitch")]
  [SerializeField] private float _basePitch = 1.0f;
  [FormerlySerializedAs("maxPitchMultiplier")]
  [SerializeField] private float _maxPitchMultiplier = 1.5f;

  private AudioSource _audioSource;
  private Rigidbody _carRigidbody;

  private void Start() {
    _audioSource = GetComponent<AudioSource>();
    _audioSource.loop = true;
    _audioSource.spatialBlend = 0f; // Sonido 2D
    _carRigidbody = GetComponentInParent<Rigidbody>();
  }

  private void Update() {
    if (_carRigidbody == null) {
      return;
    }

    float speed = _carRigidbody.linearVelocity.magnitude * 3.6f;
    float speedFactor = Mathf.Clamp01(speed / _maxSpeed); // factor de intensidad (de 0 a 1)
    _audioSource.volume = (speedFactor * speedFactor) * _maxVolume;
    _audioSource.pitch = Mathf.Lerp(_basePitch, _basePitch * _maxPitchMultiplier, speedFactor);
  }
}