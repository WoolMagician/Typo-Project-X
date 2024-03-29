using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState = default;

    [Header("Inventory")]
    [SerializeField] private InventorySO _inventory = default;

    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    void StartGame()
    {
        _gameState.UpdateGameState(GameState.Gameplay);
    }
}