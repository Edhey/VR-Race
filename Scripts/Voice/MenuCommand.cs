using UnityEngine;
using UnityEngine.Serialization;
using Whisper.Samples;

namespace Assets.Scripts.Voice {
  // Definimos qué puede hacer el menú
  public enum MenuAction {
    NextCar,
    PrevCar,
    NextTrack,
    MoreLaps,
    LessLaps,
    StartGame
  }

  [CreateAssetMenu(fileName = "NewMenuCommand", menuName = "Voice Commands/Menu Action")]
  public class MenuCommand : VoiceCommand {

    [FormerlySerializedAs("actionType")]
    [SerializeField] private MenuAction _actionType;

    public MenuAction ActionType => _actionType;

    public override void Execute(GameObject target) {
      // Buscamos el gestor del menú en el objeto que recibe la orden
      if (!target.TryGetComponent(out MainMenuManager menu)) {
        return;
      }

      switch (_actionType) {
        case MenuAction.NextCar:
          menu.NextCar();
          break;
        case MenuAction.PrevCar:
          menu.PrevCar();
          break;
        case MenuAction.NextTrack:
          menu.NextTrack();
          break;
        case MenuAction.MoreLaps:
          menu.IncreaseLaps();
          break;
        case MenuAction.LessLaps:
          menu.DecreaseLaps();
          break;
        case MenuAction.StartGame:
          menu.StartRace();
          break;
      }
    }
  }
}