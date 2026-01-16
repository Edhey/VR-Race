using UnityEngine;

public class Boost : PowerUp {
  public override void ApplyPowerUp(CarController carController) {
    carController.GetComponent<Rigidbody>()
      .AddForce(
        carController.transform.forward * _boostStrength,
        ForceMode.VelocityChange
      );
  }
  private readonly float _boostStrength = 1.5f;
}