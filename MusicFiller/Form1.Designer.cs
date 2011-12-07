namespace MusicFiller
{
    partial class Form_Main
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Library = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Start = new System.Windows.Forms.Button();
            this.label_OutputDir = new System.Windows.Forms.Label();
            this.button_OutPutDir = new System.Windows.Forms.Button();
            this.textBox_OutPutDir = new System.Windows.Forms.TextBox();
            this.treeView_Library = new System.Windows.Forms.TreeView();
            this.label_fillSpace = new System.Windows.Forms.Label();
            this.textBox_fillSpace = new System.Windows.Forms.TextBox();
            this.progressBar_CopyProgress = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker_CopyProgress = new System.ComponentModel.BackgroundWorker();
            this.label_progressPercentage = new System.Windows.Forms.Label();
            this.backgroundWorker_treeFiller = new System.ComponentModel.BackgroundWorker();
            this.progressBar_treeFill = new System.Windows.Forms.ProgressBar();
            this.label_treeFill = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Library
            // 
            this.button_Library.Location = new System.Drawing.Point(12, 455);
            this.button_Library.Name = "button_Library";
            this.button_Library.Size = new System.Drawing.Size(75, 23);
            this.button_Library.TabIndex = 11;
            this.button_Library.Text = "&Biblioteca";
            this.button_Library.UseVisualStyleBackColor = true;
            this.button_Library.Click += new System.EventHandler(this.button_Library_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(366, 455);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(75, 23);
            this.button_Exit.TabIndex = 10;
            this.button_Exit.Text = "&Salir";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(12, 388);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 9;
            this.button_Start.Text = "Empe&zar";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // label_OutputDir
            // 
            this.label_OutputDir.AutoSize = true;
            this.label_OutputDir.Location = new System.Drawing.Point(12, 310);
            this.label_OutputDir.Name = "label_OutputDir";
            this.label_OutputDir.Size = new System.Drawing.Size(88, 13);
            this.label_OutputDir.TabIndex = 6;
            this.label_OutputDir.Text = "OutPut Directory:";
            // 
            // button_OutPutDir
            // 
            this.button_OutPutDir.Location = new System.Drawing.Point(366, 305);
            this.button_OutPutDir.Name = "button_OutPutDir";
            this.button_OutPutDir.Size = new System.Drawing.Size(75, 23);
            this.button_OutPutDir.TabIndex = 8;
            this.button_OutPutDir.Text = "Examinar...";
            this.button_OutPutDir.UseVisualStyleBackColor = true;
            this.button_OutPutDir.Click += new System.EventHandler(this.button_OutPutDir_Click);
            // 
            // textBox_OutPutDir
            // 
            this.textBox_OutPutDir.Location = new System.Drawing.Point(106, 307);
            this.textBox_OutPutDir.Name = "textBox_OutPutDir";
            this.textBox_OutPutDir.Size = new System.Drawing.Size(254, 20);
            this.textBox_OutPutDir.TabIndex = 7;
            // 
            // treeView_Library
            // 
            this.treeView_Library.CheckBoxes = true;
            this.treeView_Library.Location = new System.Drawing.Point(15, 12);
            this.treeView_Library.Name = "treeView_Library";
            this.treeView_Library.Size = new System.Drawing.Size(426, 220);
            this.treeView_Library.TabIndex = 12;
            // 
            // label_fillSpace
            // 
            this.label_fillSpace.AutoSize = true;
            this.label_fillSpace.Location = new System.Drawing.Point(103, 351);
            this.label_fillSpace.Name = "label_fillSpace";
            this.label_fillSpace.Size = new System.Drawing.Size(144, 13);
            this.label_fillSpace.TabIndex = 13;
            this.label_fillSpace.Text = "Espacio a llenar (en MBytes):";
            // 
            // textBox_fillSpace
            // 
            this.textBox_fillSpace.Location = new System.Drawing.Point(253, 348);
            this.textBox_fillSpace.Name = "textBox_fillSpace";
            this.textBox_fillSpace.Size = new System.Drawing.Size(106, 20);
            this.textBox_fillSpace.TabIndex = 14;
            // 
            // progressBar_CopyProgress
            // 
            this.progressBar_CopyProgress.Location = new System.Drawing.Point(93, 388);
            this.progressBar_CopyProgress.Name = "progressBar_CopyProgress";
            this.progressBar_CopyProgress.Size = new System.Drawing.Size(348, 23);
            this.progressBar_CopyProgress.TabIndex = 15;
            // 
            // backgroundWorker_CopyProgress
            // 
            this.backgroundWorker_CopyProgress.WorkerReportsProgress = true;
            this.backgroundWorker_CopyProgress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_CopyProgress_DoWork);
            this.backgroundWorker_CopyProgress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_CopyProgress_RunWorkerCompleted);
            this.backgroundWorker_CopyProgress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_CopyProgress_ProgressChanged);
            // 
            // label_progressPercentage
            // 
            this.label_progressPercentage.AutoSize = true;
            this.label_progressPercentage.Location = new System.Drawing.Point(90, 414);
            this.label_progressPercentage.Name = "label_progressPercentage";
            this.label_progressPercentage.Size = new System.Drawing.Size(24, 13);
            this.label_progressPercentage.TabIndex = 16;
            this.label_progressPercentage.Text = "0 %";
            // 
            // backgroundWorker_treeFiller
            // 
            this.backgroundWorker_treeFiller.WorkerReportsProgress = true;
            this.backgroundWorker_treeFiller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_treeFiller_DoWork);
            this.backgroundWorker_treeFiller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_treeFiller_RunWorkerCompleted);
            this.backgroundWorker_treeFiller.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_treeFiller_ProgressChanged);
            // 
            // progressBar_treeFill
            // 
            this.progressBar_treeFill.Location = new System.Drawing.Point(15, 262);
            this.progressBar_treeFill.Name = "progressBar_treeFill";
            this.progressBar_treeFill.Size = new System.Drawing.Size(426, 23);
            this.progressBar_treeFill.Step = 1;
            this.progressBar_treeFill.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_treeFill.TabIndex = 17;
            // 
            // label_treeFill
            // 
            this.label_treeFill.AutoSize = true;
            this.label_treeFill.Location = new System.Drawing.Point(12, 246);
            this.label_treeFill.Name = "label_treeFill";
            this.label_treeFill.Size = new System.Drawing.Size(121, 13);
            this.label_treeFill.TabIndex = 18;
            this.label_treeFill.Text = "Cargando biblioteca: 0%";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 500);
            this.Controls.Add(this.label_treeFill);
            this.Controls.Add(this.progressBar_treeFill);
            this.Controls.Add(this.label_progressPercentage);
            this.Controls.Add(this.progressBar_CopyProgress);
            this.Controls.Add(this.textBox_fillSpace);
            this.Controls.Add(this.label_fillSpace);
            this.Controls.Add(this.treeView_Library);
            this.Controls.Add(this.button_Library);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.label_OutputDir);
            this.Controls.Add(this.button_OutPutDir);
            this.Controls.Add(this.textBox_OutPutDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Music Filler";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeNode rootNode; // Nodo raiz del TreeView
        private int nDir; // Utilizada para calcular el porcentaje del rellenado del treeView

        private System.Windows.Forms.Button button_Library;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Label label_OutputDir;
        private System.Windows.Forms.Button button_OutPutDir;
        private System.Windows.Forms.TextBox textBox_OutPutDir;
        private System.Windows.Forms.TreeView treeView_Library;
        private System.Windows.Forms.Label label_fillSpace;
        private System.Windows.Forms.TextBox textBox_fillSpace;
        private System.Windows.Forms.ProgressBar progressBar_CopyProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker_CopyProgress;
        private System.Windows.Forms.Label label_progressPercentage;
        private System.ComponentModel.BackgroundWorker backgroundWorker_treeFiller;
        private System.Windows.Forms.ProgressBar progressBar_treeFill;
        private System.Windows.Forms.Label label_treeFill;

    }
}

