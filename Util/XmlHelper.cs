using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SQLiteTool.Util
{
    class XmlHelper
    {
        private XDocument doc;
        private string filePath;

        public XmlHelper()
        {
            doc = new XDocument();
        }

        /// <summary>
        /// Open Xml File
        /// </summary>
        /// <param name="_filePath"></param>
        /// <returns></returns>
        public bool OpenXml(string _filePath)
        {
            try
            {
                filePath = _filePath;
                doc = XDocument.Load(_filePath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get Xml Document
        /// </summary>
        /// <returns></returns>
        public XDocument GetDocument()
        {
            return doc.Document;
        }

        /// <summary>
        /// Save XML File
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                doc.Save(filePath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Save XML Copy File
        /// </summary>
        /// <param name="copyFilePath"></param>
        /// <returns></returns>
        public bool Save(string copyFilePath)
        {
            try
            {
                doc.Save(copyFilePath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get Value By XML Node Name Full Path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetValueByNodeName(string path)
        {
            List<string> list = new List<string>();

            foreach (var item in doc.XPathSelectElements(path))
            {
                list.Add(item.Value);
            }

            return list;
        }
    }
}
