﻿using System;
using System.Collections.Generic;
using System.Reflection;
using SakuraCommandHandler.Parameter;

namespace SakuraCommandHandler
{
    public class ConsoleCommand : CommandAttribute
    {    
        public ConsoleCommand(Type type, CommandAttribute attr): base(attr.Name, attr.Description)
        {
            List<CommandParameter> parameters = new List<CommandParameter>();
            foreach (var property in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (CommandHandler.TryGetAttribute(property.GetCustomAttributes(), out ParameterAttribute pAttr))
                    parameters.Add(new CommandParameter(pAttr, property));
            }
            Parameters = parameters;
            Type = type;
        }

        public Type Type { get; private set; }
        public IReadOnlyList<CommandParameter> Parameters { get; private set; }
    }
}
