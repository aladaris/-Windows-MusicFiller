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
                //DBHandler.sqlDA = new System.Data.SQLite.SQLiteDataAdapter("SELECT * FROM directories ORDER BY dID", DBHandler.sqlCon);
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
            ((System.ComponentModel.ISupportInitialize)(DBHandler.dSet)).EndInit();

            this.rootNode = new TreeNode("Biblioteca");
            treeView_Library.TopNode = rootNode;
            this.nDir = 0;
            backgroundWorker_treeFiller.RunWorkerAsync();
            // TODO: Dar opcion a cancelar la carga del TreeView, lo que implicaria detener
            // 'backgroundWorker_treeFiller', dejar de mostrar la progressBar, el texto y el TreeView
            // lo que a su vez implica encoger el alto de la ventana para rellenar el hueco dejado por el treeView.
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            backgroundWorker_CopyProgress.RunWorkerAsync(); // Iniciamos el proceso de copia 
        }

        private void backgroundWorker_CopyProgress_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((DBHandler.dSet.Tables["directories"].Rows.Count > 0) && (DBHandler.dSet.Tables["files"].Rows.Count > 0) /*&& (checkedCount(rootNode) == 0)*/)
            {
                //MessageBox.Show("ENTRO!"); // DEBUG
                System.Random randGen = new Random();
                int copiedSize = 0; // Tamaño de lo que se ha copiado ya
                int nFallos = 0, maxFallos = 100; // Numero de fallos (fichero elegido y no copiado) seguidos y el maximo de estos para que el bucle se detenga
                int totalSize = System.Convert.ToInt32(this.textBox_fillSpace.Text) * 1024; // Tamaño a rellenar en KBytes

                if (totalSize > 0)
                {
                    this.button_Exit.Enabled = false;
                    this.button_Start.Enabled = false;
                    this.button_OutPutDir.Enabled = false;
                    this.button_Library.Enabled = false;
                    this.textBox_fillSpace.Enabled = false;
                    this.textBox_OutPutDir.Enabled = false;
                }

                int randDirID, randFile;
                String DBfileFullPath, outFileFullPath;
                while ((copiedSize < totalSize)&&(nFallos <= maxFallos))
                {
                    // Generamos los IDs aleatorios para cada tabla
                    randDirID = randGen.Next(1, DBHandler.dSet.Tables["directories"].Rows.Count);


                    DataRow dir = DBHandler.dSet.Tables["directories"].Select("dID = " + randDirID.ToString())[0];

                    DataRow[] files = DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString());
                    if (files.Length > 0)
                    {
                        randFile = randGen.Next(0, files.Length);

                        /*
                        // Nos aseguramos de no elegir el mismo fichero
                        int[] copiedFilesPos; // Posiciones dentro del vector 'files' de los ficheros copiados
                        int filePickFails = 0;
                        int maxFilePickFails = files.Length + (files.Length / 2);
                    
                    
                        while (
                        {
                            if (filePickFails > files.Length)
                                break;
                            randFile = randGen.Next(0, files.Length);
                            filePickFails++;
                        }
                        */
                        //fullPath = DBHandler.dSet.Tables["directories"].Select("dID = " + randDirID.ToString())[0]["dirPath"] + "\\" + files[randFile]["fileName"];

                        bool marcado_ = isCheckedTreeNode(treeView_Library.TopNode, dir["dirName"].ToString());
                        //MessageBox.Show("isChecked " + dir["dirName"].ToString() + "?: " + marcado_.ToString());

                        DBfileFullPath = DBHandler.dSet.Tables["directories"].Select("dID = " + randDirID.ToString())[0]["dirPath"] + "\\" + files[randFile]["fileName"];
                        outFileFullPath = this.textBox_OutPutDir.Text + "\\" + files[randFile]["fileName"].ToString();
                        //MessageBox.Show(DBfileFullPath + " ||| " + outFileFullPath);
                        if ((File.Exists(DBfileFullPath)) && (!File.Exists(outFileFullPath)) && ((totalSize - copiedSize) >= Convert.ToInt32(files[randFile]["fileSize"])) && (marcado_))
                        {
                            //MessageBox.Show("Copiando[" + copiedSize.ToString() + "/" + totalSize.ToString() + "]: " + DBHandler.dSet.Tables["directories"].Select("dID = " + randDirID.ToString())[0]["dirPath"] + "\\" + files[randFile]["fileName"]);
                            File.Copy(DBfileFullPath, outFileFullPath);
                            nFallos = 0;
                            copiedSize += Convert.ToInt32(files[randFile]["fileSize"]);
                        }
                        else
                            nFallos++;

                        int denominador = (int)(totalSize / 100); // Evitemos DIVIDIR POR CERO
                        if (denominador <= 0)
                            denominador = 1;
                        int ProgressPerc = (int)(copiedSize / denominador);
                        if ((ProgressPerc >= progressBar_CopyProgress.Minimum) && (ProgressPerc <= progressBar_CopyProgress.Maximum))
                            backgroundWorker_CopyProgress.ReportProgress(ProgressPerc);


                    }
                }


