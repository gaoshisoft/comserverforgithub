using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace yxOpcServer
{
    class class_checkListBox : CheckedListBox
    {
        public ArrayList bindNameList = new ArrayList();

        public void AddItem(item it)
        {
            item item;
            for (int i = 0; i < bindNameList.Count; i++)
            {
                item = (item)bindNameList[i];
                if (item.bindName == it.bindName)
                    return;
            }
            bindNameList.Add(it);
            Items.Add(it.bindName);
        }

        public void UpdateItem(string bindName, string name, int index)
        {
            item item;
            for (int i = 0; i < bindNameList.Count; i++)
            {
                item = (item)bindNameList[i];
                if (item.bindName == bindName)
                {
                    item.name = name;
                    break;
                }
            }
            Items[index] = name;
        }

        public item GetItem(int index)
        {
            item it;
            for (int i = 0; i < bindNameList.Count; i++)
            {
                it = (item)bindNameList[i];
                if (Items[index].ToString() == it.name)
                    return it;
            }
            return null;
        }

        public void Clear()
        {
            bindNameList.Clear();
        }
    }
}
