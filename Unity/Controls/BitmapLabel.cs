﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using UnityEngine;
using Color = System.Drawing.Color;

namespace Unity.Controls
{
    /// <summary>
    /// Less allocation, slower baking.
    /// Multiline not supported yet.
    /// </summary>
    public class BitmapLabel : Control
    {
        internal readonly BitmapText bmText;
        
        public float Scale
        {
            get { return bmText.Scale; }
            set { bmText.Scale = value; }
        }
        public bool ShowRects { get; set; }
        public override string Text
        {
            get { return bmText.Text; }
            set
            {
                bmText.Text = value;
                Refresh();
            }
        }

        public BitmapLabel(BitmapFont font)
        {
            if (font == null || font.Loaded == false)
                throw new NullReferenceException("font");

            bmText = new BitmapText(font);

            AutoSize = true;
            Size = new Size(120, 20);
        }
        
        public override void Refresh()
        {
            base.Refresh();

            bmText.Apply();
            if (AutoSize)
                Size = new Size(bmText.Width, bmText.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.uwfFillRectangle(BackColor, 0, 0, Width, Height);
            e.Graphics.uwfDrawImage(bmText, ForeColor, 0, 0, bmText.Width, bmText.Height);
        }
    }
}
