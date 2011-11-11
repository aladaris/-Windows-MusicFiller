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
            CreateLibrary(this.textBox_RootLibPath.Text);
            this.progressBar_Library.MarqueeAnimationSpeed = 0;
            this.Dispose();
        }

        /*
         * Método que rellena una DB vacia con los datos de todos lo ficheros y directorios incluidos en "inPath"
         * mediante llamadas recursivas para cada subdirectorio.
         */
        private void CreateLibrary(String inPath)
        {
            if (Directory.Exists(inPath))
            {
                bool insertado = false; // Indica si se ha insertado algún elemento del directorio actual en la tabla "files"
                                        // Así se sabe si hay que almacenar también el directorio en la tabla "directories"

                this.button_CreateLib.Enabled = false;
                this.button_Exit.Enabled = false;
                this.button_RootLibPathBrowse.Enabled = false;
                this.textBox_RootLibPath.Enabled = false;
                // Reseteamos la biblioteca TODO: Opcion para habilitar esto => Implementar mejor globalDirID
                // TODO: Revisar, no funciona <--------------------------------------------------------------
                DBHandler.dSet.Tables["files"].Rows.Clear();
                DBHandler.dSet.Tables["directories"].Rows.Clear();
                // Añadir ficheros
                foreach (String fileFullPath in Directory.GetFiles(inPath))
                {
                    String[] splitted = fileFullPath.Split('\\');
                    String fileName = splitted[splitted.Length - 1];

                    if (fileName.EndsWith(".mp3")) // TODO: Para aceptar otros tipos de ficheros (mp3, ogg, wav ...) hacer un método para comprobar. IE: Bool isAudioFile(String f)
                    {
                        FileInfo fInf = new FileInfo(fileFullPath); // Necesario para obtener el tamaño del fichero
                        DataRow newFile = DBHandler.dSet.Tables["files"].NewRow();

                        newFile["fileName"] = fileName;
                        newFile["fileSize"] = fInf.Length / 1024; // Almacenamos el tamaño en KB
                        newFile["dirID"] = DBHandler.gobalDirID;
                        DBHandler.dSet.Tables["files"].Rows.Add(newFile);

                        if (!insertado)
                            insertado = true;
                    }
                    this.progressBar_Library.Refresh();
                }

                // Añadir directorio
                //if (insertado){
                    DataRow newDir = DBHandler.dSet.Tables["directories"].NewRow();

                    DBHandler.gobalDirID++; // Directorio insertado -> dirID++
                    newDir["dirPath"] = inPath;
                    DBHandler.dSet.Tables["directories"].Rows.Add(newDir);
                    try
                    {
                        DBHandler.dirDA.Update(DBHandler.dSet, "directories"); // Persistimos los cambios en ambas tablas
                        DBHandler.filesDA.Update(DBHandler.dSet, "files");     // (El orden es importante)
                    }
                    catch (System.Data.SQLite.SQLiteException ex)
                    {
                        MessageBox.Show("Error escribiendo en la Base de Datos\n" + ex.ToString());
                        this.Dispose(); // TODO: mejor metodo (recargar la info del dSet en caso de fallo al escribir en BD)
                    }
                //}

                // Llamada recursiva por cada directorio en el directorio actual
                foreach (String dirFullPath in Directory.GetDirectories(inPath))
                {
                    CreateLibrary(dirFullPath);
                }
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
