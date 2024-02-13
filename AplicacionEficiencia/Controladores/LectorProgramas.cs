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
using System.Windows.Controls;
using System.IO;
using Shell32;


namespace AplicacionEficiencia.Controladores
{
    public static class LectorProgramas
    {
        public static MainWindow MainWindow { get; set; }

        public static List<Programa> programas;
        public static List<Process> procesosActivosPC()
        {
            List<Process> procesos = new List<Process>();

            return procesos;
        }

        public static List<Programa> GetProgramas()
        {
            return programas;
        }

        public static void obtenerProgramasInstalados()
        {
            List<Programa> installedApps = new List<Programa>();

            string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string programsPath = Path.Combine(startMenuPath, "Programs");

            installedApps = ObtenerProgramas(programsPath);

            programas = installedApps;
        }

        static List<Programa> ObtenerProgramas(string carpeta)
        {
            List<Programa> programas = new List<Programa>();
            int id = 1;

            // Obtener archivos .lnk en la carpeta especificada
            string[] archivosLnk = Directory.GetFiles(carpeta, "*.lnk");

            foreach (string archivo in archivosLnk)
            {
                try
                {
                    // Obtener la información del archivo .lnk
                    var shortcut = new FileInfo(archivo);
                    var shell = new Shell32.Shell();
                    var folder = shell.NameSpace(Path.GetDirectoryName(shortcut.FullName));
                    var folderItem = folder.ParseName(Path.GetFileName(shortcut.FullName));

                    string nombre = folder.GetDetailsOf(folderItem, 0); // Nombre del programa
                    string ruta = folder.GetDetailsOf(folderItem, 194); // Ruta del ejecutable

 
                    FolderItem folderIt = folder.ParseName(System.IO.Path.GetFileName(ruta));
                    if (folderIt != null)
                    {
                        Shell32.ShellLinkObject lnk = folderIt.GetLink;
                        Shell32.FolderItem target = lnk.Target;
                        ruta = target.Path;

                        Debug.WriteLine("Ruta: " + ruta);
                        if(!ruta.Contains("Uninstall") && !ruta.Contains("Desinstalar"))
                        programas.Add(new Programa(id++, nombre, ruta));
                    }
                    else Debug.WriteLine("Error al obtener ruta");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener programa: {ex.Message}");
                }
            }

            // Recursivamente obtener programas de las subcarpetas
            foreach (string subCarpeta in Directory.GetDirectories(carpeta))
                programas.AddRange(ObtenerProgramas(subCarpeta));
            

            return programas;
        }

        /*
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
*/




    }
}
