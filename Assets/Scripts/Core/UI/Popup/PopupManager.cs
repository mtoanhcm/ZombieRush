using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZRCore.UI.Popup
{
    public class PopupManager : MonoBehaviour
    {
        private Dictionary<string, PopupBase> popups;

        private void Awake()
        {
            popups = new Dictionary<string, PopupBase>();
        }

        public TPopup ShowPopup<TPopup,TData>(string popupType,TData popupData) 
            where TPopup : PopupBase
            where TData : PopupData
        {
            Debug.Log($"{gameObject.scene.name}");
            if (popups.ContainsKey(popupType)) {
                var targetPopup = popups[popupType] as TPopup; 
                targetPopup.Show(popupData);
                return targetPopup;
            }

            var popupPrefab = Resources.Load<GameObject>($"Popup/{popupType}");
            if (popupPrefab == null) {
                Debug.LogError($"Cannot load popup {popupType} prefab");
                return null;
            }

            var popup = Instantiate(popupPrefab, transform).GetComponent<TPopup>();
            popup.Show(popupData);

            popups.Add(popupType, popup);

            return popup;
        }
    }
}
