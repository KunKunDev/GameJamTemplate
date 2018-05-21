using UnityEngine;
using UnityEngine.UI;

namespace DialogManager
{
    public class DialogView : MonoBehaviour
    {
        #region Inspector Variables
        [SerializeField]
        private Dialog helloWorld;
        [SerializeField]
        private Image avatarImage;
        [SerializeField]
        private Text avatarName;
        [SerializeField]
        private Text statementText;
        #endregion

        #region Variables
        private Dialog dialog;
        private DialogController dialogCtrl;
        #endregion

        #region Mono Methods
        private void Awake()
        {
            LoadDialog(helloWorld);
        }
        #endregion

        #region Class Methods
        public void LoadDialog(Dialog dialog)
        {
            this.dialog = dialog;
            dialog.RefreshView = DisplayCurrentStatement;
            dialogCtrl = new DialogController(dialog);

            DisplayCurrentStatement();
        }

        public void DisplayCurrentStatement()
        {
            avatarImage.sprite = dialog.currentStatement.speaker.avatar.sprite;
            avatarName.text = dialog.currentStatement.speaker.avatar.name;
            statementText.text = dialog.currentStatement.text;
        }
        #endregion
    }
}