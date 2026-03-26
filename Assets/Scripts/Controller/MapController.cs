using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    [SerializeField] public List<ArenaController> mapArenas = new List<ArenaController>(); 
    [SerializeField] private MainCharacterController mainCharacter;
    [SerializeField] public ArenaController mapMain;

    private bool isAtHome = true;

    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        isAtHome = true;
        UIController.Instance.ShowGameUI();
        MythicalController.Instance.SetUp();
        UITime.OnProgessEnd += MoveToMap;
    }

    public void MoveToMap()
    {
        if (isAtHome)
        {
            MoveToMapEnemy();
        }
        else
        {
            MoveToMapHome();
        }

        UIController.Instance.ResetProgess();
    }

    private void MoveToMapEnemy()
    {
        int index = UnityEngine.Random.Range(0, mapArenas.Count);
        ArenaController targetArena = mapArenas[index];
        ActualMove(targetArena);
        isAtHome = false;
    }

    private void MoveToMapHome()
    {
        ActualMove(mapMain);
        isAtHome = true;
    }

    private void ActualMove(ArenaController arena)
    {
        CameraController.Instance.FollowTarget(arena);
        mainCharacter.transform.position = arena.PointEnemyMove.position;
        mainCharacter.StatusMoveToMapEnemy();
    }

    public void ViewMapOnly(ArenaController arena)
    {
        if (arena == null) return;

        CameraController.Instance.FollowTarget(arena);
        ActualMove(arena);
    }
}