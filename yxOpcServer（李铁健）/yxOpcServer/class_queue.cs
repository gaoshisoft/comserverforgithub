using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace yxOpcServer
{
    class item
    {
        public string bindName { get; set; }  // 绑定名称，用于接收数据
        public IntPtr handle { get; set; }
        public string name { get; set; }
        public string sendName { get; set; }
        public string value { get; set; }
        public string vt { get; set; }
        public short status { get; set; }
    }

    class class_queue : Queue
    {
        public void EnqueueItem(string value)
        {
            string strTmp;
            ArrayList al = new ArrayList();
            ArrayList alInfo = new ArrayList();

            strTmp = value.Substring(2, value.Length - 3);

            al.AddRange(strTmp.Split(','));

            for (int i = 0; i < al.Count; i++)
            {
                item it = new item();

                strTmp = al[i].ToString();
                alInfo.AddRange(strTmp.Split(':'));

                it.name = alInfo[0].ToString();
                it.sendName = "";
                it.value = alInfo[2].ToString();
                it.vt = class_opcDll.valueTypeChange(alInfo[1].ToString());
                it.bindName = alInfo[0].ToString();
                it.sendName = class_opcDll.shortTostring(class_opcDll.OPC_QUALITY_GOOD);

                // 解析字符串
                Enqueue(it);
                alInfo.Clear();
            }
        }
    }
}
