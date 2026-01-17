using UnityEngine;
using UnityEngine.Serialization;

namespace Whisper.Samples {
  public abstract class VoiceCommand : ScriptableObject {
    [field: SerializeField]
    [field: FormerlySerializedAs("keywords")]
    public string[] Keywords { get; private set; }

    public bool Matches(string text) {
      if (string.IsNullOrEmpty(text)) {
        return false;
      }

      var lowerText = text.ToLowerInvariant();

      foreach (var keyword in Keywords) {
        if (lowerText.Contains(keyword.ToLowerInvariant())) {
          return true;
        }
      }
      return false;
    }

    public abstract void Execute(GameObject target);
  }
}