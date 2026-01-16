using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class CarAudio : MonoBehaviour {

    [Header("Configuración")]
    [Tooltip("El tono del motor cuando el coche está quieto")]
    [SerializeField] private float minPitch = 0.8f;

    [Tooltip("El tono máximo cuando va a tope de velocidad")]
    [SerializeField] private float maxPitch = 2.5f;

    [Tooltip("A qué velocidad (km/h) el motor suena al máximo")]
    [SerializeField] private float maxSpeedForAudio = 180f;

    private AudioSource _audioSource;
    private Rigidbody _rb;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }

    void Update() {
        // Calculamos velocidad actual en km/h (Unity usa m/s, 
        // multiplicamos por 3.6 para km/h)
        float currentSpeed = _rb.linearVelocity.magnitude * 3.6f;

        // Calculamos el Pitch
        // Usamos Lerp para interpolar entre el tono mínimo y el máximo según la velocidad
        float pitch = Mathf.Lerp(minPitch, maxPitch, currentSpeed / maxSpeedForAudio);

        _audioSource.pitch = pitch;
        _audioSource.volume = currentSpeed / maxSpeedForAudio; // ajustar volumen según velocidad
    }
}