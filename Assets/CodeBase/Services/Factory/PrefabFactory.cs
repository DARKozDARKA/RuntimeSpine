using CodeBase.Logic.Cube;
using CodeBase.Logic.Player;
using CodeBase.Services.AssetManagment;
using CodeBase.StaticData;
using CodeBase.Tools;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factory
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;

        public PrefabFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _container = container;
        }

        public GameObject CreatePlayer(Vector3 at) =>
            _assetProvider.Instantiate(PrefabsPath.Player, at)
                .With(_ => _container.InjectGameObject(_));

        public void CreateCube(Vector3 at, PlayerMovement playerMovement) =>
            _assetProvider.Instantiate(PrefabsPath.Cube, at)
                .With(_ => _.GetComponent<ObjectFollower>()?.SetTarget(playerMovement.transform))
                .GetComponent<ChangeMaterialOnPlayerDash>()
                .SetPlayerMovement(playerMovement);
    }
}