using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using FontFamily = System.Windows.Media.FontFamily;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace WinFormsApp.GUI;

public partial class ScriptEditor : Form
{
    private readonly TextEditor editor;

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

        editor.TextArea.TextView.LineTransformers.Add(new ScriptHighlightingColorizer(HighlightKeywords));
        editor.TextArea.TextView.BackgroundRenderers.Add(new CurrentLineBackgroundRenderer(editor));
        editor.TextArea.Caret.PositionChanged += (_, _) => editor.TextArea.TextView.InvalidateLayer(KnownLayer.Background);
        editor.TextChanged += (_, _) => editor.TextArea.TextView.Redraw();
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

        textEditor.Resources.Add(typeof(System.Windows.Controls.Primitives.ScrollBar), new Style(typeof(System.Windows.Controls.Primitives.ScrollBar))
        {
            Setters =
            {
                new Setter(System.Windows.Controls.Control.BackgroundProperty, System.Windows.Media.Brushes.Transparent),
                new Setter(System.Windows.Controls.Control.BorderThicknessProperty, new Thickness(0)),
                new Setter(FrameworkElement.WidthProperty, 8.0),
                new Setter(FrameworkElement.HeightProperty, 8.0),
                new Setter(System.Windows.Controls.Control.PaddingProperty, new Thickness(0)),
                new Setter(System.Windows.Controls.Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(130, 130, 130)))
            }
        });

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
