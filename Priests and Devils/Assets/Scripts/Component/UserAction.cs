using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UserAction {
    void MoveBoat();
    void MoveRole(Role roleModel);
    void Check();
    void RestartGame();
}
