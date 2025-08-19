using UnityEngine;
using ZRCore.Character;

namespace ZRCharacter.Config
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/Character/CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterID ID;
        public float Health;
        public float MoveSpeed;
        public float JumPower;
    }
}
