﻿using Microsoft.Win32;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace RegIO
{
    /// <summary>
    /// Read/write a registry key
    /// </summary>
    public class RegIO
    {
        private static bool _testing = false;

        public static void Main(string[] args)
        {
            var path = args[0];
            var key = args[1];
            var method = args[2];
            var value = args.Length == 4 ? args[3] : "";

            //Registry.

            if(_testing)
            { //..dont show console etc...
            }
        }

        public static void Test(string filename)
        {
            _testing = true;
            Main(new string[] {filename});
        }
    }
}
