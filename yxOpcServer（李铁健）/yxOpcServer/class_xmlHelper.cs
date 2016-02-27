using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;

using System.Windows.Forms;
using System.Diagnostics;

namespace yxOpcServer
{
    /// <summary>
    /// XmlHelper 的摘要说明
    /// </summary>
    public class class_xmlHelper
    {
        public static string xmlPath { get; set; }
        public class_xmlHelper()
        {
        }

        // 加载xml文件
        public static bool ReadXml(string node, Frm_main main)
        {
            try
            {
                XmlAttributeCollection abconGroup;
                XmlAttributeCollection abconTag;
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode xmlGroup = doc.SelectSingleNode(node);
                foreach (XmlNode group in xmlGroup)
                {
                    // 添加

                    class_treeNode tn = null;
                    abconGroup = group.Attributes;
                    tn = main.AddGroup(abconGroup["name"].Value);

                    foreach (XmlNode tag in group)
                    {
                        abconTag = tag.Attributes;
                        tn.AddItem(abconTag["bindName"].Value.ToString(), abconTag["sendName"].Value.ToString(), abconTag["name"].Value.ToString(), abconTag["vt"].Value.ToString()
                            , abconTag["value"].Value.ToString(), Convert.ToInt16(abconTag["status"].Value));
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public static bool WriteXml(Frm_main main)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlElement xe = doc.CreateElement("", main.rootGroup.Text, "");
                doc.AppendChild(xe);

                class_treeNode group = (class_treeNode)main.rootGroup.FirstNode;
                while (group != null)
                {
                    // 保存Group
                    XmlNode xmlGroup = doc.CreateNode(XmlNodeType.Element, "group", "");
                    XmlAttribute xmlGroupName = doc.CreateAttribute("name");
                    xmlGroupName.Value = group.Text;
                    xmlGroup.Attributes.Append(xmlGroupName);

                    // 保存Tag
                    for (int i = 0; i < group.dtList.Rows.Count; i++)
                    {
                        XmlNode xmlTag = doc.CreateNode(XmlNodeType.Element, "tag", "");
                        XmlAttribute xmlTagName = doc.CreateAttribute("name");
                        xmlTagName.Value = group.dtList.Rows[i]["name"].ToString();
                        xmlTag.Attributes.Append(xmlTagName);

                        XmlAttribute xmlTagBindName = doc.CreateAttribute("bindName");
                        xmlTagBindName.Value = group.dtList.Rows[i]["bindName"].ToString();
                        xmlTag.Attributes.Append(xmlTagBindName);

                        XmlAttribute xmlTagSendName = doc.CreateAttribute("sendName");
                        xmlTagSendName.Value = group.dtList.Rows[i]["sendName"].ToString();
                        xmlTag.Attributes.Append(xmlTagSendName);

                        XmlAttribute xmlTagVt = doc.CreateAttribute("vt");
                        xmlTagVt.Value = group.dtList.Rows[i]["vt"].ToString();
                        xmlTag.Attributes.Append(xmlTagVt);

                        XmlAttribute xmlTagValue = doc.CreateAttribute("value");
                        xmlTagValue.Value = group.dtList.Rows[i]["value"].ToString();
                        xmlTag.Attributes.Append(xmlTagValue);

                        XmlAttribute xmlTagStatus = doc.CreateAttribute("status");
                        string status = group.dtList.Rows[i]["status"].ToString();
                        xmlTagStatus.Value = class_opcDll.stringToshort(status).ToString();
                        xmlTag.Attributes.Append(xmlTagStatus);

                        xmlGroup.AppendChild(xmlTag);
                    }

                    xe.AppendChild(xmlGroup);
                    group = (class_treeNode)group.NextNode;
                }
                doc.Save(xmlPath);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        ///// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        ///// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(xmlPath);
            }
            catch { }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        ///// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(xmlPath);
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        ///// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(xmlPath);
            }
            catch { }
        }
    }
}
