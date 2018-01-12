namespace Proiect
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Dest1L = new System.Windows.Forms.Label();
            this.SalutL = new System.Windows.Forms.Label();
            this.Dest2L = new System.Windows.Forms.Label();
            this.Dest1C = new System.Windows.Forms.ComboBox();
            this.Dest2C = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.DataL = new System.Windows.Forms.Label();
            this.CautaB = new System.Windows.Forms.Button();
            this.avioaneDataSet = new Proiect.AvioaneDataSet();
            this.bileteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bileteTableAdapter = new Proiect.AvioaneDataSetTableAdapters.BileteTableAdapter();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CheckinB = new System.Windows.Forms.Button();
            this.CumparaB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.avioaneDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bileteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(420, 400);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Dest1L
            // 
            this.Dest1L.AutoSize = true;
            this.Dest1L.Location = new System.Drawing.Point(110, 40);
            this.Dest1L.Name = "Dest1L";
            this.Dest1L.Size = new System.Drawing.Size(35, 13);
            this.Dest1L.TabIndex = 2;
            this.Dest1L.Text = "label1";
            // 
            // SalutL
            // 
            this.SalutL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SalutL.AutoSize = true;
            this.SalutL.Location = new System.Drawing.Point(290, 10);
            this.SalutL.Name = "SalutL";
            this.SalutL.Size = new System.Drawing.Size(35, 13);
            this.SalutL.TabIndex = 3;
            this.SalutL.Text = "label2";
            // 
            // Dest2L
            // 
            this.Dest2L.AutoSize = true;
            this.Dest2L.Location = new System.Drawing.Point(480, 40);
            this.Dest2L.Name = "Dest2L";
            this.Dest2L.Size = new System.Drawing.Size(35, 13);
            this.Dest2L.TabIndex = 4;
            this.Dest2L.Text = "label3";
            // 
            // Dest1C
            // 
            this.Dest1C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dest1C.FormattingEnabled = true;
            this.Dest1C.Location = new System.Drawing.Point(60, 60);
            this.Dest1C.Name = "Dest1C";
            this.Dest1C.Size = new System.Drawing.Size(121, 21);
            this.Dest1C.TabIndex = 5;
            this.Dest1C.SelectedIndexChanged += new System.EventHandler(this.Dest1C_SelectedIndexChanged);
            // 
            // Dest2C
            // 
            this.Dest2C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dest2C.FormattingEnabled = true;
            this.Dest2C.Location = new System.Drawing.Point(440, 60);
            this.Dest2C.Name = "Dest2C";
            this.Dest2C.Size = new System.Drawing.Size(121, 21);
            this.Dest2C.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(210, 100);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // DataL
            // 
            this.DataL.AutoSize = true;
            this.DataL.Location = new System.Drawing.Point(290, 80);
            this.DataL.Name = "DataL";
            this.DataL.Size = new System.Drawing.Size(33, 13);
            this.DataL.TabIndex = 8;
            this.DataL.Text = "labe1";
            // 
            // CautaB
            // 
            this.CautaB.Location = new System.Drawing.Point(260, 150);
            this.CautaB.Name = "CautaB";
            this.CautaB.Size = new System.Drawing.Size(100, 23);
            this.CautaB.TabIndex = 9;
            this.CautaB.Text = "Cauta Bilete";
            this.CautaB.UseVisualStyleBackColor = true;
            this.CautaB.Click += new System.EventHandler(this.CautaB_Click);
            // 
            // avioaneDataSet
            // 
            this.avioaneDataSet.DataSetName = "AvioaneDataSet";
            this.avioaneDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bileteBindingSource
            // 
            this.bileteBindingSource.DataMember = "Bilete";
            this.bileteBindingSource.DataSource = this.avioaneDataSet;
            // 
            // bileteTableAdapter
            // 
            this.bileteTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(50, 190);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(540, 150);
            this.dataGridView1.TabIndex = 10;
            // 
            // CheckinB
            // 
            this.CheckinB.Location = new System.Drawing.Point(470, 360);
            this.CheckinB.Name = "CheckinB";
            this.CheckinB.Size = new System.Drawing.Size(95, 23);
            this.CheckinB.TabIndex = 11;
            this.CheckinB.Text = "CHECK-IN";
            this.CheckinB.UseVisualStyleBackColor = true;
            // 
            // CumparaB
            // 
            this.CumparaB.Location = new System.Drawing.Point(80, 360);
            this.CumparaB.Name = "CumparaB";
            this.CumparaB.Size = new System.Drawing.Size(120, 23);
            this.CumparaB.TabIndex = 12;
            this.CumparaB.Text = "Cumpara Biletele";
            this.CumparaB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 429);
            this.Controls.Add(this.CumparaB);
            this.Controls.Add(this.CheckinB);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.CautaB);
            this.Controls.Add(this.DataL);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.Dest2C);
            this.Controls.Add(this.Dest1C);
            this.Controls.Add(this.Dest2L);
            this.Controls.Add(this.SalutL);
            this.Controls.Add(this.Dest1L);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.avioaneDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bileteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Dest1L;
        private System.Windows.Forms.Label SalutL;
        private System.Windows.Forms.Label Dest2L;
        private System.Windows.Forms.ComboBox Dest1C;
        private System.Windows.Forms.ComboBox Dest2C;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label DataL;
        private System.Windows.Forms.Button CautaB;
        private AvioaneDataSet avioaneDataSet;
        private System.Windows.Forms.BindingSource bileteBindingSource;
        private AvioaneDataSetTableAdapters.BileteTableAdapter bileteTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button CheckinB;
        private System.Windows.Forms.Button CumparaB;
    }
}

