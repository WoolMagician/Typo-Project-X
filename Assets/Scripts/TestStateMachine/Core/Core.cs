using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => movement;
        private set => movement = value;
    }
    public CollisionSenses CollisionSenses
    {
        get => collisionSenses;
        private set => collisionSenses = value;
    }

    public CharacterController CharacterController
    {
        get => characterController;
        private set => characterController = value;
    }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private CharacterController characterController;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        characterController = GetComponentInParent<CharacterController>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
