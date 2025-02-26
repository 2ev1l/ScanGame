using Game.DataBase;
using Game.UI.Text;

namespace Game.UI.Collections
{
    public class QualityItem : IntItem
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override void OnListUpdate(int param)
        {
            value = param;
            int textId = value switch
            {
                0 => 14,
                1 => 15,
                2 => 16,
                _ => -1
            };
            Text.text = LanguageLoader.GetTextByType(TextType.Menu, textId);
        }
        #endregion methods
    }
}