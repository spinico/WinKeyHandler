namespace WinKeyHandler
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class LogMessage
        {
            public int Id { get; private set; }
            public string Message { get; private set; }

            public LogMessage(int id, string message)
            {
                Id = id;
                Message = message;
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Attach to static keyboard event handler
            Keyboard.WinKeyPressed += OnWinKeyPressed;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Release static keyboard event handler
            Keyboard.WinKeyPressed -= OnWinKeyPressed;
        }

        /// <summary>
        /// Handle low level windows key combination events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWinKeyPressed(object sender, EventArgs e)
        {
            var key = (e as WinKeyPressedEventArgs).Key;

            string message = String.Empty;

            switch (key)
            {
                case VK.KEY_D:

                    message = "Minimize / Restore all windows to the desktop (WinKey + D)";

                    AddToListview(message);
                    break;

                case VK.KEY_M:

                    message = "Minimize all windows (WinKey + M)";

                    AddToListview(message);
                    break;

                case VK.HOME:
                    if (!IsFocused)
                    {
                        message = "Minimize / Restore all windows other than the active window (WinKey + HOME)";

                        AddToListview(message);
                    }
                    break;

                case VK.LEFT:
                    message = "Moving the window left (WinKey + LEFT)";

                    AddToListview(message);
                    break;

                case VK.RIGHT:
                    message = "Moving the window right (WinKey + RIGHT)";

                    AddToListview(message);
                    break;

                case VK.UP:
                    message = "Maximizing the window (WinKey + UP)";

                    AddToListview(message);
                    break;

                case VK.DOWN:
                    message = "Minimizing or restoring the window from maximized (WinKey + DOWN)";

                    AddToListview(message);
                    break;


            }
        }

        private void AddToListview(string message)
        {
            int count = listView.Items.Count;

            listView.Items.Add(new LogMessage(count, message));

            Debug.Print(message);
        }
    }
}
