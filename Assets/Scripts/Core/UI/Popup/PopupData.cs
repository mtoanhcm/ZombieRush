using System;

namespace ZRCore.UI.Popup
{
    public abstract class PopupData
    {
        public Action<PopupBase> SubmitAction { get; set; }
        public Action CancelAction { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
