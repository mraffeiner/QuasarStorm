public abstract class ControlsBase
{
    public float verticalInput = 0f;
    public float horizontalInput = 0f;
    public bool shoot = false;
    public int cycleShip = 0;
    public int cycleWeapon = 0;

    public abstract void ReadInput();

    public void ClearInput()
    {
        verticalInput = 0f;
        horizontalInput = 0f;
        shoot = false;
        cycleShip = 0;
        cycleWeapon = 0;
    }
}
