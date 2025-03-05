namespace Game.Serialization.Settings.Input
{
    public enum KeyCodeDescription
    {
        None,
        Interact,
        OpenSettings,
        Info,
    }
    public static class KeyCodeDescriptionExtensions
    {
        /// <summary>
        /// Ref 'menu' text
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static int GetLanguageTextId(this KeyCodeDescription description) => description switch
        {
            KeyCodeDescription.None => -1,
            KeyCodeDescription.Interact => -1,
            KeyCodeDescription.OpenSettings => -1,
            KeyCodeDescription.Info => -1,
            _ => -1
        };
    }
}