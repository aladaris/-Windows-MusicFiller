using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MusicFiller
{
    public partial class Form_Biblioteca : Form
    {
        public Form_Biblioteca()
        {
            InitializeComponent();
        }

        private void biblioteca_Load(object sender, EventArgs e)
        {
            // Inicializacion de elementos del form
            this.label_nFilesValue.Text = DBHandler.dSet.Tables["files"].Rows.Count.ToString();
            this.textBox_RootLibPath.Text = Properties.Settings.Default.LibraryPath;
        }

        private void button_RootLibPathBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog foldBrow = new FolderBrowserDialog();
            foldBrow.SelectedPath = textBox_RootLibPath.Text;
            if (foldBrow.ShowDialog() == DialogResult.OK)
                textBox_RootLibPath.Text = foldBrow.SelectedPath;
            // Guardamos los cambios
            Properties.Settings.Default.LibraryPath = textBox_RootLibPath.Text;
            Properties.Settings.Default.Save();
        }

        private void button_CreateLib_Click(object sender, EventArgs e)
        {
            this.progressBar_Library.MarqueeAnimationSpeed = 50;
            CreateLibrary(this.textBox_RootLibPath.Text, -1);
            this.progressBar_Library.MarqueeAnimationSpeed = 0;
            this.Dispose();
        }


       /*
        * Método que rellena una DB vacia con los datos de todos lo ficheros y directorios incluidos en "inPath"
        * mediante llamadas recursivas para cada subdirectorio.
        */
        // TODO: Al utilizar la variable global impedimos "actualizar la biblioteca", al menos de un modo sencillo.

        private void CreateLibrary(String inPath, int fatherID)
        {
            if (Directory.Exists(inPath))
            {
                this.button_CreateLib.Enabled = false;
                this.button_Exit.Enabled = false;
                this.button_RootLibPathBrowse.Enabled = false;
                this.textBox_RootLibPath.Enabled = false;
                DBHandler.dSet.Tables["files"].Rows.Clear();
                DBHandler.dSet.Tables["directories"].Rows.Clear();

                // Añadir ficheros
                foreach (String fileFullPath in Directory.GetFiles(inPath))
                {
                    String[] splitted = fileFullPath.Split('\\');
                    String fileName = splitted[splitted.Length - 1]; // Obtenemos el ultimo elemento, que es el nombre del archivo

                    if (fileName.EndsWith(".mp3")) // TODO: Para aceptar otros tipos de ficheros (mp3, ogg, wav ...) hacer un método para comprobar. IE: Bool isAudioFile(String f)
                    {
                        FileInfo fInf = new FileInfo(fileFullPath); // Necesario para obtener el tamaño del fichero
                        DataRow newFile = DBHandler.dSet.Tables["files"].NewRow();

                        newFile["fileName"] = fileName;
                        newFile["fileSize"] = fInf.Length / 1024; // Almacenamos el tamaño en KB
                        newFile["dirID"] = DBHandler.gobalDirID;
                        DBHandler.dSet.Tables["files"].Rows.Add(newFile);
                    }
                    this.progressBar_Library.Refresh();
                }

                // Añadir directorio
                DataRow newDir = DBHandler.dSet.Tables["directories"].NewRow();
                int newFatherID = DBHandler.gobalDirID; // ID de este directorio

                DBHandler.gobalDirID++; // Directorio insertado -> dirID++
                newDir["dirPath"] = inPath;
                newDir["fatherID"] = fatherID;

                String[] slicedPath = inPath.Split('\\');
                newDir["dirName"] = slicedPath[slicedPath.Length - 1]; // El ultimo elemento del path es el nombre del directorio

                DBHandler.dSet.Tables["directories"].Rows.Add(newDir);
                try
                {
                    DBHandler.dirDA.Update(DBHandler.dSet, "directories"); // Persistimos los cambios en ambas tablas
                    DBHandler.filesDA.Update(DBHandler.dSet, "files");     // (El orden es importante)
                    DBHandler.dSet.AcceptChanges();
                    // Llamada recursiva por cada directorio en el directorio actual
                    foreach (String dirFullPath in Directory.GetDirectories(inPath))
                    {
                        CreateLibrary(dirFullPath, newFatherID);
                    }
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    MessageBox.Show("Error escribiendo en la Base de Datos\n" + ex.ToString());
                    this.Dispose(); // TODO: mejor metodo (recargar la info del dSet en caso de fallo al escribir en BD)
                }
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
