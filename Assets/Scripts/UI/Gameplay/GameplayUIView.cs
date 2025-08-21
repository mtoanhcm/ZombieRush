using UnityEngine;
using ZRCore.UI.View;
using TMPro;
using ZREvent;
using System;

namespace ZRUI.Gameplay
{
    public class GameplayUIView : UIViewBase
    {
        [Header("Countdown start game")]
        [SerializeField]
        private GameObject countdownGameStartGroup;
        [SerializeField]
        private TextMeshProUGUI countdownTxt;

        //[Header("Gameplay Group")]
        //[SerializeField]

        private void OnEnable()
        {
            GameplayEvent.OnCountdownStarGame += CountdownStartGame;
            GameplayEvent.OnStarGame += StartGame;
        }

        private void OnDisable()
        {
            GameplayEvent.OnCountdownStarGame -= CountdownStartGame;
            GameplayEvent.OnStarGame -= StartGame;
        }

        public override void Show()
        {
            countdownGameStartGroup.SetActive(false);

            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void StartGame()
        {
            countdownGameStartGroup.SetActive(false);
        }

        private void CountdownStartGame(int countdownValue)
        {
            countdownGameStartGroup.SetActive(true);

            var countdownStr = countdownValue > 0 ? countdownValue.ToString() : "GO";

            countdownTxt.SetText(countdownStr);
        }
    }
}
