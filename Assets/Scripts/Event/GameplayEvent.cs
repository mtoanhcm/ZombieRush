using System;
using UnityEngine;

namespace ZREvent
{
    public static class GameplayEvent
    {
        public static event Action OnStarGame;
        public static event Action<bool> OnGameover;
        public static event Action OnGetWinKey;
        public static event Action<int> OnCountdownStarGame;

        public static void TriggerCountdownStartGame(int countdown)
        {
            OnCountdownStarGame?.Invoke(countdown);
        }

        public static void TriggerGetWinKey()
        {
            OnGetWinKey?.Invoke();
        }

        public static void TriggerStartGame()
        {
            OnStarGame?.Invoke();
        }

        public static void TriggerGameover(bool isWin)
        {
            OnGameover?.Invoke(isWin);
        }
    }
}
