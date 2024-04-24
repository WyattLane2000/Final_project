public static class GameEvent
{
    public const string TWOD_PAUSED = "TWOD_PAUSED";//pause state control for 2D
    public const string TWOD_RESUMED = "TWOD_RESUMED";//pause state control for 2D
    public const string THREED_PAUSED = "THREED_PAUSED";//pause state control for 3D
    public const string THREED_RESUMED = "THREED_RESUMED";//pause state controlfor 3D

    public const string POPUP_OPENED = "POPUP_OPENED";//managing ui popups
    public const string POPUP_CLOSED = "POPUP_CLOSED";//managing ui popups
    public const string GAME_ACTIVE = "GAME_ACTIVE";//not much code doen't work so i used my own pause code
    public const string GAME_INACTIVE = "GAME_INACTIVE";//not much code doen't work so i used my own pause code

    public const string PLAYER_HEALTH_CHANGED = "PLAYER_HEALTH_CHANGED";//update player health
    public const string SHIP_HEALTH_CHANGED = "SHIP_HEALTH_CHANGED";//update ship health
    public const string PLAYER_DEAD = "PLAYER_DEAD";//end game

    public const string BOOST_HIT = "BOOST_HIT";//boost pickup
    public const string SHIP_DAMAGE = "SHIP_DAMAGE";
    public const string REPAIR = "REPAIR";//repair pickup

    public const string TWOD_PLAYING = "TWOD_PLAYING";//ui pause state control
    public const string THREED_PLAYING = "THREED_PLAYING";//ui pause state control

    public const string ROBO_ENEMY_DEAD = "ROBO_ENEMY_DEAD";//to increase count
    public const string FAC_ENEMY_ON = "FAC_ENEMY_ON";//turn on Fac's enemys
    public const string AIRLOCK_SWITCH = "AIRLOCK_SWITCH";//swap doors close/open
    public const string FAC_ONE_COLLECTED = "FAC_ONE_COLLECTED";//to collect cyrstal
    public const string FAC_TWO_COLLECTED = "FAC_TWO_COLLECTED";//to collect cyrstal
    public const string FAC_THREE_COLLECTED = "FAC_THREEE_COLLECTED";//to collect cyrstal
    public const string FAC_ONE_HIT = "FAC_ONE_HIT";//to activate fac one and swap to 3D
    public const string FAC_TWO_HIT = "FAC_TWO_HIT";//to activate fac 2 and swap to 3D
    public const string FAC_THREE_HIT = "FAC_THREE_HIT";//to activate fac 3 and swap to 3D
    public const string SWAP_GAME = "SWAP_GAME";//to swap back to 2D
}