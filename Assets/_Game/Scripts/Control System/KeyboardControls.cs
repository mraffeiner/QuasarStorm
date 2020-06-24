using UnityEngine;

public class KeyboardControls : ControlsBase
{
    public override void ReadInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Previous Ship"))
            cycleShip = -1;
        if (Input.GetButtonDown("Next Ship"))
            cycleShip = 1;

        if (Input.GetButtonDown("Previous Weapon"))
            cycleWeapon = -1;
        if (Input.GetButtonDown("Next Weapon"))
            cycleWeapon = 1;

        shoot = Input.GetButton("Shoot");
    }
}
