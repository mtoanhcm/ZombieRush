using UnityEngine;

namespace ZRCore.Character
{
    public interface ICharacter
    {
        GameObject CharacterObject { get; }
        CharacterID ID { get; }
    }
}
