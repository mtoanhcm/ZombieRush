using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZRCharacter;
using ZRCharacter.Config;
using ZRCore.Character;
using ZRCore.Context;
using ZRCore.UI.View;
using ZREvent;
using ZRUI.Gameplay;

namespace ZRGameplay
{
    public class GameplayManager : MonoBehaviour
    {
        public ICharacter MainCharacter => mainCharacter;

        [SerializeField]
        private CharacterConfig mainCharacterConfig;
        [SerializeField]
        private Character mainCharacter;
        [SerializeField]
        private int countdowntStarGame;

        [SerializeField]
        private UISceneViewBase uiSceneView;

        private void OnEnable()
        {
            GameplayEvent.OnGameover += Gameover;
        }

        private void OnDisable()
        {
            GameplayEvent.OnGameover -= Gameover;
        }

        private void Start()
        {
            uiSceneView.ShowView<GameplayUIView>();

            CharacterFactory.Init();

            SceneContext.MainCharacter = mainCharacter;

            StartCoroutine(CountdownStartGame());
        }

        private IEnumerator CountdownStartGame()
        {
            int count = countdowntStarGame;
            var delay = new WaitForSeconds(1);
            while (count > 0)
            {
                GameplayEvent.TriggerCountdownStartGame(count);

                yield return delay;

                count--;
            }

            GameplayEvent.TriggerCountdownStartGame(0);

            yield return delay;

            GameplayEvent.TriggerStartGame();

            mainCharacter.Init(mainCharacterConfig);
        }

        private void Gameover(bool isWin)
        {
            var view = uiSceneView.ShowView<GameoverUIView>();
            view.SetViewInfo(isWin ? "YOU WIN" : "LOSE", RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
