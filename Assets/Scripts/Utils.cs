using UnityEngine;

namespace Utils {
    public class ControllerUTE {
        public static Controller GetController(GameObject other)
        {
            // Get the pawn
            CharacterPawn pawn = other.GetComponent<CharacterPawn>();
    
            // If pawn doesn't have a controller, return null. 
            if (!pawn || !pawn.controller) return null;
    
            // Get the Controller from the pawn
            return pawn.controller.GetComponent<Controller>();
        }
    
        public static Controller GetPlayerController(GameObject other)
        {
            // Get the controller
            Controller controller = GetController(other);
            if (!controller) return null;
    
            // If the controller is not a PlayerController, it'll be null
            return controller.GetComponent<PlayerController>();
        }
    }
}