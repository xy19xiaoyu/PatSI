using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BLL.UIHelper
{
    public class ChartPaletteHelper
    {
        public static List<msChartPalette> chartPalette = new List<msChartPalette>();
        static ChartPaletteHelper()
        {
            chartPalette.Add(new msChartPalette() { ShowName = "亮柔", Name = "BrightPastel", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "标准1", Name = "cs1", Type = "1", Colors = new Color[] { Color.FromArgb(82, 89, 107), Color.FromArgb(189, 32, 16), Color.FromArgb(231, 186, 16), Color.FromArgb(99, 150, 41), Color.FromArgb(156, 85, 173), Color.FromArgb(206, 195, 198) } });
            chartPalette.Add(new msChartPalette() { ShowName = "标准2", Name = "cs2", Type = "1", Colors = new Color[] { Color.FromArgb(179, 222, 105), Color.FromArgb(217, 217, 217), Color.FromArgb(188, 128, 189), Color.FromArgb(255, 237, 111), Color.FromArgb(204, 235, 197), Color.FromArgb(252, 205, 229) } });
            chartPalette.Add(new msChartPalette() { ShowName = "标准3", Name = "cs3", Type = "1", Colors = new Color[] { Color.FromArgb(144, 211, 199), Color.FromArgb(255, 255, 179), Color.FromArgb(251, 128, 114), Color.FromArgb(128, 177, 211), Color.FromArgb(253, 189, 98) } });
            chartPalette.Add(new msChartPalette() { ShowName = "标准4", Name = "cs4", Type = "1", Colors = new Color[] { Color.FromArgb(0, 174, 247), Color.FromArgb(255, 134, 24), Color.FromArgb(206, 219, 41), Color.FromArgb(0, 166, 82) } });
            chartPalette.Add(new msChartPalette() { ShowName = "标准5", Name = "cs5", Type = "1", Colors = new Color[] { Color.FromArgb(0, 81, 107), Color.FromArgb(90, 146, 181), Color.FromArgb(0, 166, 231) } });
            chartPalette.Add(new msChartPalette() { ShowName = "明亮", Name = "Bright", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "灰度", Name = "Grayscale", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "Excel", Name = "Excel", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "浅色", Name = "Light", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "柔和", Name = "Pastel", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "茶色", Name = "EarthTones", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "半透明", Name = "SemiTransparent", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "蓝紫", Name = "Berry", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "巧克力", Name = "Chocolate", Type = "0", Colors = null });
            chartPalette.Add(new msChartPalette() { ShowName = "热情", Name = "Fire", Type = "0", Colors = new Color[] { } });
            chartPalette.Add(new msChartPalette() { ShowName = "海绿", Name = "SeaGreen", Type = "0", Colors = null });

        }
        public static void IniChartPalette(ComboBox cmb)
        {
            cmb.DataSource = chartPalette;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.DisplayMember = "ShowName";
            cmb.ValueMember = "Name";
        }
    }
}
