using UnityEngine;
using ZRCharacter;
using ZRCharacter.Config;
using ZRCore.Character;
using ZRCore.Context;
using ZREvent;

namespace ZRGameplay
{
    public class GameplayManager : MonoBehaviour
    {
        public ICharacter MainCharacter => mainCharacter;

        [SerializeField]
        private CharacterConfig mainCharacterConfig;
        [SerializeField]
        private Character mainCharacter;

        private void Start()
        {
            CharacterFactory.Init();

            mainCharacter.Init(mainCharacterConfig);

            SceneContext.MainCharacter = mainCharacter;

            GameplayEvent.TriggerStartGame();
        }
    }
}
