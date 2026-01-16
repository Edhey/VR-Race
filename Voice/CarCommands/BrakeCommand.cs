using UnityEngine;

namespace Whisper.Samples {
  [CreateAssetMenu(fileName = "BrakeCommand", menuName = "Voice Commands/Brake")]
  public class BrakeCommand : VoiceCommand {
    public float brakeForce = 5f;

    public override void Execute(GameObject target) {
      // ApplyBrakes(brakeTorque * 5);
      // _rb.linearDamping = 2f;
      // Invoke(nameof(ResetDrag), 2f);
    }
  }
}