using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public class Leaderboard : MonoBehaviour
    {
        public static Leaderboard instance;

        public enum ComparisonType { ZPosition }
        [SerializeField] private ComparisonType comparisonType;

        [Header(" Elements ")]
        [SerializeField] private List<LeaderboardCharacter> characters;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }



        void Update()
        {
            SortCharacters();
        }



        public string GetPlayerPositionString()
        {
            int playerPositionIndex = GetPlayerPosition() + 1;

            if (playerPositionIndex == 1)
                return "1st";
            else if (playerPositionIndex == 2)
                return "2nd";
            else if (playerPositionIndex == 3)
                return "3rd";

            return playerPositionIndex.ToString() + "th";
        }

        public ComparisonType GetComparisonType()
        {
            return comparisonType;
        }

        private int GetPlayerPosition()
        {
            for (int i = 0; i < characters.Count; i++)
                if (characters[i].IsPlayer())
                    return i;

            return 0;
        }

        private void SortCharacters()
        {
            characters.Sort();
        }
    }
}