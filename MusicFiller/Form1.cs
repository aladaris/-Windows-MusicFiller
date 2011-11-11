﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MusicFiller
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button_OutPutDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog foldBrow = new FolderBrowserDialog();
            foldBrow.SelectedPath = textBox_OutPutDir.Text;
            DialogResult dResult = foldBrow.ShowDialog();
            if (dResult == DialogResult.OK)
            {
                textBox_OutPutDir.Text = foldBrow.SelectedPath;
                Properties.Settings.Default.OutPutPath = textBox_OutPutDir.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void button_Library_Click(object sender, EventArgs e)
        {
            Form_Biblioteca fLibrary = new Form_Biblioteca();
            fLibrary.ShowDialog(this);
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            // Inicializacion
            DBHandler.dSet = new System.Data.DataSet();
            DBHandler.dSet.DataSetName = "musicFiller";
            ((System.ComponentModel.ISupportInitialize)(DBHandler.dSet)).BeginInit();
            this.textBox_fillSpace.Text = Properties.Settings.Default.fillSpace.ToString();
            DBHandler.gobalDirID = 1;
            textBox_OutPutDir.Text = Properties.Settings.Default.OutPutPath;
            // Seccion de la BD
            try
            {
                DBHandler.sqlCon = new System.Data.SQLite.SQLiteConnection("data source = \"" + Properties.Settings.Default.DBPath + "\"");
                DBHandler.sqlCon.Open();
                // Carga de datos en el DataSet
                DBHandler.sqlDA = new System.Data.SQLite.SQLiteDataAdapter("SELECT * FROM directories ORDER BY dID", DBHandler.sqlCon);
                DBHandler.sqlDA.Fill(DBHandler.dSet, "directories");
                DBHandler.sqlDA = new System.Data.SQLite.SQLiteDataAdapter("SELECT * FROM files ORDER BY fID", DBHandler.sqlCon);
                DBHandler.sqlDA.Fill(DBHandler.dSet, "files");
                // Obtención de comandos de los DataAdapters
                DBHandler.dirDA = new System.Data.SQLite.SQLiteDataAdapter();
                DBHandler.dirDA.SelectCommand = new System.Data.SQLite.SQLiteCommand("SELECT * FROM directories", DBHandler.sqlCon);
                System.Data.SQLite.SQLiteCommandBuilder comBuild = new System.Data.SQLite.SQLiteCommandBuilder(DBHandler.dirDA);
                comBuild.GetInsertCommand();
                comBuild.GetDeleteCommand();

                DBHandler.filesDA = new System.Data.SQLite.SQLiteDataAdapter();
                DBHandler.filesDA.SelectCommand = new System.Data.SQLite.SQLiteCommand("SELECT * FROM files", DBHandler.sqlCon);
                comBuild = new System.Data.SQLite.SQLiteCommandBuilder(DBHandler.filesDA);
                comBuild.GetInsertCommand();
                comBuild.GetDeleteCommand();
            }
            catch (System.Data.SQLite.SQLiteException ex)
            {
                MessageBox.Show("Error accediendo a la Base de Datos\n" + ex.ToString());
            }
            // Creacion del arbol de directorios
            //this.treeView_Library.BeginUpdate();
            //this.treeView_Library.Nodes.Clear();
            //this.treeView_Library.Nodes.Add(new TreeNode("1"));
            //this.treeView_Library.Nodes[0].Nodes.Add("1.1");
            //this.treeView_Library.Nodes[0].Nodes.Add("1.2");
            //this.treeView_Library.Nodes[0].Nodes.Add("1.3");
            //this.treeView_Library.Nodes[0].Nodes.Add("1.4");
            //this.treeView_Library.Nodes.Add("2");
            //this.treeView_Library.EndUpdate();
            ((System.ComponentModel.ISupportInitialize)(DBHandler.dSet)).EndInit();

            //TreeView
            TreeNode rootNode = new TreeNode("J:\\");
            treeView_Library.BeginUpdate();
            fillTreeView("J:\\", rootNode);
            treeView_Library.Nodes.Add(rootNode);
            treeView_Library.EndUpdate();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            backgroundWorker_CopyProgress.RunWorkerAsync(); // Iniciamos el proceso de copia 
        }

        private void backgroundWorker_CopyProgress_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show(randGen.Next().ToString()); // DEBUG
            if ((DBHandler.dSet.Tables["directories"].Rows.Count > 0) && (DBHandler.dSet.Tables["files"].Rows.Count > 0))
            {
                System.Random randGen = new Random();
                int copiedSize = 0; // Tamaño de lo que se ha copiado ya
                int randDirID = 0; // Declarado fuera para evitar redeclaracion en el bucle
                int randFileID = 0; // IDEM UP
                //int ProgressPerc; // IDEM UP -- Porcentaje del proceso
                String fullPath, fileName; // IDEM UP
                int fSize;
                int nFallos = 0, maxFallos = 100; // Numero de fallos (fichero elegido y no copiado) seguidos y el maximo de estos para que el bucle se detenga
                int totalSize = System.Convert.ToInt32(this.textBox_fillSpace.Text);

                this.button_Exit.Enabled = false;
                this.button_Start.Enabled = false;
                this.button_OutPutDir.Enabled = false;
                this.button_Library.Enabled = false;
                this.textBox_fillSpace.Enabled = false;
                this.textBox_OutPutDir.Enabled = false;

                //MessageBox.Show("WTF?");       // DEBUG
                while (copiedSize <= totalSize)
                {
                    // Generamos los IDs aleatorios para cada tabla
                    randDirID = randGen.Next(1, DBHandler.dSet.Tables["directories"].Rows.Count + 1);
                    // randFileID empieza en 0 porque se utiliza como indice en la tabla del DataSet
                    // al contrario que ranDirID que se utiliza para seleccionar mediante el campo "ID".
                    randFileID = randGen.Next(0, DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString()).Length);
                    //MessageBox.Show("Random entre (" + "1" + ", " + DBHandler.dSet.Tables["directories"].Rows.Count.ToString() + ")"); // DEBUG
                    //MessageBox.Show("ranDirID = " + randDirID.ToString()); // DEBUG

                    // Obtenemos la informacion necesaria del fichero seleccionado
                    fileName = DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString())[randFileID]["fileName"].ToString();
                    fSize = Convert.ToInt32(DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString())[randFileID]["fileSize"]);

                    // Construimos la ruta completa hasta el fichero seleccionado
                    fullPath = DBHandler.dSet.Tables["directories"].Select("dID = " + randDirID.ToString())[0]["dirPath"].ToString();
                    fullPath += "\\" + fileName;

                    //MessageBox.Show("UFFFFFFFFFFF!");       // DEBUG
                    // Si existe el fichero a copiar
                    // y no existe un fichero con el mismo nombre en el directorio de destino
                    // y el fichero cabe en el espacio restante de totalSize (es decir totalSize - copiedSize)
                    // => Copiar el fichero seleccionado en el directorio de destino
                    if ((File.Exists(fullPath)) && (!File.Exists(this.textBox_OutPutDir.Text + "\\" + fileName)) && ((totalSize - copiedSize) >= (fSize / 1024)))
                    {
                        //MessageBox.Show("CHE!");       // DEBUG
                        File.Copy(fullPath, this.textBox_OutPutDir.Text + "\\" + fileName);
                        //MessageBox.Show("File.Copy("+fullPath+", "+this.textBox_OutPutDir.Text + "\\" + fileName+");");
                        nFallos = 0;
                        copiedSize += Convert.ToInt32(fSize / 1024);
                    }
                    else
                    {
                        nFallos++;
                        if (nFallos >= maxFallos)
                        {
                            MessageBox.Show("nFallos = " + nFallos.ToString());       // DEBUG
                            break; // Evitamos caer en un bucle infinito,
                            // como por ejemplo: El espacio total ocupado por la biblioteca es menor que el que se ha especificado (para ser rellenado).
                        }
                    }
                    //MessageBox.Show("ÑÑÑÑÑÑÑÑÑÑÑA!");       // DEBUG
                    int denominador = (int) (totalSize / 100); // Evitemos DIVIDIR POR CERO
                    if (denominador <= 0)
                        denominador = 1;
                    int ProgressPerc = (int)( copiedSize / denominador);
                    //MessageBox.Show("ÑEEEEEEEEEEEEE!");       // DEBUG
                    backgroundWorker_CopyProgress.ReportProgress(ProgressPerc);
                }
                //MessageBox.Show("PUG!");       // DEBUG
                backgroundWorker_CopyProgress.ReportProgress(100);
            }
        }

        private void backgroundWorker_CopyProgress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar_CopyProgress.Value = e.ProgressPercentage;
            this.progressBar_CopyProgress.Refresh();
            this.label_progressPercentage.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker_CopyProgress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("Trabajo finalizado");
            this.button_Exit.Enabled = true;
            this.button_Start.Enabled = true;
            this.button_OutPutDir.Enabled = true;
            this.button_Library.Enabled = true;
            this.textBox_fillSpace.Enabled = true;
            this.textBox_OutPutDir.Enabled = true;
            this.progressBar_CopyProgress.Value = 0;
            this.label_progressPercentage.Text = "0 %";
        }

        
        private void fillTreeView(String dir, TreeNode parent)
        {
            DirectoryInfo directorio = new DirectoryInfo(dir);

            //MessageBox.Show("dir = " + dir + ", parent = " + parent.Text + ", nDirs = " + directorio.GetDirectories().Length.ToString());

            foreach (DirectoryInfo dInf in directorio.GetDirectories())
            {
                //MessageBox.Show(dInf.FullName);

                //if (DBHandler.dSet.Tables["directories"].Select("dirPath = " + dInf.FullName).Length == 1)
                if (isDirInDB(dInf.FullName))
                {
                    //MessageBox.Show("Añadiendo nodo " + dInf.Name);

                    TreeNode newNode = new TreeNode(dInf.Name);
                    fillTreeView(dInf.FullName, newNode);
                    parent.Nodes.Add(newNode);
                }
            }
        }

        private bool isDirInDB(String dir)
        {
            foreach (DataRow d in DBHandler.dSet.Tables["directories"].Rows)
            {
                if (dir == d["dirPath"].ToString())
                    return true;
            }
            return false;
        }

    }

}