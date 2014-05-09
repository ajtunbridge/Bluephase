#region Using directives

using System.Windows.Forms;
using Bluephase.Common;

#endregion

namespace Bluephase
{
    /// <summary>
    /// Implementation of <see cref="IDialogService"/> using the .NET MessageBox class
    /// </summary>
    public sealed class StandardDialogService : IDialogService
    {
        #region IDialogService Members

        public void ShowError(string errorMessage, string title)
        {
            MessageBox.Show(errorMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public AskResult Ask(string question, string title)
        {
            return Ask(question, title, false);
        }

        public AskResult Ask(string question, string title, bool canCancel)
        {
            var buttons = canCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;

            var dialogResult = MessageBox.Show(question, title, buttons, MessageBoxIcon.Question);

            switch (dialogResult) {
                case DialogResult.Yes:
                    return AskResult.Yes;
                case DialogResult.No:
                    return AskResult.No;
                case DialogResult.Cancel:
                    return AskResult.Cancel;
                default:
                    return AskResult.Cancel;
            }
        }

        #endregion
    }
}