using System.Collections.Generic;
using UnityEngine;

namespace ZRCore.UI.View
{
    public class UISceneViewBase : MonoBehaviour
    {
        private List<UIViewBase> uiViews;

        private void Awake()
        {
            uiViews = new List<UIViewBase>(GetComponentsInChildren<UIViewBase>(true));
        }

        public T ShowView<T>() where T : UIViewBase
        {
            foreach (var view in uiViews)
            {
                if (view is T)
                {
                    view.Show();
                    return view as T;
                }
            }

            return null;
        }

        public T ShowViewAndCloseOtherView<T>() where T : UIViewBase {
            T targetView = null;
            foreach (var view in uiViews)
            {
                if (view is T)
                {
                    view.Show();
                    targetView = view as T;
                    continue;
                }

                view.Hide();
            }

            return targetView;
        }

        public void HideView<T>() where T : UIViewBase
        {
            foreach (var view in uiViews)
            {
                if (view is T)
                {
                    view.Hide();
                    return;
                }
            }
        }

        public T GetView<T>() where T : UIViewBase
        {
            foreach (var view in uiViews)
            {
                if (view is T)
                {
                    return view as T;
                }
            }

            Debug.LogError($"View of type {typeof(T).Name} not found.");
            return null;
        }
    }
}
