using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLiteTool.Model;
using SQLiteTool.Util;

namespace SQLiteTool
{
    class MainWindowViewModel : NotifyPropertyBase
    {
        List<DatabaseTree> treeList;

        internal List<DatabaseTree> TreeList
        {
            get 
            { 
                return treeList; 
            }
            set 
            {
                treeList = value;
                RaiseChange("TreeList");
            }
        }
        
    }

    class DatabaseTree
    {
        public DatabaseTree Parent { get; set; }

        public List<DatabaseTree> Children { get; set; }

        public DatabaseItem Item { get; set; }


        public DatabaseTree(DatabaseItem item)
        {
            this.Item = item;
            this.Children = new List<DatabaseTree>();
        }
        public DatabaseTree() { }

        bool _isTreeSelected;
        public bool IsSelected
        {
            get
            {
                return _isTreeSelected;
            }
            set
            {
                _isTreeSelected = value;
                if (_isTreeSelected)
                {
                    SelectedTreeItem = this;
                }
                else
                    SelectedTreeItem = null;
            }
        }

        public DatabaseTree SelectedTreeItem { get; set; }


        public void CreateTreeWithChildre(DatabaseTree children, bool? isChecked)
        {
            this.Children.Add(children);
            children.Parent = this;
        }
    
    }
}
