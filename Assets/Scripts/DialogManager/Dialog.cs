using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogManager
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "DialogManager/Dialog", order = 0)]
    [Serializable]
    public class Dialog : ScriptableObject
    {
        #region Variables
        public List<Statement> statements;
        [HideInInspector]
        public Statement currentStatement;
        [HideInInspector]
        public int currentIndex = -1;
        #endregion

        #region Delegate
        public delegate void Refresh();
        public Refresh RefreshView;
        #endregion

        #region Class 
        public void UpdateStatement()
        {
            currentStatement = statements[currentIndex];
            RefreshView();
        }
        #endregion
    }
}