/*
                while (copiedSize <= totalSize)
                {
                    // Generamos los IDs aleatorios para cada tabla
                    randDirID = randGen.Next(1, DBHandler.dSet.Tables["directories"].Rows.Count + 1);
                    // randFileID empieza en 0 porque se utiliza como indice en la tabla del DataSet
                    // al contrario que ranDirID que se utiliza para seleccionar mediante el campo "ID".
                    randFileID = randGen.Next(0, DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString()).Length);

                    // Obtenemos la informacion necesaria del fichero seleccionado
                    fileName = DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString())[randFileID]["fileName"].ToString();
                    fSize = Convert.ToInt32(DBHandler.dSet.Tables["files"].Select("dirID = " + randDirID.ToString())[randFileID]["fileSize"]);

                    // Construimos la ruta completa hasta el fichero seleccionado
                    fullPath = DBHandler.dSet.Tables["directories"].Select("dID = " + randDirID.ToString())[0]["dirPath"].ToString();
                    fullPath += "\\" + fileName;

                    // Si existe el fichero a copiar
                    // y no existe un fichero con el mismo nombre en el directorio de destino
                    // y el fichero cabe en el espacio restante de totalSize (es decir totalSize - copiedSize)
                    // => Copiar el fichero seleccionado en el directorio de destino
                    if ((File.Exists(fullPath)) && (!File.Exists(this.textBox_OutPutDir.Text + "\\" + fileName)) && ((totalSize - copiedSize) >= (fSize / 1024)))
                    {
                        File.Copy(fullPath, this.textBox_OutPutDir.Text + "\\" + fileName);
                        nFallos = 0;
                        copiedSize += Convert.ToInt32(fSize / 1024);
                    }
                    else
                    {
                        nFallos++;
                        if (nFallos >= maxFallos)
                        {
                            break; // Evitamos caer en un bucle infinito,
                            // como por ejemplo: El espacio total ocupado por la biblioteca es menor que el
                            // que se ha especificado (para ser rellenado).
                        }
                    }
                    int denominador = (int) (totalSize / 100); // Evitemos DIVIDIR POR CERO
                    if (denominador <= 0)
                        denominador = 1;
                    int ProgressPerc = (int)( copiedSize / denominador);
                    if ((ProgressPerc >= progressBar_CopyProgress.Minimum) && (ProgressPerc <= progressBar_CopyProgress.Maximum))
                        backgroundWorker_CopyProgress.ReportProgress(ProgressPerc);
                }
*/
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

        private void backgroundWorker_treeFiller_DoWork(object sender, DoWorkEventArgs e)
        {
            // Sólo rellenaremos el arbol si hay datos en el DataSet
            if (DBHandler.dSet.Tables["directories"].Rows.Count > 0)
            {
            //    this.button_Library.Enabled = false;
            //    this.button_Start.Enabled = false;

                treeView_Library.BeginUpdate();
                fillTreeView(-1, rootNode);
                treeView_Library.EndUpdate();

            //    this.button_Start.Enabled = true;
            //    this.button_Library.Enabled = true;
            }
        }

        private void backgroundWorker_treeFiller_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar_treeFill.Value = e.ProgressPercentage;
            this.progressBar_treeFill.Refresh();
        }

        private void backgroundWorker_treeFiller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.button_Library.Visible = true;
            this.progressBar_treeFill.Visible = false;

            this.treeView_Library.Nodes.Add(rootNode);
        }
        
        private void fillTreeView(int parentDir, TreeNode parentNode)
        {
            foreach (DataRow dir in DBHandler.dSet.Tables["directories"].Select("fatherID = " + parentDir.ToString()))
            {
                TreeNode newNode = new TreeNode(dir["dirName"].ToString());
                newNode.Tag = dir["dID"].ToString(); // Almacenamos en el campo 'tag' del nodo el ID del directorio
                // Llamada recursiva y "concatenacion" de nodos
                fillTreeView(Convert.ToInt32(dir["dID"]), newNode);
                parentNode.Nodes.Add(newNode);
                nDir++;
                // Actualizar BackgroundWorker
                int denominador = (int)(DBHandler.dSet.Tables["directories"].Rows.Count / 100); // Evitemos DIVIDIR POR CERO
                if (denominador <= 0)
                    denominador = 1;
                int ProgressPerc = (int)(nDir / denominador);
                if ((ProgressPerc >= progressBar_treeFill.Minimum) && (ProgressPerc <= progressBar_treeFill.Maximum))
                    backgroundWorker_treeFiller.ReportProgress(ProgressPerc);
            }
            backgroundWorker_treeFiller.ReportProgress(100);
        }

        // Al marcar un nodo como 'checked' o 'unchecked', hacemos lo mismo con sus hijos
        private void treeView_Library_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Si se ha marcado como checked
            if (e.Node.Checked == true)
                checkUncheckChilds(e.Node, 0);
            else
                checkUncheckChilds(e.Node, 1);
        }

        // Marca/Desmarca el nodo 'father' y todos sus hijos
        // checkUncheck = 0 => Check
        // checkUncheck = 1 => Uncheck
        private void checkUncheckChilds(TreeNode father, byte checkUncheck)
        {
            foreach (TreeNode child in father.Nodes)
            {
                switch (checkUncheck)
                {
                    case 0:
                        child.Checked = true;
                        break;
                    case 1:
                        child.Checked = false;
                        break;
                }
                checkUncheckChilds(child, checkUncheck);
            }
        }

        // Cuenta los nodos seleccionados
        private int checkedCount(TreeNode padre)
        {
            int result = 0;
            if (padre.Nodes.Count > 0)
            {
                foreach (TreeNode nodo in padre.Nodes)
                {
                    if (nodo.Checked)
                        result++;
                    result += checkedCount(nodo);
                }
            }
            return result;
        }

        // Comprueba el nodo "nodeName" esta marcado en el subarbol con raiz "rootNode"
        private bool isCheckedTreeNode(TreeNode rootNode, String nodeText)
        {
            if ((rootNode.Text == nodeText) && (rootNode.Checked))
                return true;
            else
            {
                foreach (TreeNode hijo in rootNode.Nodes)
                {
                    if (isCheckedTreeNode(hijo, nodeText))
                        return true;
                }
            }
            return false;
        }

    }

}
