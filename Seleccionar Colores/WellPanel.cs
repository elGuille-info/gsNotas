//-----------------------------------------------------------------------------
// WellPanel un selector de colores                                 (25/Nov/20)
//
// Código (para C# y VB) de: Hannes DuPreez
// https://www.codeguru.com/csharp/.net/net_general/graphics/creating-a-custom-color-picker-in-.net-part-1-the-color-wells.html
// https://www.codeguru.com/csharp/.net/net_general/graphics/creating-a-custom-color-picker-in-.net-part-2-putting-it-all-together.html
//
// (c) Guillermo (elGuille) Som, 2020
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Seleccionar_Colores
{
    public partial class WellPanel
    {
        public enum Scheme
        {
            Web,
            System
        }

        public enum Order
        {
            Name,
            Hue,
            Saturation,
            Brightness,
            Distance,
            Unsorted
        }

        public enum OrderES
        {
            Nombre,
            Matiz,
            Saturación,
            Brillo,
            Distancia,
            Sin_Clasificar
        }

        private new void Layout()
        {
            int x = sborder.Width;
            int y = sborder.Height;

            foreach (ColorWellInfo c in arrWells)
            {
                c.colorPos = new Rectangle(x, y, scolorwell.Width, scolorwell.Height);
                x += scolorwell.Width;

                if (x + scolorwell.Width > ClientRectangle.Width)
                {
                    y += scolorwell.Height;
                    x = sborder.Width;
                }
            }
        }


        private ColorWellInfo WellFromPoint(int x, int y)
        {
            int w = ClientRectangle.Width;
            int h = ClientRectangle.Height;

            foreach (ColorWellInfo c in arrWells)
            {
                if (c.colorPos.Contains(x, y))
                {
                    return c;
                }
            }
            return null;
        }

        private ColorWellInfo WellFromColor(Color col)
        {
            foreach (ColorWellInfo c in arrWells)
            {
                if (c.Color == col)
                {
                    return c;
                }
            }
            return null;
        }

        private int IndexFromWell(ColorWellInfo col)
        {
            int num_colorWells = arrWells.Length;
            int index = -1;
            for (int i = 0; i < num_colorWells; i++)
            {
                if (arrWells[i] == col)
                {
                    index = i;
                }
            }
            return index;
        }

        [Browsable(true)]
        public event ColorChangedEventHandler ColorChanged;
        private void FireColorChanged()
        {
            if (null != cwColor)
            {
                OnColorChanged(new ColorChangedEventArgs(cwColor.Color));
            }
        }

        protected virtual void OnColorChanged(ColorChangedEventArgs e)
        {
            ColorChanged?.Invoke(this, e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (null != cwCurrent)
            {
                if (null != cwColor)
                {
                    Invalidate(cwColor.colorPos);
                }
                cwColor = cwCurrent;

                Invalidate(cwColor.colorPos);

                FireColorChanged();
            }
        }

        private void ChangeColor(ColorWellInfo newColor)
        {
            if (newColor != cwCurrent)
            {
                if (null != cwCurrent)
                {
                    Invalidate(cwCurrent.colorPos);
                }

                cwCurrent = newColor;
                if (null != cwCurrent)
                {
                    Invalidate(cwCurrent.colorPos);
                    tTip.SetToolTip(this, cwCurrent.Color.Name);
                }
                else
                {
                    tTip.SetToolTip(this, "");
                }
                Update();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!Enabled)
                return;

            Point mousePosition = new Point(e.X, e.Y);

            if (ClientRectangle.Contains(mousePosition) && (mousepos != mousePosition))
            {
                mousepos = mousePosition;

                ColorWellInfo newColor = WellFromPoint(e.X, e.Y);

                ChangeColor(newColor);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!Enabled)
                return;

            ColorWellInfo invalidColor = cwCurrent;
            cwCurrent = null;
            if (null != invalidColor)
            {
                Invalidate(invalidColor.colorPos);
                Update();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Refresh();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Refresh();
        }
        private void MoveColumn(int index, bool bNext)
        {
            int numColors = arrWells.Length;

            int r = index / columns;
            int c = index - (r * columns);

            int nextIndex = 0;
            if (bNext)
            {
                c++;
                if (c >= columns)
                {
                    c = 0;
                }

                nextIndex = r * columns + c;
                if (nextIndex >= numColors)
                {
                    nextIndex = r * columns;
                }
            }
            else
            {
                c--;

                if (c < 0)
                {
                    c = columns - 1;
                }

                nextIndex = r * columns + c;
                if (nextIndex >= numColors)
                {
                    nextIndex = numColors - 1;
                }
            }
            ChangeColor(arrWells[nextIndex]);
        }

        private void MoveRow(int index, bool bNext)
        {
            int numColors = arrWells.Length;

            int r = index / columns;
            int c = index - (r * columns);

            int nextIndex = 0;
            if (bNext)
            {
                r++;
                if (r >= rows)
                {
                    r = 0;
                }
                nextIndex = r * columns + c;
                if (nextIndex >= numColors)
                {
                    nextIndex = c;
                }
            }
            else
            {
                r--;
                if (r < 0)
                {
                    r = rows - 1;
                }
                nextIndex = r * columns + c;

                if (nextIndex >= numColors)
                {
                    nextIndex = (r - 1) * columns + c;
                }
            }
            ChangeColor(arrWells[nextIndex]);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!Enabled)
                return;

            int index = IndexFromWell((null != cwCurrent) ?
               (cwCurrent) : (cwColor));

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (null != cwCurrent)
                    {
                        ColorWellInfo oldColor = cwColor;

                        cwColor = cwCurrent;
                        FireColorChanged();

                        Invalidate(oldColor.colorPos);
                        Invalidate(cwCurrent.colorPos);

                        Update();
                    }
                    break;

                case Keys.Left:
                    if (index < 0)
                    {
                        ChangeColor(arrWells[arrWells.Length - 1]);
                    }
                    else
                    {
                        MoveColumn(index, false);
                    }
                    break;

                case Keys.Right:
                    if (index < 0 || index > (arrWells.Length - 1))
                    {
                        ChangeColor(arrWells[0]);
                    }
                    else
                    {
                        MoveColumn(index, true);
                    }
                    break;

                case Keys.Down:
                    if (index < 0)
                    {
                        ChangeColor(arrWells[0]);
                    }
                    else
                    {
                        MoveRow(index, true);
                    }
                    break;

                case Keys.Up:
                    if (index < 0)
                    {
                        ChangeColor(arrWells[arrWells.Length - 1]);
                    }
                    else
                    {
                        MoveRow(index, false);
                    }
                    break;
            }
        }
        protected virtual int GetPreferredWidth()
        {
            return Size.Width;
        }

        protected void AutoSizePanel()
        {
            if (ccolumns <= 0)
            {
                int preferredWidth = GetPreferredWidth();

                int w = preferredWidth - sborder.Width * 2;

                int remw = w % scolorwell.Width;
                columns = w / scolorwell.Width;

                rows = arrWells.Length / columns +
                         ((arrWells.Length % columns != 0) ? 1 : 0);
                int h = rows * scolorwell.Height + sborder.Height * 2;

                if (remw != 0 || h != Size.Height)
                {
                    w = preferredWidth - remw;
                    ClientSize = new Size(w, h);
                }
                Layout();
                Refresh();
            }
            else
            {
                int preferred = ccolumns;
                if (arrWells.Length < ccolumns)
                {
                    preferred = arrWells.Length;
                }
                columns = preferred;
                int w = preferred * scolorwell.Width +
                   sborder.Width * 2;

                rows = arrWells.Length / columns + ((arrWells.Length %
                   columns != 0) ? 1 : 0);
                int h = rows * scolorwell.Height + sborder.Height * 2;

                ClientSize = new Size(w, h);

                Layout();
                Refresh();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            AutoSizePanel();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (ColorWellInfo c in arrWells)
            {
                c.DrawColorWell(e.Graphics, Enabled, c == cwCurrent,
                   c == cwColor);
            }
            switch (borderstyle)
            {
                case BorderStyle.Fixed3D:
                    ControlPaint.DrawBorder3D(e.Graphics,
                       ClientRectangle, Border3DStyle.Sunken);
                    break;

                case BorderStyle.FixedSingle:
                    ControlPaint.DrawBorder3D(e.Graphics,
                       ClientRectangle, Border3DStyle.Flat);
                    break;
            }

            if (Focused && Enabled)
            {
                Rectangle r = ClientRectangle;

                r.Inflate(-sborder.Width + 1, -sborder.Height + 1);
                ControlPaint.DrawFocusRectangle(e.Graphics, r);
            }
            base.OnPaint(e);
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);

            if (colorSet == Scheme.System)
            {
                arrWells = ColorWellInfo.GetColorWells(colorSet,
                   sortorder);
                Layout();

                UpdatePickColor();

                FireColorChanged();

                Refresh();
            }
        }

        [Browsable(true), Category("Appearance")]
        public new BorderStyle BorderStyle
        {
            get
            {
                return borderstyle;
            }
            set
            {
                if (!Utilities.CheckValidEnumValue("BorderStyle", value, typeof(BorderStyle)))
                    value = 0;

                if (borderstyle != value)
                {
                    borderstyle = value;

                    UpdateBorderSize();

                    AutoSizePanel();
                }
            }
        }

        private void UpdateBorderSize()
        {
            Size bs = new Size();

            switch (borderstyle)
            {
                case BorderStyle.Fixed3D:
                    bs = SystemInformation.Border3DSize;
                    break;

                case BorderStyle.FixedSingle:
                    bs = SystemInformation.BorderSize;
                    break;

                case BorderStyle.None:
                    break;
            }
            bs.Width++;
            bs.Height++;

            sborder = bs;
        }

        [Browsable(true), Category("ColorPanel")]
        public Color Color
        {
            get
            {
                if (cwColor != null)
                {
                    return cwColor.Color;
                }
                else
                {
                    return dcolor;
                }
            }
            set
            {
                if (((cwColor != null) && (cwColor.Color
                   != value)) || (cwColor == null))
                {
                    UpdatePickColor(value);
                    Refresh();
                }
            }
        }

        public void ResetColor()
        {
            Color = dcolor;
        }

        private void UpdatePickColor()
        {
            if (cwColor != null)
            {
                UpdatePickColor(cwColor.Color);
            }
            else
            {
                UpdatePickColor(dcolor);
            }
        }

        private void UpdatePickColor(Color c)
        {
            cwColor = WellFromColor(c);
            if (null == cwColor)
            {
                cwColor = WellFromColor(dcolor);
            }
            if (null == cwColor)
            {
                cwColor = arrWells[0];
            }
        }

        [Browsable(true)]
        public Scheme ColorScheme
        {
            get
            {
                return colorSet;
            }
            set
            {
                if( ! Utilities.CheckValidEnumValue("ColorScheme", value, typeof(Scheme)) )
                    value = 0;

                if (value != colorSet)
                {
                    arrWells = ColorWellInfo.GetColorWells(value, sortorder);
                    colorSet = value;

                    UpdatePickColor();

                    FireColorChanged();

                    AutoSizePanel();
                }
            }
        }

        [Browsable(true)]
        public Size WellSize
        {
            get
            {
                return scolorwell;
            }
            set
            {
                if (value.Height > SystemInformation.Border3DSize
                      .Height * 2 + 2 &&
                   value.Width > SystemInformation.Border3DSize
                      .Width * 2 + 2)
                {
                    if (value != WellSize)
                    {
                        scolorwell = value;

                        AutoSizePanel();
                    }
                }
                else
                {
                    Size min = new Size(SystemInformation.Border3DSize
                       .Height * 2 + 2, SystemInformation.Border3DSize
                       .Width * 2 + 2);

                    string msg = string.Format("Too Small", min);

                    throw new ArgumentOutOfRangeException("WellSize", value, msg);
                }
            }
        }

        public void ResetWellSize()
        {
            WellSize = dwellsize;
        }

        [Browsable(true)]
        public Order SortOrder
        {
            get
            {
                return sortorder;
            }
            set
            {
                if( ! Utilities.CheckValidEnumValue("SortOrder", value, typeof(Order)) )
                    value = 0;

                if (value != sortorder)
                {
                    ColorWellInfo.SortWells(arrWells, value);
                    Layout();

                    sortorder = value;
                    Refresh();
                }
            }
        }

        [Browsable(true)]
        public int Columns
        {
            get
            {
                return ccolumns;
            }
            set
            {
                if (value > 0)
                {
                    if (value <= arrWells.Length)
                    {
                        ccolumns = value;
                    }
                    else
                    {
                        ccolumns = arrWells.Length;
                    }
                }
                else
                {
                    ccolumns = 0;
                }
                AutoSizePanel();
            }
        }

        public class ColorChangedEventArgs : EventArgs
        {
            private Color color;

            public ColorChangedEventArgs(Color color)
            {
                this.color = color;
            }

            public Color Color
            {
                get
                {
                    return color;
                }
            }
        }

        public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);
    }

    partial class WellPanel : UserControl
    {
        internal const Order dorder = Order.Hue;
        internal const Scheme dscheme = Scheme.Web;
        internal const BorderStyle dborder = BorderStyle.FixedSingle;
        internal const int dcol = 0;
        internal static readonly Size dwellsize = new Size(16, 16);
        internal static readonly Color dcolor = Color.Black;

        private ToolTip tTip = new ToolTip();

        private BorderStyle borderstyle = dborder;
        private Size sborder = new Size(1, 1);
        private Size scolorwell = dwellsize;
        private ColorWellInfo[] arrWells = null;
        private ColorWellInfo cwColor = null;
        private ColorWellInfo cwCurrent = null;
        private Scheme colorSet = dscheme;
        private Order sortorder = dorder;
        private int ccolumns = dcol;

        private int columns;
        private int rows;
        private Point mousepos;

        public WellPanel()
        {
            arrWells = ColorWellInfo.GetColorWells(colorSet, sortorder);

            //InitializeComponent();

            UpdateBorderSize();

            AutoSizePanel();
        }

        //// Esto no estaba definido, pero lo pongo como en ctrColorPanel
        //// Le añado que use el nombre para ordenar y los colores Web
        //private void InitializeComponent()
        //{
        //    Name = "WellPanel";
        //}


        private class ColorWellInfo
        {
            private int iUnsorted;
            private long distance;

            public readonly Color Color;
            public Rectangle colorPos;

            public ColorWellInfo(Color color, int unsortedindex)
            {
                Color = color;
                distance = color.R * color.R + color.B * color.B + color.G * color.G;
                iUnsorted = unsortedindex;
            }

            private class DistanceComparer : IComparer
            {
                public int Compare(object a, object b)
                {
                    ColorWellInfo _a = (ColorWellInfo)a;
                    ColorWellInfo _b = (ColorWellInfo)b;

                    return _a.distance.CompareTo(_b.distance);
                }
            }

            private class NameComparer : IComparer
            {
                public int Compare(object a, object b)
                {
                    ColorWellInfo _a = (ColorWellInfo)a;
                    ColorWellInfo _b = (ColorWellInfo)b;

                    return _a.Color.Name.CompareTo(_b.Color.Name);
                }
            }

            private class SaturationComparer : IComparer
            {
                public int Compare(object a, object b)
                {
                    ColorWellInfo _a = (ColorWellInfo)a;
                    ColorWellInfo _b = (ColorWellInfo)b;

                    return _a.Color.GetSaturation().CompareTo(_b.Color.GetSaturation());
                }
            }

            private class HueComparer : IComparer
            {
                public int Compare(object a, object b)
                {
                    ColorWellInfo _a = (ColorWellInfo)a;
                    ColorWellInfo _b = (ColorWellInfo)b;

                    return _a.Color.GetHue().CompareTo(_b.Color.GetHue());
                }
            }

            private class BrightnessComparer : IComparer
            {
                public int Compare(object a, object b)
                {
                    ColorWellInfo _a = (ColorWellInfo)a;
                    ColorWellInfo _b = (ColorWellInfo)b;
                    return _a.Color.GetBrightness().CompareTo(_b.Color.GetBrightness());
                }
            }

            private class UnsortedComparer : IComparer
            {
                public int Compare(object a, object b)
                {
                    ColorWellInfo _a = (ColorWellInfo)a;
                    ColorWellInfo _b = (ColorWellInfo)b;
                    return _a.iUnsorted.CompareTo(_b.iUnsorted);
                }
            }

            public static IComparer CompareColorDistance()
            {
                return new DistanceComparer();
            }

            public static IComparer CompareColorName()
            {
                return new NameComparer();
            }

            public static IComparer CompareColorSaturation()
            {
                return new SaturationComparer();
            }

            public static IComparer CompareColorHue()
            {
                return new HueComparer();
            }

            public static IComparer CompareColorBrightness()
            {
                return new BrightnessComparer();
            }

            public static IComparer CompareUnsorted()
            {
                return new UnsortedComparer();
            }

            public static ColorWellInfo[] GetColorWells(Scheme sscheme, Order order)
            {
                Array arrKNown = Enum.GetValues(typeof(KnownColor));

                int iColors = 0;

                switch (sscheme)
                {
                    case Scheme.Web:
                        foreach (KnownColor k in arrKNown)
                        {
                            Color c = Color.FromKnownColor(k);
                            if (!c.IsSystemColor && (c.A > 0))
                            {
                                iColors++;
                            }
                        }
                        break;

                    case Scheme.System:
                        foreach (KnownColor k in arrKNown)
                        {
                            Color c = Color.FromKnownColor(k);
                            if (c.IsSystemColor && (c.A > 0))
                            {
                                iColors++;
                            }
                        }
                        break;
                }

                ColorWellInfo[] wells = new ColorWellInfo[iColors];

                int i = 0;

                switch (sscheme)
                {
                    case Scheme.Web:
                        foreach (KnownColor k in arrKNown)
                        {
                            Color c = Color.FromKnownColor(k);
                            if (!c.IsSystemColor && (c.A > 0))
                            {
                                wells[i] = new ColorWellInfo(c, i);
                                i++;
                            }
                        }

                        break;

                    case Scheme.System:
                        foreach (KnownColor k in arrKNown)
                        {
                           Color c = Color.FromKnownColor(k);
                            if (c.IsSystemColor && (c.A > 0))
                            {
                                wells[i] = new ColorWellInfo(c, i);
                                i++;
                            }
                        }
                        break;
                }

                SortWells(wells, order);

                return wells;
            }

            public static void SortWells(ColorWellInfo[] Wells,
               Order SortOrder)
            {
                switch (SortOrder)
                {
                    case Order.Brightness:
                        Array.Sort(Wells, CompareColorBrightness());
                        break;

                    case Order.Distance:
                        Array.Sort(Wells, CompareColorDistance());
                        break;

                    case Order.Hue:
                        Array.Sort(Wells, CompareColorHue());
                        break;

                    case Order.Name:
                        Array.Sort(Wells, CompareColorName());
                        break;

                    case Order.Saturation:
                        Array.Sort(Wells, CompareColorSaturation());
                        break;

                    case Order.Unsorted:
                        Array.Sort(Wells, CompareUnsorted());
                        break;
                }
            }

            public void DrawColorWell(Graphics g, bool enabled, bool selected, bool pickColor)
            {
                if (!enabled)
                {
                    Rectangle r = colorPos;

                    r.Inflate(-SystemInformation.BorderSize.Width,
                       -SystemInformation.BorderSize.Height);
                    ControlPaint.DrawBorder3D(g, r,
                       Border3DStyle.Flat);

                    r.Inflate(-SystemInformation.BorderSize.Width,
                       -SystemInformation.BorderSize.Height);
                    g.FillRectangle(SystemBrushes.Control, r);
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color);
                    if (pickColor)
                    {
                        Rectangle r = colorPos;

                        ControlPaint.DrawBorder3D(g, r,
                           Border3DStyle.Sunken);

                        r.Inflate(-SystemInformation.Border3DSize.Width, 
                            -SystemInformation.Border3DSize.Height);

                        g.FillRectangle(br, r);
                    }
                    else
                    {
                        if (selected)
                        {
                            Rectangle r = colorPos;

                            ControlPaint.DrawBorder3D(g, r,
                               Border3DStyle.Raised);
                            r.Inflate(-SystemInformation.Border3DSize.Width, 
                                -SystemInformation.Border3DSize.Height);

                            g.FillRectangle(br, r);
                        }
                        else
                        {
                            Rectangle r = colorPos;
                            g.FillRectangle(SystemBrushes.Control, r);

                            r.Inflate(-SystemInformation.BorderSize
                               .Width, -SystemInformation
                               .BorderSize.Height);
                            ControlPaint.DrawBorder3D(g, r,
                               Border3DStyle.Flat);
                            r.Inflate(-SystemInformation.BorderSize
                               .Width, -SystemInformation.BorderSize
                               .Height);

                            g.FillRectangle(br, r);
                        }
                    }
                    br.Dispose();
                    br = null;
                }
            }
        }
    }
}
