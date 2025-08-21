using UnityEngine;

namespace ZRCore.UI.Popup
{
    public abstract class PopupBase : MonoBehaviour
    {
        public virtual void Show<T>(T data) where T : PopupData
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
