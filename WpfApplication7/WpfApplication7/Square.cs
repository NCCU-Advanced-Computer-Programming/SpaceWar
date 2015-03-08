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
using System.Windows.Controls.Primitives;


namespace WpfApplication7
{
    class Square : Image
    {
        private int _row;
        private int _col;
        
        public enum Condition
        {
            Null, Odstacle, Brick, Item_speed, Item_power, Item_amount
        }

        public int ROW
        {
            get { return _row; }
            set { _row = value; }
        }

        public int COL
        {
            get { return _col; }
            set { _col = value; }
        }


        //預設背景，目前無用
        public Square()
            : base()
        {
            
        }
    }
}
