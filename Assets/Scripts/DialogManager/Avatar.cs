using System;
using UnityEngine;

namespace DialogManager
{
    [CreateAssetMenu(fileName = "Avatar", menuName = "DialogManager/Avatar", order = 3)]
    [Serializable]
    public class Avatar : ScriptableObject
    {
        #region Variables
        public Sprite sprite;
        #endregion
    }
}
