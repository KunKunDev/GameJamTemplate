using System;
using UnityEngine;

namespace DialogManager
{
    [CreateAssetMenu(fileName = "Statement", menuName = "DialogManager/Statement", order = 1)]
    [Serializable]
    public class Statement : ScriptableObject
    {
        #region Variables
        public string text;        
        public Speaker speaker;
        #endregion
    }
}

