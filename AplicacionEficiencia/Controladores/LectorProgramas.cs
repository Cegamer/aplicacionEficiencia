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
        public List<Process> procesosActivosPC()
        {
            List<Process> procesos = new List<Process>();

            return procesos;
        }

        private List<Programa> GetAppsByUnistalKey(string key)
        {
            List<Programa> listAll = new List<Programa>();

            using (RegistryKey? rKey = Registry.LocalMachine.OpenSubKey(key))
            {
                if (rKey is null) return listAll;
                int i = 0;

                foreach (string skName in rKey.GetSubKeyNames())
                {
                    using (RegistryKey? sk = rKey.OpenSubKey(skName))
                    {
                        if (sk is null) continue;
                        string? displayName = sk.GetValue("DisplayName") as string;
                        string? installLocation = sk.GetValue("InstallLocation") as string;

                        if (!string.IsNullOrEmpty(displayName))
                        {
                            string executablePath = GetExecutablePath(installLocation!);

                            if (executablePath != "" && !executablePath.Contains("uninstall") && !executablePath.Contains("install"))
                                listAll.Add(new Programa(i++, displayName, executablePath));
                        }
                    }
                }
            }
            return listAll;
        }

        public void obtenerProgramasInstalados()
        {
            List<Programa> installedApps = new List<Programa>();

            //Leer los progrmas de 64bit y 32bit 
            installedApps.AddRange(GetAppsByUnistalKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"));
            installedApps.AddRange(GetAppsByUnistalKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"));
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
