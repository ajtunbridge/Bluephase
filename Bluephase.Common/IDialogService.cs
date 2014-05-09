namespace Bluephase.Common
{
    public interface IDialogService
    {
        /// <summary>
        /// Shows an error dialog to the user with the specified message
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        /// <param name="title">A title for this error dialog</param>
        void ShowError(string errorMessage, string title);

        /// <summary>
        /// Shows a dialog to the user with the specified message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">A title for this message dialog</param>
        void ShowMessage(string message, string title);

        /// <summary>
        /// Shows a dialog to the user with the specified message and returns
        /// the user's response in the form of a <see cref="AskResult"/> enum
        /// </summary>
        /// <param name="question">The question to ask the user</param>
        /// <param name="title">A title for this question dialog</param>
        /// <returns></returns>
        AskResult Ask(string question, string title);

        /// <summary>
        /// Shows a dialog to the user with the specified message and returns the user's 
        /// response in the form of a <see cref="AskResult"/> enum. Optionally gives the 
        /// user an option to cancel instead of just yes or no responses
        /// </summary>
        /// <param name="question">The question to ask the user</param>
        /// <param name="title">A title for this question dialog</param>
        /// <param name="canCancel">Whether the user can choose to cancel instead of answer yes or no</param>
        /// <returns></returns>
        AskResult Ask(string question, string title, bool canCancel);
    }

    public enum AskResult
    {
        Yes,
        No,
        Cancel
    }
}