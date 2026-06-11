using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using FontFamily = System.Windows.Media.FontFamily;
using FormsMessageBox = System.Windows.Forms.MessageBox;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Search;

namespace WinFormsApp.GUI;

public partial class ScriptEditor : Form
{
    private readonly TextEditor editor;
    private string? _currentFilePath;
    private ExecutionEngine? _executionEngine;
    private CancellationTokenSource? _executionCts;
    private bool _isExecuting;
    private bool _abortButtonEnabled;
    private bool _isRecording;
    private readonly RecordingManager _recordingManager;

    private static readonly Dictionary<string, Color> HighlightKeywords = new()
    {
        ["mouse"] = Color.FromRgb(150, 220, 255),
        ["keyboard"] = Color.FromRgb(150, 220, 255),
        ["engine"] = Color.FromRgb(0, 220, 225),
        ["wait"] = Color.FromRgb(100, 225, 80),
        ["move"] = Color.FromRgb(50, 125, 225),
        ["moveto"] = Color.FromRgb(50, 125, 225),
        ["moveby"] = Color.FromRgb(50, 125, 225),
        ["scroll"] = Color.FromRgb(50, 125, 225),
        ["down"] = Color.FromRgb(10, 255, 200),
        ["hold"] = Color.FromRgb(10, 255, 200),
        ["up"] = Color.FromRgb(10, 255, 200),
        ["release"] = Color.FromRgb(10, 255, 200),
        ["press"] = Color.FromRgb(10, 255, 200),
        ["click"] = Color.FromRgb(10, 255, 200),
        ["type"] = Color.FromRgb(10, 255, 200),
        ["combo"] = Color.FromRgb(10, 255, 200),
        ["tap"] = Color.FromRgb(10, 255, 200),
        ["ifheld"] = Color.FromRgb(220, 115, 255),
        ["endif"] = Color.FromRgb(220, 115, 255),
        ["ifon"] = Color.FromRgb(130, 70, 255),
        ["ifoff"] = Color.FromRgb(130, 70, 255),
        ["repeat"] = Color.FromRgb(130, 70, 255),
        ["endrepeat"] = Color.FromRgb(130, 70, 255),
        ["setvar"] = Color.FromRgb(150, 220, 255),
        ["updatevar"] = Color.FromRgb(150, 220, 255),
        ["deletevar"] = Color.FromRgb(150, 220, 255),
        ["setfunction"] = Color.FromRgb(150, 220, 255),
        ["endfunction"] = Color.FromRgb(150, 220, 255),
        ["callfunction"] = Color.FromRgb(150, 220, 255),
        ["ctrl"] = Color.FromRgb(150, 220, 255),
        ["shift"] = Color.FromRgb(150, 220, 255),
        ["alt"] = Color.FromRgb(150, 220, 255),
        ["oskey"] = Color.FromRgb(150, 220, 255),
        ["altgr"] = Color.FromRgb(150, 220, 255),
        ["capslock"] = Color.FromRgb(150, 220, 255),
        ["numlock"] = Color.FromRgb(150, 220, 255),
        ["scrolllock"] = Color.FromRgb(150, 220, 255)
    };

    public ScriptEditor()
    {
        InitializeComponent();
        editor = CreateEditor();
        elementHost1.Child = editor;

        string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        var resourceUri = new Uri($"/{assemblyName};component/GUI/SearchTheme.xaml", UriKind.Relative);

        var resDict = (System.Windows.ResourceDictionary)System.Windows.Application.LoadComponent(resourceUri);
        this.editor.Resources.MergedDictionaries.Add(resDict);
        elementHost1.Child = editor;

        SearchPanel.Install(editor);
        editor.TextArea.TextView.LineTransformers.Add(new ScriptHighlightingColorizer(HighlightKeywords));
        editor.TextArea.TextView.BackgroundRenderers.Add(new CurrentLineBackgroundRenderer(editor));
        editor.TextArea.Caret.PositionChanged += (_, _) => editor.TextArea.TextView.InvalidateLayer(KnownLayer.Background);
        editor.TextChanged += (_, _) => editor.TextArea.TextView.Redraw();

        _recordingManager = new RecordingManager();

        // Wire up event handlers for menu items
        newScriptMenuItem.Click += NewScriptMenuItem_Click;
        openScriptMenuItem.Click += OpenScriptMenuItem_Click;
        saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
        saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
        executeToolStripMenuItem.Click += ExecuteToolStripMenuItem_Click;
        stopExecutionToolStripMenuItem.Click += StopExecutionToolStripMenuItem_Click;
        startRecordingMenuItem.Click += StartRecordingMenuItem_Click;
        stopRecordingMenuItem.Click += StopRecordingMenuItem_Click;
    }

