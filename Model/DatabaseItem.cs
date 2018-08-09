using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteTool.Model
{
    public class DatabaseItem
    {
        /// <summary>
        /// Reserved
        /// </summary>
        public string Icon { get; set; }     
        public string DisplayName { get; set; }
        public string Description { get; set; }       
        public string FilePath { get; set; }

        public bool OpenState { get; set; }

        public List<DatabaseItem> Children { get; set; }

        public DatabaseItem()
        {
            Children = new List<DatabaseItem>();
        }
    }
}
