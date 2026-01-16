using UnityEngine;

namespace Whisper.Samples {
  [CreateAssetMenu(fileName = "JumpCommand", menuName = "Voice Commands/Jump")]
  public class JumpCommand : VoiceCommand {
    public override void Execute(GameObject target) {
      if (target == null)
        return;

      var car = target.GetComponent<CarController>();

      if (car == null)
        car = target.GetComponentInParent<CarController>();

      if (car != null) {
        car.Jump();
      }
      else {
        Debug.LogError($"[JumpCommand] El objeto {target.name} no tiene CarController.");
      }
    }
  }
}