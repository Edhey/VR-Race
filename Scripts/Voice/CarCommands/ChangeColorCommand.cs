using UnityEngine;
using UnityEngine.Serialization;

namespace Whisper.Samples {
  [CreateAssetMenu(fileName = "ChangeColorCommand", menuName = "Voice Commands/Change Color")]
  public class ChangeColorCommand : VoiceCommand {
    [FormerlySerializedAs("targetColor")]
    [SerializeField] private Color _targetColor = Color.red;

    public override void Execute(GameObject target) {
      if (target == null) {
        return;
      }

      if (!target.TryGetComponent(out Renderer rend)) {
        rend = target.GetComponentInChildren<Renderer>();
      }

      if (rend != null) {
        rend.material.color = _targetColor;
      }
    }
  }
}