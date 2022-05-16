using Microsoft.Win32;
using RegExDebug.views;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RegExDebug.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Stopwatch _stopwatch = new Stopwatch();
        #region Binding Parameters

        private string _regex_content;
        private string _regex_select_content;
        private string _replace_content;
        private string _source_content;
        private string _result_content;
        private DataTable _resultCollection;
        private DataGridCellInfo _selectedMatch;
        private int _source_sel_start = 0;
        private int _source_sel_len = 0;
        private string _elapsedTime;
        private bool _is_replace_mode;
        private string _exception_tip;
        private bool _rof_IgnoreCase;
        private bool _rof_Multiline;
        private bool _rof_ExplicitCapture;
        private bool _rof_Compiled;
        private bool _rof_Singleline;
        private bool _rof_IgnorePatternWhitespace;
        private bool _rof_RightToLeft;
        private bool _rof_ECMAScript;
        private bool _rof_CultureInvariant;
        public string Regex_content
        {
            get => _regex_content;
            set
            {
                RaiseAndSetIfChanged(ref _regex_content, value, nameof(Regex_content));
                RaisePropertyChanged(nameof(Regex_line));
            }
        }
        public string Replace_content
        {
            get => _replace_content;
            set
            {
                RaiseAndSetIfChanged(ref _replace_content, value, nameof(Replace_content));
                RaisePropertyChanged(nameof(Replace_line));
            }
        }
        public string Source_content
        {
            get => _source_content;
            set
            {
                RaiseAndSetIfChanged(ref _source_content, value, nameof(Source_content));
                RaisePropertyChanged(nameof(Source_line));
            }
        }
        public string Result_content
        {
            get => _result_content;
            set
            {
                RaiseAndSetIfChanged(ref _result_content, value, nameof(Result_content));
                RaisePropertyChanged(nameof(Result_line));
            }
        }
        public string Result_line
        {
            get => GetLinesString(Result_content);
        }
        public string Source_line
        {
            get => GetLinesString(Source_content);
        }
        public string Replace_line
        {
            get => GetLinesString(Replace_content);
        }
        public string Regex_line
        {
            get => GetLinesString(Regex_content);
        }

        public bool Is_replace_mode
        {
            get => _is_replace_mode;
            set
            {
                RaiseAndSetIfChanged(ref _is_replace_mode, value, nameof(Is_replace_mode));
                RaisePropertyChanged(nameof(Is_match_mode));
            }
        }
        public bool Is_match_mode
        {
            get => !Is_replace_mode;
            set
            {
                RaiseAndSetIfChanged(ref _is_replace_mode, !value, nameof(Is_match_mode));
                RaisePropertyChanged(nameof(Is_replace_mode));
            }
        }

        public DataTable ResultCollection
        {
            get => _resultCollection;
            set => RaiseAndSetIfChanged(ref _resultCollection, value, nameof(ResultCollection));
        }

        public DataGridCellInfo SelectedMatch
        {
            get => _selectedMatch;
            set
            {
                RaiseAndSetIfChanged(ref _selectedMatch, value, nameof(SelectedMatch));
                if (_selectedMatch != null)
                {
                    var col = _selectedMatch.Column as DataGridBoundColumn;
                    if (col == null) return;
                    var element = new FrameworkElement() { DataContext = _selectedMatch.Item };
                    BindingOperations.SetBinding(element, FrameworkElement.TagProperty, col.Binding);
                    var mre = element.Tag as Group;
                    Source_sel_start = 0;
                    Source_sel_len = 0;
                    Source_sel_start = mre.Index;
                    Source_sel_len = mre.Length;
                }
            }

        }

        public string ElapsedTime
        {
            get => _elapsedTime;
            set => RaiseAndSetIfChanged(ref _elapsedTime, value, nameof(ElapsedTime));
        }

        public int Source_sel_start
        {
            get => _source_sel_start;
            set => RaiseAndSetIfChanged(ref _source_sel_start, value, nameof(Source_sel_start));
        }
        public int Source_sel_len
        {
            get => _source_sel_len;
            set => RaiseAndSetIfChanged(ref _source_sel_len, value, nameof(Source_sel_len));
        }
        public bool ROF_IgnoreCase
        {
            get => _rof_IgnoreCase;
            set => RaiseAndSetIfChanged(ref _rof_IgnoreCase, value, nameof(ROF_IgnoreCase));
        }
        public bool ROF_Multiline
        {
            get => _rof_Multiline;
            set => RaiseAndSetIfChanged(ref _rof_Multiline, value, nameof(ROF_Multiline));
        }
        public bool ROF_ExplicitCapture
        {
            get => _rof_ExplicitCapture;
            set => RaiseAndSetIfChanged(ref _rof_ExplicitCapture, value, nameof(ROF_ExplicitCapture));
        }
        public bool ROF_Compiled
        {
            get => _rof_Compiled;
            set => RaiseAndSetIfChanged(ref _rof_Compiled, value, nameof(ROF_Compiled));
        }
        public bool ROF_Singleline
        {
            get => _rof_Singleline;
            set => RaiseAndSetIfChanged(ref _rof_Singleline, value, nameof(ROF_Singleline));
        }
        public bool ROF_IgnorePatternWhitespace
        {
            get => _rof_IgnorePatternWhitespace;
            set => RaiseAndSetIfChanged(ref _rof_IgnorePatternWhitespace, value, nameof(ROF_IgnorePatternWhitespace));
        }
        public bool ROF_RightToLeft
        {
            get => _rof_RightToLeft;
            set => RaiseAndSetIfChanged(ref _rof_RightToLeft, value, nameof(ROF_RightToLeft));
        }
        public bool ROF_ECMAScript
        {
            get => _rof_ECMAScript;
            set => RaiseAndSetIfChanged(ref _rof_ECMAScript, value, nameof(ROF_ECMAScript));
        }
        public bool ROF_CultureInvariant
        {
            get => _rof_CultureInvariant;
            set => RaiseAndSetIfChanged(ref _rof_CultureInvariant, value, nameof(ROF_CultureInvariant));
        }
        public RegexOptions Ro_IgnoreCase
        {
            get => (ROF_IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
        }
        public RegexOptions Ro_Multiline
        {
            get => (ROF_Multiline ? RegexOptions.Multiline : RegexOptions.None);
        }
        public RegexOptions Ro_ExplicitCapture
        {
            get => (ROF_ExplicitCapture ? RegexOptions.ExplicitCapture : RegexOptions.None);
        }
        public RegexOptions Ro_Compiled
        {
            get => (ROF_Compiled ? RegexOptions.Compiled : RegexOptions.None);
        }
        public RegexOptions Ro_Singleline
        {
            get => (ROF_Singleline ? RegexOptions.Singleline : RegexOptions.None);
        }
        public RegexOptions Ro_IgnorePatternWhitespace
        {
            get => (ROF_IgnorePatternWhitespace ? RegexOptions.IgnorePatternWhitespace : RegexOptions.None);
        }
        public RegexOptions Ro_RightToLeft
        {
            get => (ROF_RightToLeft ? RegexOptions.RightToLeft : RegexOptions.None);
        }
        public RegexOptions Ro_ECMAScript
        {
            get => (ROF_ECMAScript ? RegexOptions.ECMAScript : RegexOptions.None);
        }
        public RegexOptions Ro_CultureInvariant
        {
            get => (ROF_CultureInvariant ? RegexOptions.CultureInvariant : RegexOptions.None);
        }
        public string Regex_select_content
        {
            get => _regex_select_content;
            set => RaiseAndSetIfChanged(ref _regex_select_content, value, nameof(Regex_select_content));
        }


        public string Exception_tip
        {
            get => _exception_tip;
            set => RaiseAndSetIfChanged(ref _exception_tip, value, nameof(Exception_tip));
        }
        #endregion
        #region Binding Commands
        public RelayCommand<object> ChangeLanguageCommand
        {
            get
            {
                if (_changeLanguageCommand == null)
                {
                    _changeLanguageCommand = new RelayCommand<object>(ChangeLanguageAction);
                }
                return _changeLanguageCommand;
            }
            set => _changeLanguageCommand = value;
        }

        public RelayCommand<object> DebugCommand
        {
            get
            {
                if (_debugCommand == null)
                {
                    _debugCommand = new RelayCommand<object>(DebugAction);
                }
                return _debugCommand;
            }
            set => _debugCommand = value;
        }

        public RelayCommand<object> AboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand = new RelayCommand<object>(AboutAction);
                }
                return _aboutCommand;
            }
            set => _aboutCommand = value;
        }

        public RelayCommand<object> ExportCsvCommand
        {
            get
            {
                if (_exportCsvCommand == null)
                {
                    _exportCsvCommand = new RelayCommand<object>(ExportCsvAction);
                }
                return _exportCsvCommand;
            }
            set => _exportCsvCommand = value;
        }

        public RelayCommand<object> CopyCommand
        {
            get
            {
                if (_copyCommand == null)
                {
                    _copyCommand = new RelayCommand<object>(CopyAction);
                }
                return _copyCommand;
            }
            set => _copyCommand = value;
        }



        private RelayCommand<object> _debugCommand;
        private RelayCommand<object> _changeLanguageCommand;
        private RelayCommand<object> _aboutCommand;
        private RelayCommand<object> _exportCsvCommand;
        private RelayCommand<object> _copyCommand;

        #endregion
        #region Private Methods
        private void CopyAction(object obj)
        {
            string result = string.Join(Environment.NewLine, _DataTable.Rows.OfType<DataRow>().Select(x => string.Join("\t", x.ItemArray)));
            Clipboard.SetText(result);
        }
        private void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        private void ExportCsvAction(object obj)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV|*.csv";
            if (sfd.ShowDialog() != true) return;
            string filename = sfd.FileName;
            ToCSV(_DataTable, filename);
        }
        private void AboutAction(object obj)
        {
            Win_About win_About = new Win_About();
            win_About.Owner = obj as Window;
            win_About.ShowDialog();
        }
        Regex _REGEX;
        MatchCollection _MATCHCOLLECTION;
        DataTable _DataTable = new DataTable();
        private void DebugAction(object obj)
        {
            if (string.IsNullOrEmpty(Regex_content)) { Exception_tip = "请输入正则表达式"; return; }
            if (string.IsNullOrEmpty(Source_content)) { Exception_tip = "请输入源文本内容"; return; }
            string _regex_content = string.Empty;
            if (!string.IsNullOrEmpty(Regex_select_content)) _regex_content = Regex_select_content;
            else _regex_content = Regex_content;
            Exception_tip = string.Empty;
            _stopwatch.Restart();
            ElapsedTime = "-1";
            try
            {
                _REGEX = new Regex(_regex_content, Ro_Compiled | Ro_CultureInvariant | Ro_ECMAScript | Ro_ExplicitCapture | Ro_IgnoreCase | Ro_IgnorePatternWhitespace |
                    Ro_Multiline | Ro_RightToLeft | Ro_Singleline);
                if (Is_match_mode)
                {
                    _MATCHCOLLECTION = _REGEX.Matches(Source_content);

                    _DataTable = new DataTable();
                    foreach (Match match in _MATCHCOLLECTION)
                    {
                        if (_DataTable.Columns.Count == 0)
                        {
                            for (int i = 0; i < match.Groups.Count; i++)
                            {
                                _DataTable.Columns.Add(i.ToString(), typeof(Group));
                            }
                        }
                        ObservableCollection<Group> mres = new ObservableCollection<Group>();
                        foreach (Group group in match.Groups)
                        {
                            mres.Add(group);
                        }
                        _DataTable.Rows.Add(mres.ToArray<Group>());
                    }
                    ResultCollection = _DataTable;
                }
                else
                {
                    if (string.IsNullOrEmpty(Replace_content)) { Exception_tip = "请输入替换内容"; return; }
                    Result_content = _REGEX.Replace(Source_content, Replace_content);
                }
            }
            catch (Exception ex)
            {
                Exception_tip = ex.Message;
            }
            finally
            {
                _stopwatch.Stop();
                ElapsedTime = _stopwatch.ElapsedMilliseconds.ToString();
            }
        }



        private void ChangeLanguageAction(object obj)
        {
            string langName = obj.ToString();
            ResourceDictionary langRd = null;
            try
            {
                langRd = Application.LoadComponent(new Uri(@"languages\" + langName + ".xaml", UriKind.Relative)) as ResourceDictionary;
            }
            catch (Exception)
            {
                throw;
            }
            if (langRd != null)
            {
                int resources_count = App.Current.Resources.MergedDictionaries.Count;
                App.Current.Resources.MergedDictionaries.RemoveAt(resources_count - 1);
                App.Current.Resources.MergedDictionaries.Add(langRd);
            }
        }

        private string GetLinesString(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return "1";
            }
            else
            {
                int _len = CountLines(content);
                string _result = string.Empty;
                for (int i = 1; i <= _len; i++)
                {
                    _result += i + "\r\n";
                }
                return _result;
            }
        }
        private static int CountLines(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            int index = -1;
            int count = 0;
            while (-1 != (index = str.IndexOf(Environment.NewLine, index + 1)))
                count++;

            return count + 1;
        }
        #endregion
    }
}
