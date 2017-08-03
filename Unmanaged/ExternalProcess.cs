using csgop.Functions;
using CSGOP.Data;
using CSGOP.Imported;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace CSGOP.Unmanaged {

    interface ExternalProcess {
        System.Diagnostics.Process Process { get; set; }

        bool AttachToProccess();
        void DeattachFromProccess();
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
        public IList<Action> cheats = new List<Action>();
        public IList<Thread> cheatsThreads = new List<Thread>();

        private static MemoryMappedFile mmf;

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

        public System.Diagnostics.Process Process {
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

        public bool AttachToProccess() {
            var processes = System.Diagnostics.Process.GetProcessesByName(processName);
            if (processes.Length > 0) {
                unsafe
                {
                    mmf = MemoryMappedFile.CreateOrOpen("Global\\usedPids", 4096, MemoryMappedFileAccess.ReadWrite);
                    MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, 4096);
                    byte[] buffer = new byte[4096];
                    mmvStream.Read(buffer, 0, 4096);
                    int[] usedPids = new int[1024];
                    Buffer.BlockCopy(buffer, 0, usedPids, 0, 4096);
                    var found = false;
                    foreach (var p in processes) {
                        found = false;
                        for (var i = 1; i <= usedPids[0]; ++i) {
                            if (p.Id == usedPids[i]) {
                                found = true;
                                break;
                            }
                        }
                        if (found == false) {
                            process = p;
                            break;
                        }
                    }
                    if (found == true) {
                        return false;
                    }
                    mmvStream.Close();
                }

            } else {
                return false;
            }
            return AttachToProccess(process);
        }

        public bool AttachToProccess(System.Diagnostics.Process process) {
            if (process != null) {
                pHandle = kernel.OpenProcess(0x10 | 0x20 | 0x08, false, process.Id);
            }
            var isAttached = pHandle == IntPtr.Zero ? false : true;
            if (isAttached) {
                unsafe
                {
                    mmf = MemoryMappedFile.OpenExisting("Global\\usedPids");
                    MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, 4096);
                    byte[] buffer = new byte[4096];
                    mmvStream.Read(buffer, 0, 4096);
                    int[] usedPids = new int[1024];
                    Buffer.BlockCopy(buffer, 0, usedPids, 0, 4096);
                    usedPids[0] = usedPids[0] + 1;
                    usedPids[usedPids[0]] = process.Id;
                    Buffer.BlockCopy(usedPids, 0, buffer, 0, 4096);
                    mmvStream.Seek(0, SeekOrigin.Begin);
                    mmvStream.Write(buffer, 0, 4096);
                    mmvStream.Close();
                }
                foreach (var cheat in cheats) {
                    var t = new Thread(() => cheat());
                    cheatsThreads.Add(t);
                    t.Start();
                }
                Console.WriteLine("Attached to " + Process.Id);
            }
            return isAttached;
        }

        public void DeattachFromProccess() {
            foreach (var cheat in cheatsThreads) {
                cheat.Abort();
            }
            cheatsThreads.Clear();
            Console.WriteLine("Deattached from " + Process.Id);
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

        public void AddCheat<CheatType>() where CheatType : CheatFunction {
            var cheat = (CheatFunction)Activator.CreateInstance(typeof(CheatType), client);
            cheats.Add(() => { cheat.Run(); });
        }

        public void AddCheat(Thread cheat) {
            throw new NotImplementedException();
        }
    }
}
