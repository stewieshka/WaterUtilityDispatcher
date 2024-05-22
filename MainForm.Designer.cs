namespace WaterUtilityDispatcher
{
    partial class MainForm
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            MakeReport = new Button();
            DateDoTextBox = new TextBox();
            DateOtTextBox = new TextBox();
            TypeTextBox = new TextBox();
            LocationTextBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button5 = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(100, 23);
            button1.TabIndex = 0;
            button1.Text = "Работники";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(0, 23);
            button2.Name = "button2";
            button2.Size = new Size(100, 23);
            button2.TabIndex = 1;
            button2.Text = "Бригады";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(0, 46);
            button3.Name = "button3";
            button3.Size = new Size(100, 23);
            button3.TabIndex = 2;
            button3.Text = "Инциденты";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(0, 69);
            button4.Name = "button4";
            button4.Size = new Size(100, 23);
            button4.TabIndex = 3;
            button4.Text = "Материалы";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(106, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(693, 449);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(MakeReport);
            tabPage1.Controls.Add(DateDoTextBox);
            tabPage1.Controls.Add(DateOtTextBox);
            tabPage1.Controls.Add(TypeTextBox);
            tabPage1.Controls.Add(LocationTextBox);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(685, 421);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "ReportPage";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // MakeReport
            // 
            MakeReport.Location = new Point(3, 94);
            MakeReport.Name = "MakeReport";
            MakeReport.Size = new Size(121, 23);
            MakeReport.TabIndex = 7;
            MakeReport.Text = "Составить отчет";
            MakeReport.UseVisualStyleBackColor = true;
            MakeReport.Click += MakeReport_Click;
            // 
            // DateDoTextBox
            // 
            DateDoTextBox.Location = new Point(130, 65);
            DateDoTextBox.Name = "DateDoTextBox";
            DateDoTextBox.Size = new Size(124, 23);
            DateDoTextBox.TabIndex = 6;
            // 
            // DateOtTextBox
            // 
            DateOtTextBox.Location = new Point(3, 65);
            DateOtTextBox.Name = "DateOtTextBox";
            DateOtTextBox.Size = new Size(121, 23);
            DateOtTextBox.TabIndex = 5;
            // 
            // TypeTextBox
            // 
            TypeTextBox.Location = new Point(130, 21);
            TypeTextBox.Name = "TypeTextBox";
            TypeTextBox.Size = new Size(124, 23);
            TypeTextBox.TabIndex = 4;
            // 
            // LocationTextBox
            // 
            LocationTextBox.Location = new Point(3, 21);
            LocationTextBox.Name = "LocationTextBox";
            LocationTextBox.Size = new Size(121, 23);
            LocationTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 47);
            label3.Name = "label3";
            label3.Size = new Size(165, 15);
            label3.TabIndex = 2;
            label3.Text = "Временной интервал от и до";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(130, 3);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 1;
            label2.Text = "Вид";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Участок";
            // 
            // button5
            // 
            button5.Location = new Point(0, 92);
            button5.Name = "button5";
            button5.Size = new Size(100, 23);
            button5.TabIndex = 5;
            button5.Text = "Отчет";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button5);
            Controls.Add(tabControl1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "Программа диспетчера";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TabControl tabControl1;
        private Button button5;
        private TabPage tabPage1;
        private Label label1;
        private Button MakeReport;
        private TextBox DateDoTextBox;
        private TextBox DateOtTextBox;
        private TextBox TypeTextBox;
        private TextBox LocationTextBox;
        private Label label3;
        private Label label2;
    }
}