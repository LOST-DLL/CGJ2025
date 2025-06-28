using System.Collections.Generic;

namespace Character.Manager
{
    public class CharacterManager:Singleton<CharacterManager>
    {
        public List<CharacterBase> characterList = new List<CharacterBase>();

        public void AddCharacter(CharacterBase character)
        {
            characterList.Add(character);
        }

        public void RemoveCharacterFromList(CharacterBase character)
        {
            characterList.Remove(character);
        }
    }
}