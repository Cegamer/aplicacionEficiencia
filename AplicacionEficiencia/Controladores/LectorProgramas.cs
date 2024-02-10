using AplicacionEficiencia.Modelos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using AplicacionEficiencia.Vistas;

namespace AplicacionEficiencia.Controladores
{
    internal class LectorProgramas
    {
        public MainWindow MainWindow { get; set; }

        public List<Programa> programas;
        public List<Process> procesosActivosPC() {
            List<Process> procesos = new List<Process>();

            return procesos;
        }

        public void obtenerProgramasInstalados()
        {
            List<Programa> installedApps = new List<Programa>();
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                int i = 0;
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        string displayName = sk.GetValue("DisplayName") as string;
                        string installLocation = sk.GetValue("InstallLocation") as string;
                        if (!string.IsNullOrEmpty(displayName))
                        {
                            string executablePath = GetExecutablePath(installLocation);
                            if(executablePath != "" && !executablePath.Contains("uninstall") && !executablePath.Contains("install"))
                                installedApps.Add(new Programa(i,displayName, executablePath));
                        }
                    }
                }
            }
            this.programas = installedApps;
        }
        static string GetExecutablePath(string installLocation)
        {
            if (string.IsNullOrEmpty(installLocation) || !Directory.Exists(installLocation))
                return "";

            return Directory.GetFiles(installLocation, "*.exe", SearchOption.TopDirectoryOnly).FirstOrDefault() ?? "";
        }


        public List<Programa> GetProgramas()
        {
            return programas;
        }
    }
}
