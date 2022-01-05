namespace XplaneUtils
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_ErrorsList = new System.Windows.Forms.Button();
            this.button_SceneryPacks = new System.Windows.Forms.Button();
            this.label_Warning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_ErrorsList
            // 
            this.button_ErrorsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ErrorsList.Location = new System.Drawing.Point(146, 125);
            this.button_ErrorsList.Name = "button_ErrorsList";
            this.button_ErrorsList.Size = new System.Drawing.Size(200, 80);
            this.button_ErrorsList.TabIndex = 0;
            this.button_ErrorsList.Text = "Список ошибок";
            this.button_ErrorsList.UseVisualStyleBackColor = true;
            this.button_ErrorsList.Click += new System.EventHandler(this.ShowErrorsList);
            // 
            // button_SceneryPacks
            // 
            this.button_SceneryPacks.Enabled = false;
            this.button_SceneryPacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SceneryPacks.Location = new System.Drawing.Point(146, 285);
            this.button_SceneryPacks.Name = "button_SceneryPacks";
            this.button_SceneryPacks.Size = new System.Drawing.Size(200, 80);
            this.button_SceneryPacks.TabIndex = 0;
            this.button_SceneryPacks.Text = "Отсортировать\r\nscenery_packs.ini";
            this.button_SceneryPacks.UseVisualStyleBackColor = true;
            // 
            // label_Warning
            // 
            this.label_Warning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Warning.AutoSize = true;
            this.label_Warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Warning.Location = new System.Drawing.Point(12, 9);
            this.label_Warning.Name = "label_Warning";
            this.label_Warning.Size = new System.Drawing.Size(192, 24);
            this.label_Warning.TabIndex = 1;
            this.label_Warning.Text = "Выберете действие";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 471);
            this.Controls.Add(this.label_Warning);
            this.Controls.Add(this.button_SceneryPacks);
            this.Controls.Add(this.button_ErrorsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "X-plane Utilities";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ErrorsList;
        private System.Windows.Forms.Button button_SceneryPacks;
        private System.Windows.Forms.Label label_Warning;
    }
}

