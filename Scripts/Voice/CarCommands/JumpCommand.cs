using UnityEngine;

namespace Whisper.Samples {
  [CreateAssetMenu(fileName = "JumpCommand", menuName = "Voice Commands/Jump")]
  public class JumpCommand : VoiceCommand {
    public override void Execute(GameObject target) {
      if (target == null) {
        return;
      }


      if (!target.TryGetComponent(out CarController car)) {
        car = target.GetComponentInParent<CarController>();
      }

      if (car != null) {
        car.Jump();
      } else {
        Debug.LogError($"[JumpCommand] El objeto {target.name} no tiene CarController.");
      }
    }
  }
}