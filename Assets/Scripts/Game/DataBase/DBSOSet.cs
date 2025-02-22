using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class DBSOSet<TypeSO> where TypeSO : DBScriptableObjectBase
    {
        #region fields & properties
        public IReadOnlyList<TypeSO> Data => data;
        [SerializeField] protected TypeSO[] data;
        #endregion fields & properties

        #region methods
        /// <summary>
        /// Index for <see cref="TypeSO.Id"/>. O(1)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TypeSO this[int id] => data[id];
        /// <summary>
        /// If you want to use index get, use O(1) indexer
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public TypeSO Find(System.Predicate<TypeSO> match)
        {
            int dataCount = data.Length;
            for (int i = 0; i < dataCount; ++i)
            {
                if (match.Invoke(data[i]))
                {
                    return data[i];
                }
            }
            return null;
        }

        internal void CollectAll()
        {
            IEnumerable<TypeSO> found = Resources.FindObjectsOfTypeAll<TypeSO>();
            found = found.OrderBy(x => x.Id);
            if (data.Length != found.Count())
                Debug.Log($"Data in DBSOSet<{typeof(TypeSO)}> is outdated. Update and save it.");
            data = found.ToArray();
        }
        private void CheckObjectId(TypeSO el)
        {
            int elId = el.Id;
            if (data.Count(x => x.Id == elId) > 1)
                Debug.LogError($"Wrong name in {el.name}", el);
        }
        /// <summary>
        /// Don't need to invoke this with <see cref="CatchExceptions(Action{TypeSO}, string)"/>
        /// </summary>
        internal void CatchDefaultExceptions()
        {
            foreach (TypeSO el in Data)
            {
                CheckObjectId(el);
            }
        }
        internal void CatchExceptions(System.Func<TypeSO, bool> exceptionMatch, Exception e, string errorMessage) => CatchExceptions(x => _ = exceptionMatch.Invoke(x) ? throw e : 0, errorMessage);
        internal void CatchExceptions(System.Action<TypeSO> checkAction, string errorMessage)
        {
            foreach (TypeSO el in Data)
            {
                CheckObjectId(el);
                try { checkAction.Invoke(el); }
                catch (Exception e) { Debug.LogError($"{el.name}: Error message: <color=#FF0022>{errorMessage}</color>. Exception: {e.Message}", el); }
            }
        }
        #endregion methods
    }
}