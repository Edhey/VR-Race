using UnityEngine;
using UnityEngine.InputSystem;

public class LightSensorPause : MonoBehaviour {
  private LightSensor _lightSensor;

  private void Start() {
    if (LightSensor.current != null) {
      _lightSensor = LightSensor.current;
      InputSystem.EnableDevice(_lightSensor);
    }
  }

  private void Update() {
    if (_lightSensor == null) {
      return;
    }

    float lightLevel = _lightSensor.lightLevel.ReadValue();

    // Si hay mucha luz, es que se quitó las gafas -> PAUSA
    if (lightLevel > 100) {
      if (Time.timeScale == 1) {
        Time.timeScale = 0; // Pausa
        Debug.Log("Gafas quitadas: Juego Pausado");
      }
    }
    // Si está oscuro, es que las tiene puestas -> JUEGO
    else if (lightLevel < 20) {
      if (Time.timeScale == 0) {
        Time.timeScale = 1;
        Debug.Log("Gafas puestas: Juego Reanudado");
      }
    }
  }

  private void OnDisable() {
    if (_lightSensor != null) {
      InputSystem.DisableDevice(_lightSensor);
    }
  }
}