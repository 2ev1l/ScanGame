using UnityEngine;
using Game.UI.Text;
using Game.DataBase;

namespace Game.UI.Collections
{
    public class ScreenModeItem : IntItem
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override void OnListUpdate(int param)
        {
            int textId = (FullScreenMode)param switch
            {
                FullScreenMode.ExclusiveFullScreen => 22,
                FullScreenMode.FullScreenWindow => 23,
                FullScreenMode.Windowed => 24,
                _ => -1
            };
            Text.text = LanguageLoader.GetTextByType(TextType.Menu, textId);
            value = param;
        }
        #endregion methods
    }
}