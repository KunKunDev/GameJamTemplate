using Debugging;
using InputManager;

namespace DialogManager
{
    public class DialogController
    {
        #region Variables
        private Dialog dialog;
        #endregion

        #region Ctor
        public DialogController(Dialog dialog)
        {
            LoadDialogObjects(dialog);
            InputSystem.OnButton += InputSystem_OnButton;
        }
        #endregion

        #region Dtor
        ~DialogController()
        {

        }
        #endregion

        #region Class Methods
        private void LoadDialogObjects(Dialog dialog)
        {
            if (dialog.statements.Count > 0)
            {
                this.dialog = dialog;
                this.dialog.currentStatement = this.dialog.statements[0];
                this.dialog.currentIndex = 0;
            }
        }

        public void ReadDebugDialog()
        {
            DebugTools.Log(dialog.currentStatement.speaker.name
                + " : "
                + dialog.currentStatement.text);
        }

        public Statement GetCurrentStatement()
        {
            return dialog.currentStatement;
        }

        public void NextStatement()
        {
            if (dialog.currentIndex + 1 < dialog.statements.Count)
            {
                dialog.currentIndex++;
                dialog.UpdateStatement();
            }
            else
            {
                //dialog ends
            }
        }

        public void PreviousStatement()
        {
            if (dialog.currentIndex - 1 >= 0)
            {
                dialog.currentIndex--;
                dialog.UpdateStatement();
            }
            else
            {
                //first statement
            }
        }

        public void FirstStatement()
        {
            dialog.currentIndex = 0;
            dialog.UpdateStatement();
        }

        public void LastStatement()
        {
            dialog.currentIndex = dialog.statements.Count - 1;
            dialog.UpdateStatement();
        }
        #endregion

        #region Controller Events
        private void InputSystem_OnButton(int index, InputButton button, InputState state)
        {
            if(button == InputButton.A && state == InputState.Down)
            {
                NextStatement();
            }
        }
        #endregion
    }
}