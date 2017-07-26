﻿using CSGOP.Core.Data;
using CSGOP.Imported;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace CSGOP.Unmanaged {

    abstract class ExternalProcess {
        public IList<Action> cheats = new List<Action>();
        public IList<Thread> cheatsThreads = new List<Thread>();

        public abstract System.Diagnostics.Process Process { get; set; }

        public abstract bool AttachToProccess();
        public abstract void SetClientBaseAddress();
    }

    abstract class ExternalProcess<BindingClass> : ExternalProcess {
        private readonly static Kernel32 kernel;
        private static IntPtr pHandle;
        private static IntPtr window;
        private static int width;
        private static int height;
        private static System.Diagnostics.Process process;
        private static string processName;

        public static IClient client;
        
        protected static IntPtr clientBaseAddress = new IntPtr(0);

        static ExternalProcess() {
            kernel = Kernel32.Instance;
        }

        public static IntPtr PHandle {
            get { return pHandle; }
            set { pHandle = value; }
        }

        public static IntPtr Window {
            get { return window; }
            set { window = value; }
        }

        public static int Width {
            get { return width; }
            set { width = value; }
        }

        public static int Height {
            get { return height; }
            set { height = value; }
        }

        public override System.Diagnostics.Process Process {
            get { return process; }
            set {
                process = value;
                ProcessName = process.ProcessName;
            }
        }

        public static System.Diagnostics.Process ProcessStatic {
            get { return process; }
            set {
                process = value;
                ProcessName = process.ProcessName;
            }
        }

        public static string ProcessName {
            get { return processName; }
            set { processName = value; }
        }

        public override bool AttachToProccess() {
            var processes = System.Diagnostics.Process.GetProcessesByName(processName);
            if (processes.Length > 0) {
                process = processes[0];
            } else {
                return false;
            }
            return AttachToProccess(process);
        }

        public bool AttachToProccess(System.Diagnostics.Process process) {
            if (process != null) {
                pHandle = kernel.OpenProcess(0x10 | 0x20 | 0x08, false, process.Id);
            }
            return pHandle == IntPtr.Zero ? false : true;
        }

        public static bool WindowHandle(string process) {
            var processes = System.Diagnostics.Process.GetProcessesByName(process);
            if (processes.Length > 0) {
                window = processes[0].MainWindowHandle;
            }
            return window == IntPtr.Zero ? false : true;
        }

        public static bool WindowRect() {
            Kernel32.RECT WindowSize = new Kernel32.RECT();
            if (kernel.GetClientRect(Games.CSGO.Process.Window, out WindowSize)) {
                width = WindowSize.Right - WindowSize.Left;
                height = WindowSize.Bottom - WindowSize.Top;
                return true;
            }
            return false;
        }


        public IClient Client {
            get {
                return client;
            }
        }

        public void AddCheat(Action cheat) {
            cheats.Add(cheat);
        }

        public void AddCheat(Thread cheat) {
            throw new NotImplementedException();
        }
    }
}
