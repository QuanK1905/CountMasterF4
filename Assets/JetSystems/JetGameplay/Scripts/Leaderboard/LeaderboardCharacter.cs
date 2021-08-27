using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JetSystems
{
    [RequireComponent(typeof(JetCharacter))]
    public class LeaderboardCharacter : MonoBehaviour, IComparable
    {
        private JetCharacter.CharacterType characterType;

        private void Start()
        {
            characterType = GetComponent<JetCharacter>().GetCharacterType();
        }

        public bool IsPlayer()
        {
            return characterType == JetCharacter.CharacterType.Player;
        }

        public int CompareTo(object obj)
        {
            LeaderboardCharacter otherPlayerMovement = (LeaderboardCharacter)obj;

            switch(Leaderboard.instance.GetComparisonType())
            {
                case Leaderboard.ComparisonType.ZPosition:
                    return otherPlayerMovement.transform.position.z.CompareTo(transform.position.z);

                default:
                    return otherPlayerMovement.transform.position.z.CompareTo(transform.position.z);
            }
        }
    }
}