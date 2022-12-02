using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace AvaloniaApplication1.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.AddHandler(KeyDownEvent, MouseDownHandler, handledEventsToo: true);
        }

        private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            switch (e.Key)
            {
                case Key.Return:
                    QuickSearch.OpenSearch(textBox?.Text ?? string.Empty);
                    break;
                case Key.Escape:
                    Hide();
                    break;
            }
        }
        
        private void MouseDownHandler(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key);
        }
    }
}