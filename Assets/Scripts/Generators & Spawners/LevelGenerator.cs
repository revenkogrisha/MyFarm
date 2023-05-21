using CasualSlasher.Characters;
using CasualSlasher.Level;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CasualSlasher.Generators
{
    public class LevelGenerator
    {
        private ISurfaceGenerator _surfaceGenerator;
        private INavMeshSurfaceGenerator _navGenerator;
        private CharacterSpawner _characterSpawner;
        private PlayerCharacter _playerPrefab;

        public event Action<PlayerCharacter> OnPlayerSpawned;

        public LevelGenerator(
            ISurfaceGenerator surfaceGenerator, 
            CharacterSpawner characterSpawner, 
            PlayerCharacter playerPrefab)
        {
            _surfaceGenerator = surfaceGenerator;
            _navGenerator = new NavMeshSurfaceGenerator();
            _characterSpawner = characterSpawner;
            _playerPrefab = playerPrefab;
        }

        public void GenerateLevel(
            NavMeshSurface navSurface,
            Vector3 playerSpawnPosition)
        {
            GenerateSuface(navSurface);

            SpawnPlayer(playerSpawnPosition);
        }

        private Finish GenerateSuface(NavMeshSurface surface)
        {
            var finish = _surfaceGenerator.GenerateSurface();
            _navGenerator.GenerateSurface(surface);
            return finish;
        }

        private void SpawnPlayer(Vector3 playerSpawnPosition)
        {
            var player = _characterSpawner.Spawn(_playerPrefab, playerSpawnPosition);
            OnPlayerSpawned?.Invoke(player);
        }
    }
}
