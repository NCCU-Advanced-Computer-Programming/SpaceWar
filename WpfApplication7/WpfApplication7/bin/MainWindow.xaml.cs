using System;
using System.Collections.Generic;
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

namespace WpfApplication7
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Game gm;  //遊戲類別物件
        GetKeyBoard getkey =new GetKeyBoard(); //取得按鍵按下
        public MainWindow()
        {
            InitializeComponent();
            //到時候playeramount的來源應該會是某個button
            player0.Source = null;
            player1.Source = null;

            //BitmapImage Tower = new BitmapImage(new Uri("Images/Brick.jpg", UriKind.Relative));
            //Image image = new Image();
            //image.Stretch = Stretch.Fill;
            //image.Source = Tower;
            //image.Width =2000;
            //image.Height = 690;
            //image.Margin = new Thickness(0,0, 0, 0);
            //Game_data.instance.myWindow.mainGrid.Children.Add(image);
            
             
            
            
        }


        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.R:
                case Key.D:
                case Key.F:
                case Key.G:
                case Key.Q:
                case Key.W:
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                case Key.P:
                case Key.O:
                    getkey.GetKeyDown(e.Key.ToString());
                    break;
                default:
                    //textbox1.Text += "a";
                    break;

            }
        } 

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.R:
                case Key.D:
                case Key.F:
                case Key.G:
                case Key.Q:
                case Key.W:
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                case Key.P:
                case Key.O:
                    getkey.GetKeyUp(e.Key.ToString());
                    break;
                default:
                    //textbox1.Text += "a";
                    break;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startBtn.Margin = new Thickness(6000,6000, 0, 0);
            startMenu.Source = null;

            gm = new Game(this, 2);
            gm.Start();
        }

       

    }
}
