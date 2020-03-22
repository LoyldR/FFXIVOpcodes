﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FFXIVOpcodes
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.WriteLine("Exporting...");

            List<RegionSet> regions = new List<RegionSet>{
                new RegionSet { region = "Global" },
                new RegionSet { region = "CN" },
                new RegionSet { region = "KR" },
            };

            Type[][] enums = {
                new Type[] { typeof(Global.ServerZoneIpcType), typeof(Global.ClientZoneIpcType), typeof(Global.ServerChatIpcType), typeof(Global.ClientChatIpcType), typeof(Global.ServerLobbyIpcType), typeof(Global.ClientLobbyIpcType), },
                new Type[] { typeof(CN.ServerZoneIpcType), typeof(CN.ClientZoneIpcType), typeof(CN.ServerChatIpcType), typeof(CN.ClientChatIpcType), typeof(CN.ServerLobbyIpcType), typeof(CN.ClientLobbyIpcType), },
                new Type[] { typeof(KR.ServerZoneIpcType), typeof(KR.ClientZoneIpcType), typeof(KR.ServerChatIpcType), typeof(KR.ClientChatIpcType), typeof(KR.ServerLobbyIpcType), typeof(KR.ClientLobbyIpcType), },
            };

            string version = "5.21 hotfix";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    foreach (var ipcCommand in Enum.GetValues(enums[i][j]))
                    {
                        var command = new OutputEntry
                        {
                            name = ipcCommand.ToString(),
                            opcode = (ipcCommand as IConvertible).ToInt32(CultureInfo.CurrentCulture),
                            version = version,
                        };
                        regions[i].lists[enums[i][j].Name].Add(command);
                    }
                }
            }

            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "opcodes.json"), JsonConvert.SerializeObject(regions, Formatting.Indented));

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}