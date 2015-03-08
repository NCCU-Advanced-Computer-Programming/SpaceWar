using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication7
{
    class Item
    {
        public int position;

        public enum TypeItem
        {
            Speed, Power, Amount
        }
        public TypeItem type;

        public Item(TypeItem _type,int pos)
        {
            type = _type;
            position = pos;
        }

        //當item被吃到時，呼叫此function，對傳入的player做出效果
        public void Item_get(Player _player){
            switch (type)
            {
                case TypeItem.Amount:
                    break;
                case TypeItem.Power:
                    break;
                case TypeItem.Speed:
                    break;
            }
        }
    }
}
