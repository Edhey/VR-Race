using UnityEngine;

namespace Whisper.Samples {
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

        public MenuAction actionType;

        public override void Execute(GameObject target) {
            // Buscamos el gestor del menú en el objeto que recibe la orden
            var menu = target.GetComponent<MainMenuManager>();
            if (menu == null)
                return;

            switch (actionType) {
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