using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ObservableCollection<Toy> Toys { get; set; }






        public ObservableCollection<Toy> Toys
        {
            get { return (ObservableCollection<Toy> )GetValue(ToysProperty); }
            set { SetValue(ToysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Toys.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToysProperty =
            DependencyProperty.Register("Toys", typeof(ObservableCollection<Toy> ), typeof(MainWindow));




        public MainWindow()
        {
            

            BdConected.db.Toy.Load();
            Toys = BdConected.db.Toy.Local;

            InitializeComponent();
        }

        private void Refesh()
        {
            ObservableCollection<Toy> asdasd = Toys;

            if (FiltrToy.SelectedItem != null)
                switch ((FiltrToy.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        asdasd = BdConected.db.Toy.Local;
                        break;
                    case "2":
                        // Toys = new ObservableCollection<Toy> (BdConected.db.Toy.Local.Where(x => x.Cost > 5000));
                        asdasd = new ObservableCollection<Toy>
                            (
                                from toys in Toys
                                where toys.Cost > 5000
                                select toys
                            );
                        break;
                    case "3":
                        asdasd = new ObservableCollection<Toy>(Toys.Where(x => x.Cost < 5000));
                        break;
                    default:
                        break;
                }

            if (SortCb.SelectedItem != null)
                switch ((SortCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        asdasd = new ObservableCollection<Toy>(Toys.OrderBy(x => x.Cost));
                        break;
                    case "2":
                        asdasd = new ObservableCollection<Toy>(Toys.OrderByDescending(x => x.Cost));
                        break;
                    default:
                        break;
                }

            if (PoistBox.Text.Length > 0)
            {
                asdasd = new ObservableCollection<Toy>(Toys.Where(x => x.Name.ToLower().StartsWith(PoistBox.Text.ToLower())));
            }

            Toys = asdasd;
        }

        private void FiltrToy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refesh();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refesh();
        }

        private void TextchangePoiskBox(object sender, TextChangedEventArgs e)
        {
            Refesh();
        }
    }
}
