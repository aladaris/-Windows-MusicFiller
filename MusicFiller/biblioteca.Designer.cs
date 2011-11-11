namespace MusicFiller
{
    partial class Form_Biblioteca
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_nFilesValue = new System.Windows.Forms.Label();
            this.label_nFiles = new System.Windows.Forms.Label();
            this.label_RootLibPath = new System.Windows.Forms.Label();
            this.textBox_RootLibPath = new System.Windows.Forms.TextBox();
            this.button_RootLibPathBrowse = new System.Windows.Forms.Button();
            this.button_CreateLib = new System.Windows.Forms.Button();
            this.progressBar_Library = new System.Windows.Forms.ProgressBar();
            this.button_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_nFilesValue
            // 
            this.label_nFilesValue.AutoSize = true;
            this.label_nFilesValue.Location = new System.Drawing.Point(123, 9);
            this.label_nFilesValue.Name = "label_nFilesValue";
            this.label_nFilesValue.Size = new System.Drawing.Size(34, 13);
            this.label_nFilesValue.TabIndex = 0;
            this.label_nFilesValue.Text = "nFiles";
            // 
            // label_nFiles
            // 
            this.label_nFiles.AutoSize = true;
            this.label_nFiles.Location = new System.Drawing.Point(12, 9);
            this.label_nFiles.Name = "label_nFiles";
            this.label_nFiles.Size = new System.Drawing.Size(105, 13);
            this.label_nFiles.TabIndex = 1;
            this.label_nFiles.Text = "Número de archivos:";
            // 
            // label_RootLibPath
            // 
            this.label_RootLibPath.AutoSize = true;
            this.label_RootLibPath.Location = new System.Drawing.Point(12, 42);
            this.label_RootLibPath.Name = "label_RootLibPath";
            this.label_RootLibPath.Size = new System.Drawing.Size(150, 13);
            this.label_RootLibPath.TabIndex = 2;
            this.label_RootLibPath.Text = "Directorio raíz de la biblioteca:";
            // 
            // textBox_RootLibPath
            // 
            this.textBox_RootLibPath.Location = new System.Drawing.Point(168, 39);
            this.textBox_RootLibPath.Name = "textBox_RootLibPath";
            this.textBox_RootLibPath.Size = new System.Drawing.Size(228, 20);
            this.textBox_RootLibPath.TabIndex = 3;
            // 
            // button_RootLibPathBrowse
            // 
            this.button_RootLibPathBrowse.Location = new System.Drawing.Point(402, 37);
            this.button_RootLibPathBrowse.Name = "button_RootLibPathBrowse";
            this.button_RootLibPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.button_RootLibPathBrowse.TabIndex = 4;
            this.button_RootLibPathBrowse.Text = "&Examinar...";
            this.button_RootLibPathBrowse.UseVisualStyleBackColor = true;
            this.button_RootLibPathBrowse.Click += new System.EventHandler(this.button_RootLibPathBrowse_Click);
            // 
            // button_CreateLib
            // 
            this.button_CreateLib.Location = new System.Drawing.Point(15, 84);
            this.button_CreateLib.Name = "button_CreateLib";
            this.button_CreateLib.Size = new System.Drawing.Size(102, 23);
            this.button_CreateLib.TabIndex = 5;
            this.button_CreateLib.Text = "&Crear biblioteca";
            this.button_CreateLib.UseVisualStyleBackColor = true;
            this.button_CreateLib.Click += new System.EventHandler(this.button_CreateLib_Click);
            // 
            // progressBar_Library
            // 
            this.progressBar_Library.Enabled = false;
            this.progressBar_Library.Location = new System.Drawing.Point(126, 84);
            this.progressBar_Library.MarqueeAnimationSpeed = 0;
            this.progressBar_Library.Name = "progressBar_Library";
            this.progressBar_Library.Size = new System.Drawing.Size(351, 23);
            this.progressBar_Library.Step = 1;
            this.progressBar_Library.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_Library.TabIndex = 6;
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(402, 133);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(75, 23);
            this.button_Exit.TabIndex = 7;
            this.button_Exit.Text = "&Salir";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // Form_Biblioteca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 188);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.progressBar_Library);
            this.Controls.Add(this.button_CreateLib);
            this.Controls.Add(this.button_RootLibPathBrowse);
            this.Controls.Add(this.textBox_RootLibPath);
            this.Controls.Add(this.label_RootLibPath);
            this.Controls.Add(this.label_nFiles);
            this.Controls.Add(this.label_nFilesValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Biblioteca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Biblioteca";
            this.Load += new System.EventHandler(this.biblioteca_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Data.SQLite.SQLiteDataAdapter sqlDA;
        //private System.Data.DataSet dSet;
        //private System.Data.SQLite.SQLiteDataAdapter dirDA;
        //private System.Data.SQLite.SQLiteDataAdapter filesDA;
        //private int gobalDirID;

        private System.Windows.Forms.Label label_nFilesValue;
        private System.Windows.Forms.Label label_nFiles;
        private System.Windows.Forms.Label label_RootLibPath;
        private System.Windows.Forms.TextBox textBox_RootLibPath;
        private System.Windows.Forms.Button button_RootLibPathBrowse;
        private System.Windows.Forms.Button button_CreateLib;
        private System.Windows.Forms.ProgressBar progressBar_Library;
        private System.Windows.Forms.Button button_Exit;
    }
}