using AplicacionEficiencia.Modelos;
using Shell32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


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
                        if (!ruta.Contains("Uninstall") && !ruta.Contains("Desinstalar"))
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
    }
}
