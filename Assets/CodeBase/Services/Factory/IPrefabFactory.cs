using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IPrefabFactory
    {
        GameObject CreatePlayer(Vector3 at);
        void CreateCube(Vector3 at, PlayerMovement playerMovement);
    }
}