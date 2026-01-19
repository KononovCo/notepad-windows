using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using Notepad.Properties;

namespace Notepad
{
    public class NotepadEdit : Access
    {
        private void HideInfo()
        {
            GetMain().Info.Visibility = Visibility.Collapsed;
            GetMain().PositionMatch.Visibility = Visibility.Collapsed;
        }

        public void Copy()
        {
            if (GetMain().TextBox.SelectedText != "")
            {
                GetField().IsCopy = true;

                Clipboard.SetDataObject(GetMain().TextBox.SelectedText);
            }
        }

        public void Paste() => GetMain().TextBox.Paste();

        public void Cut()
        {
            if (GetMain().TextBox.SelectedText != "")
            {
                GetField().IsCopy = true;

                Clipboard.SetDataObject(GetMain().TextBox.SelectedText);

                GetMain().TextBox.SelectedText = "";
            }
        }

        public void SelectAll() => GetMain().TextBox.SelectAll();
        public void Delete() => GetMain().TextBox.SelectedText = "";

        public void Find()
        {
            GetMain().PanelFind.Visibility = Visibility.Visible;

            GetMain().SearchFind.Text = Settings.Default.Find;
            GetMain().SearchReplace.Text = Settings.Default.Replace;
            GetMain().IsRegister.IsChecked = Settings.Default.IsRegister;
            GetMain().IsCountMatches.IsChecked = Settings.Default.IsCountMatches;
            GetMain().IsRegularExpression.IsChecked = Settings.Default.IsRegularExpression;
        }

        public void SaveFindCheckedChanged()
        {
            GetField().MatchPosition = 0;

            Settings.Default.Find = GetMain().SearchFind.Text;
            Settings.Default.Replace = GetMain().SearchReplace.Text;
            Settings.Default.IsRegister = (bool)GetMain().IsRegister.IsChecked;
            Settings.Default.IsCountMatches = (bool)GetMain().IsCountMatches.IsChecked;
            Settings.Default.IsRegularExpression = (bool)GetMain().IsRegularExpression.IsChecked;
            Settings.Default.Save();
        }

        public void ButtonFind(object sender)
        {
            string name = (sender as Button).Name;

            try
            {
                if (name != GetMain().Close.Name)
                {
                    Regex regex = null;

                    string input = GetMain().TextBox.Text;
                    string ordinary = GetMain().SearchFind.Text;
                    string escape = Regex.Escape(GetMain().SearchFind.Text);

                    if (ordinary == "") throw new ArgumentOutOfRangeException();
                    else if (ordinary != Settings.Default.Find) SaveFindCheckedChanged();

                    if ((bool)GetMain().IsRegister.IsChecked)
                    {
                        if ((bool)GetMain().IsRegularExpression.IsChecked) regex = new Regex(ordinary);
                        else regex = new Regex(escape);
                    }

                    else
                    {
                        if ((bool)GetMain().IsRegularExpression.IsChecked) regex = new Regex(ordinary, RegexOptions.IgnoreCase);
                        else regex = new Regex(escape, RegexOptions.IgnoreCase);
                    }

                    MatchCollection matches = regex.Matches(input);

                    if (matches.Count != 0)
                    {
                        GetMain().TextBox.Focus();
                        GetMain().Info.Visibility = Visibility.Visible;

                        if (name == GetMain().Find.Name)
                        {
                            GetMain().TextBox.CaretIndex = matches[GetField().MatchPosition].Index;
                            GetMain().TextBox.Select(matches[GetField().MatchPosition].Index, matches[GetField().MatchPosition].Length);

                            GetMain().Info.Content = "Найдено совпадений: " + GetMethod().NumberFormat(matches.Count);
                            GetMain().PositionMatch.Content = "Позиция: " + GetMethod().NumberFormat(GetField().MatchPosition + 1);

                            GetMain().PositionMatch.Visibility = Visibility.Visible;

                            if ((GetField().MatchPosition + 1) < matches.Count) GetField().MatchPosition++;
                            else GetField().MatchPosition = 0;
                        }

                        else if (name == GetMain().Replace.Name)
                        {
                            GetMain().TextBox.CaretIndex = matches[0].Index;
                            GetMain().TextBox.Select(matches[0].Index, matches[0].Length);
                            GetMain().TextBox.SelectedText = GetMain().SearchReplace.Text;

                            GetMain().Info.Content = "Найдено совпадений: " + GetMethod().NumberFormat(matches.Count - 1);
                            GetMain().PositionMatch.Visibility = Visibility.Collapsed;
                        }

                        else if (name == GetMain().ReplaceAll.Name)
                        {
                            HideInfo();

                            if ((bool)GetMain().IsCountMatches.IsChecked)
                            {
                                MessageBox.Show(
                                    "Заменено элементов: " + matches.Count, "Notepad",
                                    MessageBoxButton.OK, MessageBoxImage.Information
                                );
                            }

                            GetMain().TextBox.Text = regex.Replace(GetMain().TextBox.Text, GetMain().SearchReplace.Text);
                        }
                    }

                    else throw new ArgumentOutOfRangeException();
                }

                else
                {
                    HideInfo();
                    GetMain().PanelFind.Visibility = Visibility.Collapsed;
                }
            }

            catch (ArgumentOutOfRangeException)
            {
                HideInfo();
                MessageBox.Show("Совпадений не найдено.", "Notepad", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            catch (ArgumentException)
            {
                HideInfo();
                MessageBox.Show("Неверное регулярное выражение!", "Notepad", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void GoToLine()
        {
            GoToLineWindow go = (GoToLineWindow)GetMethod().ShowDialog(new GoToLineWindow(GetMain().TextBox));

            if (go.Result == go.GetButton())
            {
                GetMain().TextBox.CaretIndex = GetMain().TextBox.GetCharacterIndexFromLineIndex(go.GetLine() - 1);
            }
        }

        public void StartScroll() => GetMain().TextBox.ScrollToHome();
        public void EndScroll() => GetMain().TextBox.ScrollToEnd();

        public void CurrentDate() => GetMain().TextBox.SelectedText = DateTime.Now.ToString("dd MMMM, dddd, HH:mm:ss, yyyy год");

        public void Undo()
        {
            if (GetMain().TextBox.CanUndo) GetMain().TextBox.Undo();
        }

        public void Redo()
        {
            if (GetMain().TextBox.CanRedo) GetMain().TextBox.Redo();
        }
    }
}