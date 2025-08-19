using UnityEngine;
using ZRCharacter;

namespace ZRGameplay
{
    public class GameplayManager : MonoBehaviour
    {
        private void Start()
        {
            CharacterFactory.Init();
        }
    }
}
