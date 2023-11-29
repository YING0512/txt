using System;
using System.Collections.Generic;
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
                fontFamilyComboBox.Items.Add(fontFamily.Source );
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
            MyDocumentViewer myDocumentViewer = new MyDocumentViewer();
            myDocumentViewer.Show();
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            boldButton.IsChecked = (property is FontWeight && (FontWeight)property == FontWeights.Bold);

            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            italicButton.IsChecked = (property is FontStyle && (FontStyle)property == FontStyles.Italic);

            property = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            underlineButton.IsChecked = (property != DependencyProperty.UnsetValue && property == TextDecorations.Underline);
        }

        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
