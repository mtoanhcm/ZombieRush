using System;
using System.Collections;
using UnityEngine;
using ZRCharacter.Config;
using ZRCore.Context;
using ZREvent;
using ZRUtility;

namespace ZRCharacter
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private CharacterConfig enemySpawnConfig;
        [SerializeField]
        private float timeDelaySpawnEnemy;
        [SerializeField]
        private float minDistanceSpawnFromPlayer;
        [SerializeField]
        private float maxDistanceSpawnFromPlayer;

        private Transform mainPlayer;
        private bool canSpawn;

        private void Awake()
        {
            GameplayEvent.OnStarGame += StartCheckSpawnEnemy;
            GameplayEvent.OnGetWinKey += TerminateSpawn;
        }

        private void OnDestroy()
        {
            GameplayEvent.OnStarGame -= StartCheckSpawnEnemy;
            GameplayEvent.OnGetWinKey -= TerminateSpawn;
        }

        private void TerminateSpawn()
        {
            canSpawn = false;
            StopAllCoroutines();
        }

        private void StartCheckSpawnEnemy()
        {
            if(mainPlayer == null)
            {
                if(SceneContext.MainCharacter == null)
                {
                    return;
                }

                mainPlayer = SceneContext.MainCharacter.CharacterObject.transform;
            }

            canSpawn = true;
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            var delayTime = new WaitForSeconds(timeDelaySpawnEnemy);
            while (true)
            {
                if (!canSpawn)
                {
                    yield return delayTime;
                    continue;
                }

                CharacterFactory.SpawnCharacter(enemySpawnConfig.ID, OnCharacterSpawnSuccess);

                yield return delayTime;
            }
        }

        private void OnCharacterSpawnSuccess(Character character)
        {
            mainPlayer.GetPositionAround(minDistanceSpawnFromPlayer, maxDistanceSpawnFromPlayer, ObjectLayer.GroundLayer, ObjectLayer.ObstacleLayer, 1f, out var point);
            character.transform.position = point;
            character.Init(enemySpawnConfig);
        }
    }
}
