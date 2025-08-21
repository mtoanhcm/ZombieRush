using UnityEngine;

namespace ZRCore.UI.View
{
    public abstract class UIViewBase : MonoBehaviour
    {
        private bool isInit;

        public virtual void Show()
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }

            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Init() { 
        
        }
    }
}
