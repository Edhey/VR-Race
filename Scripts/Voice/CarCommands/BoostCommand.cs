using UnityEngine;

namespace Whisper.Samples {
  [CreateAssetMenu(fileName = "BoostCommand", menuName = "Voice Commands/Boost")]
  public class BoostCommand : VoiceCommand {
    public override void Execute(GameObject target) {
      if (target == null)
        return;
      var car = target.GetComponent<CarController>();
      if (car == null)
        car = target.GetComponentInParent<CarController>();
      if (car != null) {
        car.Boost();
      }
      else {
        Debug.LogError($"[BoostCommand] El objeto {target.name} no tiene CarController.");
      }
    }
  }
}