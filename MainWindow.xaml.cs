using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace DXSample
{
    public partial class MainWindow
    {
        const string LayoutFilePath = "Layout.xml";
        const string AppName = "TestApplication";

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        void OnInitialized(object sender, System.EventArgs e)
        {
            if (File.Exists(LayoutFilePath))
            {
                DXSerializer.SetSerializationID(this, nameof(ThemedWindow));
                DXSerializer.SetEnabled(this, true);
                DXSerializer.Deserialize(this, LayoutFilePath, AppName, new DXOptionsLayout());

                WindowState = WindowState.Normal;

                DispatcherTimer timer = new();
                timer.Interval = TimeSpan.FromMilliseconds(200);
                timer.Tick += (o, args) =>
                {
                    DXSerializer.Deserialize(this, LayoutFilePath, AppName, new DXOptionsLayout());
                    timer.Stop();
                };
                timer.Start();
            }
        }
        void OnClosing(object sender, CancelEventArgs e)
        {
            DXSerializer.Serialize(this, LayoutFilePath, AppName, new DXOptionsLayout());
        }

        /// <inheritdoc />
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
        }

        private void MainWindow_OnStateChanged(object sender, EventArgs e)
        {
        }
    }
}