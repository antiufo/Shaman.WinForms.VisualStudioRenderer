using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shaman.WinForms
{
#if !COCOA
    public class VisualStudio2013ToolStripRenderer : ToolStripProfessionalRenderer
    {
        public VisualStudio2013ToolStripRenderer()
            : base(new VisualStudio2013ColorTable())
        {
            this.RoundedEdges = false;
        }

        public static readonly Color TextColor = GetColor(0x1B293E);

        private class VisualStudio2013ColorTable : ProfessionalColorTable
        {
            internal Color background = GetColor(0xCFD6E5);
            private Color highlighted = GetColor(0xFDF4BF);
            private Color highlightedBorder = GetColor(0xE5C365);
            private Color gripDark = GetColor(0x60728C);
            private Color menuBackground = GetColor(0xD6DBE9);
            private Color text = GetColor(0x1B293E);
            private Color overflow = GetColor(0xDCE0EC);
            private Color separatorDark = GetColor(0x8591A2);
            private Color separatorLight = GetColor(0xD6DBE9);
            private Color panelBackground = GetColor(0xEAF0FF);
            private Color imageMargin = GetColor(0xF2F4FE);
            public override Color ToolStripGradientBegin { get { return background; } }
            public override Color ToolStripGradientMiddle { get { return background; } }
            public override Color ToolStripGradientEnd { get { return background; } }

            public override Color ButtonSelectedHighlight { get { return highlighted; } }
            public override Color ButtonSelectedHighlightBorder { get { return highlightedBorder; } }

            public override Color ButtonPressedHighlight { get { return highlighted; } }
            public override Color ButtonPressedHighlightBorder { get { return highlightedBorder; } }

            public override Color ButtonCheckedHighlight { get { return highlighted; } }
            public override Color ButtonCheckedHighlightBorder { get { return highlightedBorder; } }

            public override Color ToolStripBorder { get { return background; } }

            public override Color CheckBackground { get { return highlighted; } }
            public override Color CheckPressedBackground { get { return highlighted; } }
            public override Color CheckSelectedBackground { get { return highlighted; } }

            public override Color ButtonCheckedGradientBegin { get { return highlighted; } }
            public override Color ButtonCheckedGradientEnd { get { return highlighted; } }
            public override Color ButtonCheckedGradientMiddle { get { return highlighted; } }
            public override Color ButtonPressedBorder { get { return highlightedBorder; } }
            public override Color ButtonPressedGradientBegin { get { return highlighted; } }
            public override Color ButtonPressedGradientEnd { get { return highlighted; } }
            public override Color ButtonPressedGradientMiddle { get { return highlighted; } }
            public override Color ButtonSelectedBorder { get { return highlightedBorder; } }
            public override Color ButtonSelectedGradientBegin { get { return highlighted; } }
            public override Color ButtonSelectedGradientEnd { get { return highlighted; } }
            public override Color ButtonSelectedGradientMiddle { get { return highlighted; } }

            public override Color GripDark { get { return Color.Transparent; } }
            public override Color GripLight { get { return gripDark; } }

            public override Color MenuBorder { get { return menuBackground; } }
            public override Color MenuItemBorder { get { return highlightedBorder; } }
            public override Color MenuItemPressedGradientBegin { get { return highlighted; } }
            public override Color MenuItemPressedGradientEnd { get { return highlighted; } }
            public override Color MenuItemPressedGradientMiddle { get { return highlighted; } }
            public override Color MenuItemSelected { get { return highlighted; } }
            public override Color MenuItemSelectedGradientBegin { get { return highlighted; } }
            public override Color MenuItemSelectedGradientEnd { get { return highlighted; } }
            public override Color MenuStripGradientBegin { get { return menuBackground; } }
            public override Color MenuStripGradientEnd { get { return menuBackground; } }
            public override Color OverflowButtonGradientBegin { get { return overflow; } }
            public override Color OverflowButtonGradientEnd { get { return overflow; } }
            public override Color OverflowButtonGradientMiddle { get { return overflow; } }
            public override Color SeparatorDark { get { return separatorDark; } }
            public override Color SeparatorLight { get { return separatorLight; } }
            public override Color StatusStripGradientBegin { get { return background; } }
            public override Color StatusStripGradientEnd { get { return background; } }
            public override Color ToolStripContentPanelGradientBegin { get { return panelBackground; } }
            public override Color ToolStripContentPanelGradientEnd { get { return panelBackground; } }
            public override Color ToolStripDropDownBackground { get { return panelBackground; } }
            public override Color ToolStripPanelGradientBegin { get { return panelBackground; } }
            public override Color ToolStripPanelGradientEnd { get { return panelBackground; } }
            public override Color RaftingContainerGradientBegin { get { return Color.Red; } }
            public override Color ImageMarginGradientBegin { get { return imageMargin; } }
            public override Color ImageMarginGradientEnd { get { return imageMargin; } }
            public override Color ImageMarginGradientMiddle { get { return imageMargin; } }
            public override Color ImageMarginRevealedGradientBegin { get { return Color.Green; } }


        }

        private static Color GetColor(uint code)
        {
            code |= 0xFF000000;
            return Color.FromArgb((int)code);
        }


        public static new void Initialize(ToolStrip toolstrip)
        {
            toolstrip.Renderer = new VisualStudio2013ToolStripRenderer();
            toolstrip.GripStyle = ToolStripGripStyle.Hidden;
            var status = toolstrip as StatusStrip;
            if (status != null) status.SizingGrip = false;
            foreach (ToolStripItem item in toolstrip.Items)
            {
                InitializeRecursive(item, TextColor);
            }
        }

        private static void InitializeRecursive(ToolStripItem item, Color foreColor)
        {
            item.ForeColor = foreColor;

            var toolStripDropDownItem = item as ToolStripDropDownItem;
            if (toolStripDropDownItem != null)
            {
                foreach (ToolStripItem subitem in toolStripDropDownItem.DropDownItems)
                {
                    InitializeRecursive(subitem, foreColor);
                }
            }

        }




        public static void SetStatusBarColor(StatusStrip statusStrip, VisualStudioStatusBarColor color)
        {
            var foreColor = Color.White;
            if (statusStrip.ForeColor != foreColor)
            {
                statusStrip.ForeColor = foreColor;
                foreach (ToolStripItem item in statusStrip.Items)
                {
                    InitializeRecursive(item, foreColor);
                }
            }
            var col = color == VisualStudioStatusBarColor.Purple ? GetColor(0x68217A) :
                color == VisualStudioStatusBarColor.Cyan ? GetColor(0x007ACC) :
                color == VisualStudioStatusBarColor.Orange ? GetColor(0xCA5100) :
                color == VisualStudioStatusBarColor.DarkBlue ? GetColor(0x293955) :
                Color.Black;
            ((VisualStudio2013ColorTable)(((VisualStudio2013ToolStripRenderer)statusStrip.Renderer).ColorTable)).background = col;
            statusStrip.Invalidate();
        }



    }
#endif
    public enum VisualStudioStatusBarColor
    {
        Cyan,
        Orange,
        Purple,
        DarkBlue
    }
}