    private static TextEditor CreateEditor()
    {
        var textEditor = new TextEditor
        {
            ShowLineNumbers = true,
            FontFamily = new FontFamily("Consolas"),
            FontSize = 13,
            Background = new SolidColorBrush(Color.FromRgb(25, 25, 25)),
            Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            Options =
            {
                EnableEmailHyperlinks = false,
                EnableHyperlinks = false,
                ConvertTabsToSpaces = false,
                IndentationSize = 4
            },
            WordWrap = false
        };

        // Custom scrollbar style - proper sizing
        var scrollBarStyle = new Style(typeof(System.Windows.Controls.Primitives.ScrollBar));
        scrollBarStyle.Setters.Add(new Setter(System.Windows.Controls.Control.BackgroundProperty, System.Windows.Media.Brushes.Transparent));
        scrollBarStyle.Setters.Add(new Setter(System.Windows.Controls.Control.BorderThicknessProperty, new Thickness(0)));
        scrollBarStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 14.0));
        scrollBarStyle.Setters.Add(new Setter(FrameworkElement.WidthProperty, double.NaN));
        scrollBarStyle.Setters.Add(new Setter(System.Windows.Controls.Control.PaddingProperty, new Thickness(0)));
        scrollBarStyle.Setters.Add(new Setter(System.Windows.Controls.Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(130, 130, 130))));
        textEditor.Resources.Add(typeof(System.Windows.Controls.Primitives.ScrollBar), scrollBarStyle);

        return textEditor;
    }

    private sealed class ScriptHighlightingColorizer : DocumentColorizingTransformer
    {
        private static readonly Brush CommentBrush = new SolidColorBrush(Color.FromRgb(100, 255, 140));
        private readonly Dictionary<string, Color> keywords;
        private readonly Regex commentRegex = new("#.*$", RegexOptions.Compiled | RegexOptions.Multiline);

        public ScriptHighlightingColorizer(Dictionary<string, Color> keywords)
        {
            this.keywords = keywords;
        }

        protected override void ColorizeLine(DocumentLine line)
        {
            string text = CurrentContext.Document.GetText(line.Offset, line.Length);

            foreach (var kvp in keywords)
            {
                foreach (Match match in Regex.Matches(text, $"\\b{Regex.Escape(kvp.Key)}\\b", RegexOptions.IgnoreCase))
                {
                    ChangeLinePart(line.Offset + match.Index, line.Offset + match.Index + match.Length, element =>
                    {
                        element.TextRunProperties.SetForegroundBrush(new SolidColorBrush(kvp.Value));
                    });
                }
            }

            foreach (Match commentMatch in commentRegex.Matches(text))
            {
                ChangeLinePart(line.Offset + commentMatch.Index, line.Offset + commentMatch.Index + commentMatch.Length, element =>
                {
                    element.TextRunProperties.SetForegroundBrush(CommentBrush);
                });
            }
        }
    }

    private sealed class CurrentLineBackgroundRenderer : IBackgroundRenderer
    {
        private readonly TextEditor editor;
        private readonly Brush backgroundBrush;

        public CurrentLineBackgroundRenderer(TextEditor editor)
        {
            this.editor = editor;
            backgroundBrush = new SolidColorBrush(Color.FromRgb(55, 95, 170)) { Opacity = 0.18 };
            backgroundBrush.Freeze();
        }

        public KnownLayer Layer => KnownLayer.Background;

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (editor.Document == null || !textView.VisualLinesValid)
            {
                return;
            }

            var caretOffset = editor.CaretOffset;
            if (caretOffset < 0 || caretOffset > editor.Document.TextLength)
            {
                return;
            }

            var line = editor.Document.GetLineByOffset(caretOffset);
            var visualLine = textView.GetVisualLine(line.LineNumber);
            if (visualLine == null)
            {
                return;
            }

            var y = visualLine.GetTextLineVisualYPosition(visualLine.TextLines[0], VisualYPosition.TextTop);
            var rect = new Rect(0, y, textView.RenderSize.Width, visualLine.Height);
            drawingContext.DrawRectangle(backgroundBrush, null, rect);
        }
    }

    private void FindToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        var searchPanel = SearchPanel.Install(editor);
        searchPanel.Open();
        searchPanel.Focus();
    }

    private void NewScriptMenuItem_Click(object? sender, EventArgs e)
    {
        editor.Text = string.Empty;
        _currentFilePath = null;
        UpdateTitle();
    }

    private async void StartRecordingMenuItem_Click(object? sender, EventArgs e)
    {
        await StartRecordingAsync();
    }

    private void StopRecordingMenuItem_Click(object? sender, EventArgs e)
    {
        StopRecording();
    }

    private void OpenScriptMenuItem_Click(object? sender, EventArgs e)
    {
        OpenMacroFile();
    }

    private void SaveToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        SaveScript();
    }

    private void SaveAsToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        SaveScriptAs();
    }

    private void ExecuteToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        ExecuteScript();
    }

    private void StopExecutionToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        StopScriptExecution();
    }

    private void OpenMacroFile()
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Open Macro File",
            Filter = "Macro Files|*.macro|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string content = File.ReadAllText(openFileDialog.FileName);
                editor.Text = content;
                _currentFilePath = openFileDialog.FileName;
                UpdateTitle();
            }
            catch (Exception ex)
            {
                FormsMessageBox.Show(
                    $"Error opening file: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void SaveScript()
    {
        if (string.IsNullOrEmpty(_currentFilePath))
        {
            SaveScriptAs();
            return;
        }

        try
        {
            File.WriteAllText(_currentFilePath, editor.Text);
        }
        catch (Exception ex)
        {
            FormsMessageBox.Show(
                $"Error saving file: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void SaveScriptAs()
    {
        using var saveFileDialog = new SaveFileDialog
        {
            Title = "Save Macro File",
            Filter = "Macro Files|*.macro|All Files|*.*",
            DefaultExt = "macro",
            AddExtension = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                // Ensure the file has .macro extension
                string filePath = saveFileDialog.FileName;
                if (!filePath.EndsWith(".macro", StringComparison.OrdinalIgnoreCase))
                {
                    filePath = Path.ChangeExtension(filePath, ".macro");
                }

                File.WriteAllText(filePath, editor.Text);
                _currentFilePath = filePath;
                UpdateTitle();
            }
            catch (Exception ex)
            {
                FormsMessageBox.Show(
                    $"Error saving file: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void ExecuteScript()
    {
        if (_isExecuting)
        {
            FormsMessageBox.Show(
                "A script is already running. Stop it first before executing another.",
                "Script Running",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(editor.Text))
        {
            FormsMessageBox.Show(
                "The script is empty. Please add some commands before executing.",
                "Empty Script",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            // Parse the script
            var parser = new ScriptParser();
            var parsedScript = parser.ParseScript(editor.Text);

            // Create the execution engine and input controller
            var inputController = new InputController();
            _executionEngine = new ExecutionEngine(inputController);
            _abortButtonEnabled = parsedScript.Options.AbortButton;

            _executionCts = new CancellationTokenSource();
            _isExecuting = true;
            UpdateExecutionState();

            // Run the script asynchronously
            _ = Task.Run(async () =>
            {
                try
                {
                    await _executionEngine.RunAsync(parsedScript);
                }
                catch (Exception ex)
                {
                    Invoke(() =>
                    {
                        FormsMessageBox.Show(
                            $"Script execution error: {ex.Message}",
                            "Execution Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    });
                }
                finally
                {
                    _isExecuting = false;
                    _abortButtonEnabled = false;
                    Invoke(UpdateExecutionState);
                }
            });
        }
        catch (Exception ex)
        {
            FormsMessageBox.Show(
                $"Error parsing script: {ex.Message}",
                "Parse Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Loads a macro file into the editor.
    /// </summary>
    /// <param name="filePath">The path to the .macro file to load.</param>
    public void LoadFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified macro file does not exist.", filePath);

        string content = File.ReadAllText(filePath);
        editor.Text = content;
        _currentFilePath = filePath;
        UpdateTitle();
    }

    /// <summary>
    /// Executes the current script from an external caller (e.g., main window or global hotkey).
    /// This method is thread-safe and can be called from any thread.
    /// </summary>
    public void ExecuteFromExternal()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(ExecuteScript));
        }
        else
        {
            ExecuteScript();
        }
    }

    public void AbortFromExternal()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(AbortFromExternal));
            return;
        }

        if (!_isExecuting || !_abortButtonEnabled)
            return;

        StopScriptExecution();
    }

    /// <summary>
    /// Starts recording from an external caller (e.g., main window or global hotkey F6).
    /// This method is thread-safe and can be called from any thread.
    /// </summary>
    public void StartRecordingFromExternal()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(async () => await StartRecordingAsync()));
            return;
        }

        _ = StartRecordingAsync();
    }

    /// <summary>
    /// Stops recording from an external caller (e.g., main window or global hotkey F7).
    /// This method is thread-safe and can be called from any thread.
    /// </summary>
    public void StopRecordingFromExternal()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(StopRecording));
            return;
        }

        StopRecording();
    }

    private void StopScriptExecution()
    {
        if (_executionEngine != null)
        {
            _executionEngine.Stop();
        }

        if (_executionCts != null && !_executionCts.IsCancellationRequested)
        {
            _executionCts.Cancel();
        }

        _isExecuting = false;
        UpdateExecutionState();
    }

    private void UpdateTitle()
    {
        if (string.IsNullOrEmpty(_currentFilePath))
        {
            Text = "Script Editor - Untitled";
        }
        else
        {
            Text = $"Script Editor - {Path.GetFileName(_currentFilePath)}";
        }
    }

    private async Task StartRecordingAsync()
    {
        if (_isRecording)
            return;

        startRecordingMenuItem.Enabled = false;
        stopRecordingMenuItem.Enabled = false;

        var countdownForm = CreateCountdownForm();
        countdownForm.Show();

        for (int seconds = 3; seconds >= 1; seconds--)
        {
            UpdateCountdownLabel(countdownForm, $"Starting in {seconds}...");
            await Task.Delay(1000);
        }

        UpdateCountdownLabel(countdownForm, "Recording started...");
        await Task.Delay(500);
        countdownForm.Close();

        try
        {
            _recordingManager.Start();
            _isRecording = true;
            startRecordingMenuItem.Enabled = false;
            stopRecordingMenuItem.Enabled = true;
        }
        catch (Exception ex)
        {
            startRecordingMenuItem.Enabled = true;
            FormsMessageBox.Show(
                $"Unable to start recording: {ex.Message}",
                "Recording Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private static void UpdateCountdownLabel(Form countdownForm, string text)
    {
        if (countdownForm.Controls.Count > 0 && countdownForm.Controls[0] is System.Windows.Forms.Label label)
        {
            label.Text = text;
            countdownForm.Refresh();
        }
    }

    private Form CreateCountdownForm()
    {
        var form = new Form
        {
            FormBorderStyle = FormBorderStyle.FixedToolWindow,
            StartPosition = FormStartPosition.CenterParent,
            Width = 320,
            Height = 140,
            Text = "Recording Countdown",
            TopMost = true
        };

        var label = new System.Windows.Forms.Label
        {
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
            Text = "Starting in 3..."
        };

        form.Controls.Add(label);
        return form;
    }

    private void StopRecording()
    {
        if (!_isRecording)
            return;

        var commands = _recordingManager.Stop();
        _isRecording = false;
        startRecordingMenuItem.Enabled = true;
        stopRecordingMenuItem.Enabled = false;

        if (commands.Count == 0)
        {
            FormsMessageBox.Show(
                "No input was captured during recording.",
                "Recording Finished",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        string scriptText = RecordingManager.FormatCommands(commands);
        if (!string.IsNullOrWhiteSpace(editor.Text))
        {
            editor.Text += Environment.NewLine;
        }

        editor.Text += scriptText;
        FormsMessageBox.Show(
            "Recording stopped and converted into macro commands.",
            "Recording Finished",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void UpdateExecutionState()
    {
        executeToolStripMenuItem.Enabled = !_isExecuting;
        stopExecutionToolStripMenuItem.Enabled = _isExecuting;

        if (_isExecuting)
        {
            executeToolStripMenuItem.Text = "Running...";
        }
        else
        {
            executeToolStripMenuItem.Text = "Execute";
        }
    }

    public MenuStrip EditorMenuStrip => menuStrip;

    private void CloseToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void UndoToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        if (editor.CanUndo)
        {
            editor.Undo();
        }
    }

    private void RedoToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        if (editor.CanRedo)
        {
            editor.Redo();
        }
    }
}
