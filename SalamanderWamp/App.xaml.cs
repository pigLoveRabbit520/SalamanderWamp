using SalamanderWamp.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace SalamanderWamp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
