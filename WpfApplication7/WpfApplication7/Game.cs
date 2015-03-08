using System;
using System.Collections;
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
using System.Windows.Forms;


namespace WpfApplication7
{
    class Game
    {
        private MainWindow _window;

        //about map
        int _height = 13;  //高度格數(列數)
        int _width = 19;  //寬度格數(行數) 
        public bool GamePlay;
        private int _PlayerAmount;
        private Collider _collider;
        Square.Condition condition;//磚塊類別判定
        private Square[,] _squares;//宣告存取格子的陣列

        public Game(MainWindow window,int PlayerAmount)
        {
            _window = window;
            _PlayerAmount = PlayerAmount;
            
        }


        public void Start()
        {
            GamePlay = true;
            _collider = new Collider();
            Game_data.instance.myWindow = _window;
            Game_data.instance.load_pic();
            CreateMap();
            CreatePlayer();
            //建塔
            CreateTower(4, 2, 5, 0);
            CreateTower(4, 8, 5.5f, 0);
            CreateTower(14, 2, 6, 1);
            CreateTower(14,8, 0.1f, 1);
            //Game_data.instance._map[5, 5] = 1;

            //建主塔
            CreateMainTower(1, 5, 0);
            CreateMainTower(17, 5, 1);

            //建障礙物
            CreateObstacle(2, 3, Game_data.instance.star1);
            CreateObstacle(2, 7, Game_data.instance.star6);
            CreateObstacle(1, 1, Game_data.instance.star3);
            CreateObstacle(1, 9, Game_data.instance.star4);
            CreateObstacle(6, 0, Game_data.instance.star7);
            CreateObstacle(6, 10, Game_data.instance.star8);

            CreateObstacle(5, 5, Game_data.instance.star2);
            CreateObstacle(9, 5, Game_data.instance.star2);
            CreateObstacle(13, 5, Game_data.instance.star2);

            CreateObstacle(16, 3, Game_data.instance.star6);
            CreateObstacle(16, 7, Game_data.instance.star1);
            CreateObstacle(17, 1, Game_data.instance.star4);
            CreateObstacle(17, 9, Game_data.instance.star3);
            CreateObstacle(12, 0, Game_data.instance.star8);
            CreateObstacle(12, 10, Game_data.instance.star7);

            //timer
            Timer tm = new Timer();
            tm.Interval = 1;
            tm.Tick += new EventHandler(Update);
            tm.Start();
        }

        //timer的handler
        void Update(object sender, EventArgs e)
        {
            Game_data.instance.worldTime += 0.02f;

            _window.ugdBoard.Children.Clear();
            CreateMap();

            ////////////////////////////////////
            foreach (Player _player in Game_data.instance.Player_list)
            {
                _player.Update();
            }

            foreach (Bomb _bomb in Game_data.instance.Bomb_list)
            {
                _bomb.Update();
            }

            foreach (Tower _tower in Game_data.instance.Tower_list)
            {
                _tower.Update();
            }

            foreach(Laser _laser in Game_data.instance.Laser_list){
                _laser.Update();
            }

            foreach (MainTower _maintower in Game_data.instance.MainTower_list)
            {
                _maintower.Update();
            }

            _collider.Update();

            //主堡血量更新
            _window.life0.Content = "白方血量:" + Game_data.instance.MainTower_list[0].life.ToString();
            _window.life1.Content = "紅方血量:" + Game_data.instance.MainTower_list[1].life.ToString();
        }


        void CreateMap()
        {
           // _window.ugdBoard.Columns = _width;
           // _window.ugdBoard.Rows = _height;
           // _squares = new Square[_height, _width];
            //null=0,obstacle=1,brick=2,item=3
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    if (Game_data.instance._map[row, col] == 0)
                    {
                        condition = Square.Condition.Null;
                    }
                    else if (Game_data.instance._map[row, col] == 1)
                    {
                        condition = Square.Condition.Odstacle;
                    }
                    else if (Game_data.instance._map[row, col] == 2)
                    {
                        condition = Square.Condition.Brick;
                    }
                    else if (Game_data.instance._map[row, col] == 3)
                    {
                        condition = Square.Condition.Item_speed;
                    }
                    else if (Game_data.instance._map[row, col] == 4)
                    {
                        condition = Square.Condition.Item_power;
                    }
                    else if (Game_data.instance._map[row, col] == 5)
                    {
                        condition = Square.Condition.Item_amount;
                    }
                    else
                    {
                        condition = Square.Condition.Null;
                    }
                    Map_Update(row, col);
                }
            }
        }

        void Map_Update(int row, int col)
        {
                    Square s = new Square();
                    s.Width = 60;
                    s.Height = 60;
                    s.Stretch = Stretch.Fill;
                    if (condition == Square.Condition.Odstacle)
                    {
                        //判定磚塊狀態 Image為存取資料夾
                        s.Source = null;
                    }
                    else if (condition == Square.Condition.Null)
                    {
                        s.Source = null;
                    }
                    else if (condition == Square.Condition.Brick)
                    {
                        s.Source = Game_data.instance.Brick;
                    }
                    else if (condition == Square.Condition.Item_amount)
                    {
                        s.Source = Game_data.instance.ItemS;
                    }
                    else if (condition == Square.Condition.Item_power)
                    {
                        s.Source = Game_data.instance.ItemP;
                    }
                    else if (condition == Square.Condition.Item_speed)
                    {
                        s.Source = Game_data.instance.ItemA;
                    }
                    //s.ROW = row;
                   // s.COL = col;
                   // _squares[row, col] = s;
                    s.Margin = new Thickness((col-9)*120,(row-6)*120, 0, 0);
                    _window.ugdBoard.Children.Add(s);
                
            }
        
    

        void CreatePlayer()
        {
            for (int i = 0; i < _PlayerAmount; i++)
            {
                Player newPlayer = new Player(_window , i);
                newPlayer.Start();
                Game_data.instance.Player_list.Add(newPlayer);
                //請在這裡將Player秀到_window上
            }
        }


        void CreateTower(int posX, int posY, float startTime, int num)
        {
            Tower tower = new Tower(posX, posY, startTime, num);
            Game_data.instance.Tower_list.Add(tower);
        }

        void CreateMainTower(int posX, int posY, int num)
        {
            MainTower mainTower = new MainTower(posX, posY, num);
            Game_data.instance.MainTower_list.Add(mainTower);
        }

        void CreateObstacle(int posX, int posY,BitmapImage star)
        {
            int[] pos_end = Tools.instance.getPos_margin(posX, posY);
            //BitmapImage Laser_horizontal_right = new BitmapImage(new Uri(right_bottom, UriKind.Relative));
            Image image_right = new Image();
            image_right.Stretch = Stretch.Fill;
            image_right.Source = star;
            image_right.Width = 60;
            image_right.Height = 60;
            image_right.Margin = new Thickness(pos_end[0], pos_end[1], 0, 0);
            Game_data.instance.myWindow.mainGrid.Children.Add(image_right);
        }
    }
}
