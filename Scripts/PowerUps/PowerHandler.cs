using UnityEngine;

[RequireComponent(typeof(CarController))]
public class PowerHandler : MonoBehaviour {

  private CarController _carController;
  private void Awake() {
    _carController = GetComponent<CarController>();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out PowerUp powerUp)) {
      powerUp.ApplyPowerUp(_carController);
      Destroy(powerUp.gameObject);
    }
  }
}