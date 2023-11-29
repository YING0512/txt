using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace txt
{
    /// <summary>
    /// MyDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class MyDocumentViewer : Window
    {
        Color fontColor = Colors.Black;
        public MyDocumentViewer()
        {
            InitializeComponent();
            fontColorPicker.SelectedColor = fontColor;
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                fontFamilyComboBox.Items.Add(fontFamily);
            }

            fontFamilyComboBox.SelectedIndex = 8;

            fontSizeComboBox.ItemsSource = new List<Double>() { 8, 9, 10, 12, 20, 24, 32, 40, 50, 60, 80, 100 };
            fontSizeComboBox.SelectedIndex = 3;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyDocumentViewer myDocumentViewer = new MyDocumentViewer();
            myDocumentViewer.Show();
        }
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Rich Text Format檔案|*.rtf|所有檔案|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    range.Load(fileStream, DataFormats.Rtf);
                }
            }
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Rich Text Format檔案|*.rtf|所有檔案|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Create))
                {
                    range.Save(fileStream, DataFormats.Rtf);
                }
            }
        }

        //工具列即時變更
        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //粗體
            object property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            boldButton.IsChecked = (property is FontWeight && (FontWeight)property == FontWeights.Bold);

            //斜體
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            italicButton.IsChecked = (property is FontStyle && (FontStyle)property == FontStyles.Italic);

            //底線
            property = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            underlineButton.IsChecked = (property != DependencyProperty.UnsetValue && property == TextDecorations.Underline);

            //字體
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            fontFamilyComboBox.SelectedItem = property;

            //文字大小
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            fontSizeComboBox.SelectedItem = property;

            //顏色
            SolidColorBrush? foregroundProperty = rtbEditor.Selection.GetPropertyValue(TextElement.ForegroundProperty) as SolidColorBrush ;
            if( foregroundProperty != null )
            {
                fontColorPicker.SelectedColor = foregroundProperty.Color;
            }
        }

        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)e.NewValue;
            SolidColorBrush fontBrush = new SolidColorBrush(fontColor);
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontBrush);
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(fontFamilyComboBox.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty,fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSizeComboBox.SelectedItem);
            }
        }

        private void rtbEditor_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
