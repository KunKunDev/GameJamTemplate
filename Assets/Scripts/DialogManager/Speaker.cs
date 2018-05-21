using System;
using UnityEngine;

namespace DialogManager
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "DialogManager/Speaker", order = 2)]
    [Serializable]
    public class Speaker : ScriptableObject
    {
        #region Variables
        public string name;
        public Avatar avatar;
        #endregion
    }
}