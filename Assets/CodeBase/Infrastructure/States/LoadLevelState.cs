using System;
using CodeBase.Logic.Player;
using CodeBase.Services.Data;
using CodeBase.Services.Factory;
using CodeBase.Services.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _gameStateMachine;
        private GameStateMachine _stateMachine;
        private IPrefabFactory _prefabFactory;
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(GameStateMachine stateMachine, ISceneLoader sceneLoader, IPrefabFactory prefabFactory, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
            _prefabFactory = prefabFactory;
            _stateMachine = stateMachine;
        }

        public void Enter(string name)
        {
            _sceneLoader.LoadAsync(name, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            LevelData levelData = _staticDataService.GetLevels()[SceneManager.GetActiveScene().name];
            
            GameObject player = CreatePlayer(levelData);
            CreateCube(levelData, player);
        }

        private GameObject CreatePlayer(LevelData levelData) => 
            _prefabFactory.CreatePlayer(levelData.SpawnPoint);

        private void CreateCube(LevelData levelData, GameObject player)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            
            if (movement == null)
                throw new Exception("No movement on player, can't spawn cube");
            
            _prefabFactory.CreateCube(levelData.SpawnPoint, movement);
        }
    }
}