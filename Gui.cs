namespace EU4_Parse_Lib
{
    public static class Gui
    {
        /// <summary>
        /// Checks if any Form is shown or disposed and will either bring it
        /// to the front or create it. Use to prevent several instances of one
        /// form from being opened.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ShowForm<T>() where T : Form, new()
        {
            Form? form = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (form == null || form.IsDisposed)
            {
                form = new T();
                form.Show();
                form.BringToFront();
            }
            else
                form.BringToFront();
            return (T)form;
        }
        /// <summary>
        /// Selects everything inside of a given text box
        /// </summary>
        /// <param name="textBox"></param>
        public static void SelectOnEnter(TextBox textBox)
        {
            textBox.BeginInvoke((MethodInvoker)textBox.SelectAll);
        }
        /// <summary>
        /// Selects everything inside of a given combo box
        /// </summary>
        /// <param name="textBox"></param>
        public static void SelectAllOnFocus(this TextBox textBox)
        {
            textBox.Enter += (sender, e) =>
            {
                textBox.BeginInvoke((MethodInvoker)textBox.SelectAll);
            };
        }
        /// <summary>
        /// Puts the cursor at the end of the text. Useful for texts longer that the Texbox can show
        /// </summary>
        /// <param name="textBox"></param>
        public static void SetCursorToEnd(this TextBox textBox)
        {
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}