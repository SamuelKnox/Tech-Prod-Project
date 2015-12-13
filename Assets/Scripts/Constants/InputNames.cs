public class InputNames
{
    public InputNames(int playerNumber)
    {
        HorizontalMovement = "Horizontal Movement " + playerNumber;
        VerticalMovement = "Vertical Movement " + playerNumber;
        Jump = "Jump " + playerNumber;
        ChopAttack = "Chop Attack " + playerNumber;
        LungeAttack = "Lunge Attack " + playerNumber;
        Dash = "Dash " + playerNumber;
    }

    /// <summary>
    /// Input name for the player's horizontal movement
    /// </summary>
    public readonly string HorizontalMovement;

    /// <summary>
    /// Input name for the player's vertical movement
    /// </summary>
    public readonly string VerticalMovement;

    /// <summary>
    /// Input name for the player's jump
    /// </summary>
    public readonly string Jump;

    /// <summary>
    /// Input name for swinging the player's melee weapon overhead
    /// </summary>
    public readonly string ChopAttack;

    /// <summary>
    /// Input name for lunging the player's melee weapon
    /// </summary>
    public readonly string LungeAttack;

    /// <summary>
    /// Input name for the player dodging
    /// </summary>
    public readonly string Dash;
}