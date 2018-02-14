using System;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace SoundBoardScript
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var ex = (Exception)args.ExceptionObject;
                MessageBox.Show($"An unhandled exception was thrown.\n{ex.Message}\n{ex.StackTrace}");
            };
            InitializeComponent();
        }

        public static SoundPlayer Player { get; set; } = new SoundPlayer();

        private void ImportScript_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Script file|*.script|All files|*",
                FilterIndex = 1,
                InitialDirectory = Environment.CurrentDirectory,
                Multiselect = false,
                Title = "Select a .script file to import"
            };
            if (window.ShowDialog(this) == true)
            {
                var str = File.ReadAllText(window.FileName);
                ScriptBox.Document = new FlowDocument();
                ParseScriptFile(str);
            }
        }

        private void AddButton(Button button)
        {
            ((Paragraph)ScriptBox.Document.Blocks.LastBlock).Inlines.Add(button);
        }

        public void ParseScriptFile(string str)
        {
            var split = Regex.Split(str, @"\[\[.+\]\]");
            var matches = Regex.Matches(str, @"\[\[.+\]\]");
            for (var i = 0;; i++)
            {
                var breakNow = true;
                if (split.Length > i)
                {
                    ScriptBox.AppendText(split[i]);
                    breakNow = false;
                }

                if (matches.Count > i)
                {
                    AddButton(ConvertRegexMatchIntoButton(matches[i].Value));
                    breakNow = false;
                }

                if (breakNow)
                {
                    break;
                }
            }
        }

        private Button ConvertRegexMatchIntoButton(string match)
        {
            match = match.Replace("[", "").Replace("]", "");
            var splitted = match.Split('|');
            var content = "";
            var fileName = "";
            foreach (var split in splitted)
            {
                var anotherSplit = split.Split('='); // readability level over 9000
                switch (anotherSplit[0].ToLower())
                {
                    case "file":
                        fileName = anotherSplit[1];
                        break;
                    case "text":
                        content = anotherSplit[1];
                        break;
                    default:
                        throw new NotImplementedException("The type of text is not valid!");
                }
            }
            
            var button = new Button {IsEnabled = true, Content = content};
            button.MouseEnter += delegate
            {
                Player = new SoundPlayer(fileName);
                Player.LoadAsync();
            };
            button.Click += delegate { Player.Play(); };
            return button;
        }

        private void StopMusic_OnClick(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }
    }
}
