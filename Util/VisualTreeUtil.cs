using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SQLiteTool.Util
{
    class VisualTreeUtil
    {
        public static DependencyObject VisualTreeUpwardSearch<M>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(M))
            {
                if (source is Visual || source is Visual3D)
                    source = VisualTreeHelper.GetParent(source);
                else
                    source = LogicalTreeHelper.GetParent(source);
            }
            return source;
        }
    }
}
