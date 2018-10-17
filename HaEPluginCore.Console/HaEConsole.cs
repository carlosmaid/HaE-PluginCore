﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Plugins;

namespace HaEPluginCore.Console
{
    public class HaEConsole : IPlugin
    {
        private static HaEConsole _instance;
        public static HaEConsole Instance => _instance;

        private StringBuilder _displayScreen;
        public StringBuilder displayScreen => _displayScreen;

        private LinkedList<string> _commandHistory;
        private LinkedListNode<string> _position = null;

        public Dictionary<string, HaEConsoleCommand> commands;

        public void Init(object gameInstance)
        {
            _instance = this;

            HaEInputHandler.HaEKeyCombination enter = new HaEInputHandler.HaEKeyCombination(VRage.Input.MyKeys.C, VRage.Input.MyKeys.RightAlt, VRage.Input.MyKeys.None, HaEConstants.quarterSecTimeOut, HaEConsoleScreen.Show);
            HaEPluginCore.HaEInputHandler.AddCombination(enter);

            HaEInputHandler.HaEKeyCombination exit = new HaEInputHandler.HaEKeyCombination(VRage.Input.MyKeys.Escape, VRage.Input.MyKeys.None, VRage.Input.MyKeys.None, HaEConstants.quarterSecTimeOut, HaEConsoleScreen.Close);
            HaEPluginCore.HaEInputHandler.AddCombination(exit);

            _displayScreen = new StringBuilder();
            _commandHistory = new LinkedList<string>();
            commands = new Dictionary<string, HaEConsoleCommand>();

            HaEConsoleScreen.RegisterKeys();
            HaEConsoleDefaultCommands.RegisterCommands();
        }

        public void RegisterCommand(HaEConsoleCommand command)
        {
            commands.Add(command.Command, command);
        }

        public void ParseCommand(string command)
        {
            if (_position == null)
            {
                _commandHistory.AddLast(command);
            } else
            {
                _commandHistory.AddAfter(_position, command);
                _position = _position.Next;
            }

            _displayScreen.Append(HandleCommand(command)).AppendLine();
        }

        public StringBuilder HandleCommand(string command)
        {
            List<string> split = HaEConsoleUtils.SplitArgs(command);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(command);

            if (split.Count <= 0)
            {
                return sb.Append("Error, Empty!");
            }

            string key = split[0];
            HaEConsoleCommand consoleCommand;
            if (!commands.TryGetValue(key, out consoleCommand))
                return sb.Append($"Error, command {key} not found!");

            split.RemoveAt(0);
            
            return sb.Append(consoleCommand.Action.Invoke(split));
        }

        public void NextLine()
        {
            if (_position != null)
            {
                _position = _position.Next;
            }
        }

        public void PreviousLine()
        {
            if (_position == null)
            {
                _position = _commandHistory.Last;
                return;
            }
            if (_position != _commandHistory.First)
            {
                _position = _position.Previous;
            }
        }

        public void Clear()
        {
            _displayScreen.Clear();
        }

        public void Update()
        {

        }

        public void Dispose()
        {

        }
    }
}
