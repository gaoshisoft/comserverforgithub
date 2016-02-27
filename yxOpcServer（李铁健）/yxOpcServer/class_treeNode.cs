using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace yxOpcServer
{
    public class class_treeNode : TreeNode
    {
        public DataTable dtList { get; set; }

        public class_treeNode()
        {
            dtList = new DataTable("Table_Tag");

            dtList.Columns.Add("bindName", System.Type.GetType("System.String"));
            dtList.Columns.Add("handle", System.Type.GetType("System.IntPtr"));
            dtList.Columns.Add("name", System.Type.GetType("System.String"));
            dtList.Columns.Add("sendName", System.Type.GetType("System.String"));
            dtList.Columns.Add("value", System.Type.GetType("System.String"));
            dtList.Columns.Add("vt", System.Type.GetType("System.String"));
            dtList.Columns.Add("status", System.Type.GetType("System.String"));

            //dtList.Columns["name"].ReadOnly = true;
            //dtList.Columns["vt"].ReadOnly = true;
            //dtList.Columns["status"].ReadOnly = true;
        }

        public bool AddItem(string bindName, string sendName, string name, string vt, string value, short Quality)
        {
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                if (dtList.Rows[i]["name"].ToString() == name)
                {
                    MessageBox.Show("同组内添加Item名称重复!");
                    return false;
                }
            }

            DataRow dr = dtList.NewRow();
            dr["handle"] = class_opcDll.CreateOpcItem(sendName, vt, value, Quality);
            dr["name"] = name;
            dr["bindName"] = bindName;
            dr["sendName"] = sendName;
            dr["value"] = value;
            dr["status"] = class_opcDll.shortTostring(Quality);
            dr["vt"] = vt;

            dtList.Rows.Add(dr);

            return true;
        }

        public bool UpdateItem(IntPtr handle, string vt, string value, short Quality)
        {
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                if (((IntPtr)dtList.Rows[i]["handle"]) == handle)
                {
                    // 修改界面数据
                    dtList.Rows[i]["vt"] = vt;
                    dtList.Rows[i]["value"] = value;
                    dtList.Rows[i]["status"] = class_opcDll.shortTostring(Quality); ;

                    // 修改OpcServer数据
                    class_opcDll.UpdateOpcItem(handle, vt, value, Quality);
                    return true;
                }
            }

            return false;
        }

        public bool UpdateItem(string name, string vt, string value, short Quality)
        {
            IntPtr uhandle;
            string uVt;
            string uValue;
            short uQuality;
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                if (dtList.Rows[i]["name"].ToString() == name)
                {
                    uhandle = (IntPtr)dtList.Rows[i]["handle"];
                    uVt = dtList.Rows[i]["vt"].ToString();
                    uValue = dtList.Rows[i]["value"].ToString();
                    uQuality = Convert.ToInt16(class_opcDll.stringToshort(dtList.Rows[i]["status"].ToString()));
                    // 修改界面数据
                    if (vt.Length <= 0)
                        vt = uVt;
                    if (value.Length <= 0)
                        value = uValue;
                    if (Quality < 0)
                        Quality = uQuality;

                    //dtList.Rows[i]["vt"] = vt;
                    dtList.Rows[i]["value"] = value;
                    //dtList.Rows[i]["status"] = class_opcDll.shortTostring(Quality); ;

                    // 修改OpcServer数据
                    class_opcDll.UpdateOpcItem(((IntPtr)dtList.Rows[i]["handle"]), vt, value, Quality);
                    return true;
                }
            }

            return false;
        }

        public bool UpdateSocketItem(string bindName, string vt, string value, short Quality)
        {
            IntPtr uhandle;
            string uVt;
            string uValue;
            short uQuality;
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                if (dtList.Rows[i]["bindName"].ToString() == bindName)
                {
                    uhandle = (IntPtr)dtList.Rows[i]["handle"];
                    uVt = dtList.Rows[i]["vt"].ToString();
                    uValue = dtList.Rows[i]["value"].ToString();
                    uQuality = Convert.ToInt16(class_opcDll.stringToshort(dtList.Rows[i]["status"].ToString()));
                    // 修改界面数据
                    if (vt.Length <= 0)
                        vt = uVt;
                    if (value.Length <= 0)
                        value = uValue;
                    if (Quality < 0)
                        Quality = uQuality;

                    //dtList.Rows[i]["vt"] = vt;
                    dtList.Rows[i]["value"] = value;
                    //dtList.Rows[i]["status"] = class_opcDll.shortTostring(Quality); ;

                    // 修改OpcServer数据
                    class_opcDll.UpdateOpcItem(((IntPtr)dtList.Rows[i]["handle"]), vt, value, Quality);
                    return true;
                }
            }

            return false;
        }

        public bool SeldateItem(IntPtr handle)
        {
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                if (((IntPtr)dtList.Rows[i]["handle"]) == handle)
                {
                    return true;
                }
            }

            return false;
        }

        public TreeNode MyRemove()
        {
            TreeNode next;
            IntPtr ptr; 
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
                ptr = (IntPtr)dtList.Rows[i]["handle"];
                if (ptr != null)
                    class_opcDll.RemoveOpcItem(ptr);
            }

            next = NextNode;

            dtList.Clear();
            Remove();
            return next;
        }
    }
}
