
using System.Windows;

namespace ModelLaba2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var simulation = new AssemblyLineSimulation();
            simulation.Simulate(50); // Запуск моделирования с 100 изделиями
        }
    }
}