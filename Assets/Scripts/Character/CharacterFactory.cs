using System.Collections.Generic;
using ZRCore.Pool;
using ZRCore.Character;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using ZRUtility;
using UnityEngine;

namespace ZRCharacter
{
    public static class CharacterFactory
    {
        private static Dictionary<CharacterID, ObjectPool<Character>> characterPool;

        public static void Init()
        {
            characterPool = new Dictionary<CharacterID, ObjectPool<Character>>();
        }

        public static async UniTask SpawnCharacter(CharacterID characterID, Action<Character> onSpawnCharacterSuccess)
        {
            if (!characterPool.ContainsKey(characterID)) { 
                await CreateCharacterPool(characterID);
            }

            var character = characterPool[characterID].GetObject();
            if (character == null)
            {
                return;
            }

            onSpawnCharacterSuccess?.Invoke(character);
        }

        public static void DespawnCharacter(Character character)
        {
            if (!characterPool.ContainsKey(character.ID))
            {
                UnityEngine.Object.Destroy(character);
                return;
            }

            characterPool[character.ID].ReturnObject(character);
        }

        private static async Task<UniTask> CreateCharacterPool(CharacterID characterID)
        {
            var characterObj = await AddressableUtility.LoadAssetAsync<GameObject>($"Prefab/Character/{characterID}.prefab");
            if (characterObj == null) {
                Debug.LogError($"Cannot load character {characterID} object");
                return UniTask.CompletedTask;
            }

            if(!characterObj.TryGetComponent<Character>(out var character))
            {
                Debug.LogError($"{characterID} Cannot get character component ");
                return UniTask.CompletedTask;
            }

            characterPool.Add(characterID, new ObjectPool<Character>(character, 20));

            return UniTask.CompletedTask;
        }
    }
}
