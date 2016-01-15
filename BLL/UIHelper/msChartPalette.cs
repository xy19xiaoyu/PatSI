using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BLL.UIHelper
{
    public class msChartPalette
    {
        private string showName;

        public string ShowName
        {
            get { return showName; }
            set { showName = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        Color[] colors;
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
