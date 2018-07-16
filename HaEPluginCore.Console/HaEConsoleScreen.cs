﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using Sandbox.Gui;
using VRage.Utils;
using VRageMath;
using HaEPluginCore;

namespace HaEPluginCore.Console
{
    public class HaEConsoleScreen : MyGuiScreenBase
    {
        private static HaEConsoleScreen _instance;

        private MyGuiControlTextbox _textBox;

        private float _screenscale;
        private Vector2 _margin;
        

        public override string GetFriendlyName() {  return "HaE Console";}

        public HaEConsoleScreen() : base(isTopMostScreen: true)
        {
            this._screenscale = MyGuiManager.GetHudSize().X / MyGuiManager.GetHudSize().Y / HaEConstants.screenScaleConstant;

            BackgroundColor = new Vector4(0, 0, 0, 0.5f);
            Size = new Vector2(_screenscale, 0.5f);
            RecreateControls(true);
        }

        public sealed override void RecreateControls(bool constructor)
        {
            Elements.Clear();
            Elements.Add(new MyGuiControlLabel
            {
                Text = "HaE Console",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_RIGHT_AND_VERTICAL_TOP,
                Position = MyGuiManager.ComputeFullscreenGuiCoordinate(MyGuiDrawAlignEnum.HORISONTAL_RIGHT_AND_VERTICAL_TOP)
            });

            Controls.Clear();
            _textBox = new MyGuiControlTextbox
            {
                BorderEnabled = false,
                Enabled = true,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                Position = new Vector2(0.5f)
            };
            Controls.Add(_textBox);

            var pistonBtn = new MyGuiControlImageButton
            {
                Name = "TorchButton",
                Text = "Torch",
                HighlightType = MyGuiControlHighlightType.WHEN_CURSOR_OVER,
                Visible = true,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP
            };
            Controls.Add(pistonBtn);
        }
    }
}
