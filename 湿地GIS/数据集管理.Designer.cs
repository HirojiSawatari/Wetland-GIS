namespace 湿地GIS
{
    partial class 数据集管理
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
            this.groupDatasets = new System.Windows.Forms.GroupBox();
            this.datasetView = new System.Windows.Forms.TreeView();
            this.deleteCurrent = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.changeName = new System.Windows.Forms.Button();
            this.groupModify = new System.Windows.Forms.GroupBox();
            this.newNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datasetTypeCombox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupCreateNew = new System.Windows.Forms.GroupBox();
            this.createDataset = new System.Windows.Forms.Button();
            this.createDatasetName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupDatasets.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupModify.SuspendLayout();
            this.groupCreateNew.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupDatasets
            // 
            this.groupDatasets.BackColor = System.Drawing.Color.White;
            this.groupDatasets.Controls.Add(this.datasetView);
            this.groupDatasets.ForeColor = System.Drawing.Color.Black;
            this.groupDatasets.Location = new System.Drawing.Point(1, 12);
            this.groupDatasets.Name = "groupDatasets";
            this.groupDatasets.Size = new System.Drawing.Size(286, 475);
            this.groupDatasets.TabIndex = 1;
            this.groupDatasets.TabStop = false;
            this.groupDatasets.Text = "数据集列表";
            // 
            // datasetView
            // 
            this.datasetView.BackColor = System.Drawing.Color.White;
            this.datasetView.ForeColor = System.Drawing.Color.Black;
            this.datasetView.Location = new System.Drawing.Point(11, 21);
            this.datasetView.Name = "datasetView";
            this.datasetView.Size = new System.Drawing.Size(258, 448);
            this.datasetView.TabIndex = 0;
            // 
            // deleteCurrent
            // 
            this.deleteCurrent.BackColor = System.Drawing.Color.White;
            this.deleteCurrent.ForeColor = System.Drawing.Color.Black;
            this.deleteCurrent.Location = new System.Drawing.Point(60, 57);
            this.deleteCurrent.Name = "deleteCurrent";
            this.deleteCurrent.Size = new System.Drawing.Size(148, 32);
            this.deleteCurrent.TabIndex = 0;
            this.deleteCurrent.Text = "删除选中";
            this.deleteCurrent.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.deleteCurrent);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(305, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 124);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "删除选中数据集";
            // 
            // changeName
            // 
            this.changeName.BackColor = System.Drawing.Color.White;
            this.changeName.ForeColor = System.Drawing.Color.Black;
            this.changeName.Location = new System.Drawing.Point(120, 91);
            this.changeName.Name = "changeName";
            this.changeName.Size = new System.Drawing.Size(88, 23);
            this.changeName.TabIndex = 2;
            this.changeName.Text = "重命名";
            this.changeName.UseVisualStyleBackColor = false;
            // 
            // groupModify
            // 
            this.groupModify.BackColor = System.Drawing.Color.White;
            this.groupModify.Controls.Add(this.changeName);
            this.groupModify.Controls.Add(this.newNameTextBox);
            this.groupModify.Controls.Add(this.label1);
            this.groupModify.ForeColor = System.Drawing.Color.Black;
            this.groupModify.Location = new System.Drawing.Point(305, 186);
            this.groupModify.Name = "groupModify";
            this.groupModify.Size = new System.Drawing.Size(235, 124);
            this.groupModify.TabIndex = 3;
            this.groupModify.TabStop = false;
            this.groupModify.Text = "修改选中数据集";
            // 
            // newNameTextBox
            // 
            this.newNameTextBox.BackColor = System.Drawing.Color.White;
            this.newNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.newNameTextBox.Location = new System.Drawing.Point(82, 48);
            this.newNameTextBox.Name = "newNameTextBox";
            this.newNameTextBox.Size = new System.Drawing.Size(135, 22);
            this.newNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(21, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "新名称:";
            // 
            // datasetTypeCombox
            // 
            this.datasetTypeCombox.BackColor = System.Drawing.Color.White;
            this.datasetTypeCombox.ForeColor = System.Drawing.Color.Black;
            this.datasetTypeCombox.FormattingEnabled = true;
            this.datasetTypeCombox.Location = new System.Drawing.Point(82, 37);
            this.datasetTypeCombox.Name = "datasetTypeCombox";
            this.datasetTypeCombox.Size = new System.Drawing.Size(135, 21);
            this.datasetTypeCombox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据集类型:";
            // 
            // groupCreateNew
            // 
            this.groupCreateNew.BackColor = System.Drawing.Color.White;
            this.groupCreateNew.Controls.Add(this.datasetTypeCombox);
            this.groupCreateNew.Controls.Add(this.label3);
            this.groupCreateNew.Controls.Add(this.createDataset);
            this.groupCreateNew.Controls.Add(this.createDatasetName);
            this.groupCreateNew.Controls.Add(this.label2);
            this.groupCreateNew.ForeColor = System.Drawing.Color.Black;
            this.groupCreateNew.Location = new System.Drawing.Point(305, 341);
            this.groupCreateNew.Name = "groupCreateNew";
            this.groupCreateNew.Size = new System.Drawing.Size(235, 140);
            this.groupCreateNew.TabIndex = 4;
            this.groupCreateNew.TabStop = false;
            this.groupCreateNew.Text = "新建数据集";
            // 
            // createDataset
            // 
            this.createDataset.BackColor = System.Drawing.Color.White;
            this.createDataset.ForeColor = System.Drawing.Color.Black;
            this.createDataset.Location = new System.Drawing.Point(120, 111);
            this.createDataset.Name = "createDataset";
            this.createDataset.Size = new System.Drawing.Size(88, 23);
            this.createDataset.TabIndex = 2;
            this.createDataset.Text = "创建";
            this.createDataset.UseVisualStyleBackColor = false;
            // 
            // createDatasetName
            // 
            this.createDatasetName.BackColor = System.Drawing.Color.White;
            this.createDatasetName.ForeColor = System.Drawing.Color.Black;
            this.createDatasetName.Location = new System.Drawing.Point(82, 71);
            this.createDatasetName.Name = "createDatasetName";
            this.createDatasetName.Size = new System.Drawing.Size(135, 22);
            this.createDatasetName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据集名称:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(305, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 124);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "删除选中数据集";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(60, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "删除选中";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // 数据集管理
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 504);
            this.Controls.Add(this.groupCreateNew);
            this.Controls.Add(this.groupModify);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupDatasets);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "数据集管理";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据集管理";
            this.groupDatasets.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupModify.ResumeLayout(false);
            this.groupModify.PerformLayout();
            this.groupCreateNew.ResumeLayout(false);
            this.groupCreateNew.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDatasets;
        private System.Windows.Forms.TreeView datasetView;
        private System.Windows.Forms.Button deleteCurrent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button changeName;
        private System.Windows.Forms.GroupBox groupModify;
        private System.Windows.Forms.TextBox newNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox datasetTypeCombox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupCreateNew;
        private System.Windows.Forms.Button createDataset;
        private System.Windows.Forms.TextBox createDatasetName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
    }
}