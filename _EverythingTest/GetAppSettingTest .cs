﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace _EverythingTest
{
    [TestClass]
    public class SetAppSettingTest
    {
        [TestMethod]
        public void GetAppSetting_Test1()
        {
            var dir = @"C:\source\VSO\MLM\.shared\SharedAppSettings.Config";
            GetAppSetting.GetAppSetting.Main(new string[] { dir, "SolutionDirectory" });
        }
    }
}
