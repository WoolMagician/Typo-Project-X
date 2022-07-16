using UnityEngine;

public enum GameState
{
    Gameplay, //regular state: player moves, attacks, can perform actions
    Pause, //pause menu is opened, the whole game world is frozen
    Inventory, //when inventory UI is open
    Dialogue,
    Cutscene,
    LocationTransition, //when the character steps into LocationExit trigger, fade to black begins and control is removed from the player
}

//[CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState", order = 51)]
public class GameStateSO : DescriptionBaseSO
{
    public GameState CurrentGameState => _currentGameState;

    [Header("Game states")]
    [SerializeField] [ShowOnly] private GameState _currentGameState = default;
    [SerializeField] [ShowOnly] private GameState _previousGameState = default;

    private void Start()
    {
    }

    public void UpdateGameState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;

        _previousGameState = _currentGameState;
        _currentGameState = newGameState;
    }

    public void ResetToPreviousGameState()
    {
        if (_previousGameState == _currentGameState)
            return;

        GameState stateToReturnTo = _previousGameState;
        _previousGameState = _currentGameState;
        _currentGameState = stateToReturnTo;
    }
}