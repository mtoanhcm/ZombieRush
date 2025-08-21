using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ZRCore.UI.View;

namespace ZRUI.Gameplay
{
    public class GameoverUIView : UIViewBase
    {
        [SerializeField]
        private TextMeshProUGUI reulstTxt;
        [SerializeField]
        private Button restartGameBtn;

        public void SetViewInfo(string result, UnityAction restartGameAction)
        {
            reulstTxt.SetText(result);

            restartGameBtn.onClick.RemoveAllListeners();
            if (restartGameAction != null) { 
                restartGameBtn.onClick.AddListener(restartGameAction);
            }
        }
    }
}
