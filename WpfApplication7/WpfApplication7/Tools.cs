using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication7
{
    //遊戲中一些共用的function
    class Tools
    {
        public static Tools instance = new Tools();

        //放炸彈
        public int[] getPos(float x, float y)
        {
            int[] pos = getPos_map(x, y);
            return getPos_margin(pos[0],pos[1]);
        }

        //回傳於陣列中的位子
        public int[] getPos_map(float x, float y)
        {
            int posX = 0, posY = 0;
            float remainX = 0, remainY = 0;//判定站哪個格子
            remainX = x % 60;
            remainY = y % 60;
            //以半格作為判斷標準
            if (remainX > 30.0f)
            {
                posX = (int)(x / 60 + 1);//取xy所在格子座標
            }
            else
            {
                posX = (int)(x / 60);
            }
            if (remainY > 30.0f)
            {
                posY = (int)(y / 60 + 1);
            }
            else
            {
                posY = (int)(y / 60);
            }
           
            int[] pos = { posX, posY };
            return pos;//回傳陣列，要使用需指派陣列
        }

        //以陣列座標回傳margin位置
        public int[] getPos_margin(int x, int y)
        {
            int posX = 0, posY = 0;
            posX =  (x - 9) * 120-2;
            posY =  (y - 6) * 120+28;
            int[] pos_margin = { posX, posY };
            return pos_margin;
        }

        //碰撞偵測，有碰到會回傳true
        public bool AABBtest(float ax1, float ay1, float ax2, float ay2, float bx1, float by1, float bx2, float by2)
        {
            return
                 !(ax1 > bx2 || ax2 < bx1 ||
                ay1 > by2 || ay2 < by1); ;
        }
    }
}
