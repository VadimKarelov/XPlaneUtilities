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
            this.components = new System.ComponentModel.Container();
            this.button_ErrorsList = new System.Windows.Forms.Button();
            this.button_SceneryPacks = new System.Windows.Forms.Button();
            this.timer_CheckThreads = new System.Windows.Forms.Timer(this.components);
            this.button_ShadowEnhancement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_ErrorsList
            // 
            this.button_ErrorsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ErrorsList.Location = new System.Drawing.Point(12, 12);
            this.button_ErrorsList.Name = "button_ErrorsList";
            this.button_ErrorsList.Size = new System.Drawing.Size(200, 80);
            this.button_ErrorsList.TabIndex = 0;
            this.button_ErrorsList.Text = "Список ошибок";
            this.button_ErrorsList.UseVisualStyleBackColor = true;
            this.button_ErrorsList.Click += new System.EventHandler(this.ShowErrorsList_Clicked);
            // 
            // button_SceneryPacks
            // 
            this.button_SceneryPacks.Enabled = false;
            this.button_SceneryPacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SceneryPacks.Location = new System.Drawing.Point(12, 98);
            this.button_SceneryPacks.Name = "button_SceneryPacks";
            this.button_SceneryPacks.Size = new System.Drawing.Size(200, 80);
            this.button_SceneryPacks.TabIndex = 0;
            this.button_SceneryPacks.Text = "Отсортировать\r\nscenery_packs.ini";
            this.button_SceneryPacks.UseVisualStyleBackColor = true;
            // 
            // timer_CheckThreads
            // 
            this.timer_CheckThreads.Enabled = true;
            this.timer_CheckThreads.Interval = 1;
            this.timer_CheckThreads.Tick += new System.EventHandler(this.CheckThreads_Tick);
            // 
            // button_ShadowEnhancement
            // 
            this.button_ShadowEnhancement.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ShadowEnhancement.Location = new System.Drawing.Point(12, 184);
            this.button_ShadowEnhancement.Name = "button_ShadowEnhancement";
            this.button_ShadowEnhancement.Size = new System.Drawing.Size(200, 80);
            this.button_ShadowEnhancement.TabIndex = 0;
            this.button_ShadowEnhancement.Text = "Улучшить тени";
            this.button_ShadowEnhancement.UseVisualStyleBackColor = true;
            this.button_ShadowEnhancement.Click += new System.EventHandler(this.ShadowEnhancement_Clicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 270);
            this.Controls.Add(this.button_ShadowEnhancement);
            this.Controls.Add(this.button_SceneryPacks);
            this.Controls.Add(this.button_ErrorsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "X-plane Utilities";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ErrorsList;
        private System.Windows.Forms.Button button_SceneryPacks;
        private System.Windows.Forms.Timer timer_CheckThreads;
        private System.Windows.Forms.Button button_ShadowEnhancement;
    }
}

