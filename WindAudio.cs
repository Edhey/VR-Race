using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindAudio : MonoBehaviour {

  [Header("Configuración")]
  [SerializeField] private float maxSpeed = 200f;
  [SerializeField] private float maxVolume = 1.0f;

  [Header("Pitch")]
  [SerializeField] private float basePitch = 1.0f;
  [SerializeField] private float maxPitchMultiplier = 1.5f;

  private AudioSource _audioSource;
  private Rigidbody _carRigidbody;

  void Start() {
    _audioSource = GetComponent<AudioSource>();
    _audioSource.loop = true;
    _audioSource.spatialBlend = 0f; // Sonido 2D
    _carRigidbody = GetComponentInParent<Rigidbody>();
  }

  void Update() {
    if (_carRigidbody == null)
      return;

    float speed = _carRigidbody.linearVelocity.magnitude * 3.6f;
    float speedFactor = Mathf.Clamp01(speed / maxSpeed); // factor de intensidad (de 0 a 1)
    _audioSource.volume = (speedFactor * speedFactor) * maxVolume;
    _audioSource.pitch = Mathf.Lerp(basePitch, basePitch * maxPitchMultiplier, speedFactor);
  }
}