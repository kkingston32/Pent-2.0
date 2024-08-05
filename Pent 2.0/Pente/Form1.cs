// Pent 2.0. 
//  Created by EL HIRACH ABDERRAZZAK on 2024-02-14. 
//  Copyright © 2024 aelhirach.me.  All rights reserved.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Pente
{
	public sealed class Form1 : Form
	{
		private IContainer components;

		private PictureBox pictureBox1;

		private Bitmap bitmap;
        private TextBox textBox1;
        private Button button1;
        private Label label1;
        private bool end;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1731, 928);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1358, 155);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Enter Your Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1456, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1460, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 73);
            this.label1.TabIndex = 3;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1731, 928);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pente game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            
            this.ResumeLayout(false);
            this.PerformLayout();



            
		}


        public Form1()
		{
            this.InitializeComponent();

			//MessageBox.Show("Hi. This game is called Pente.\nA pente is five stones in a straight line. Forming a pente gives a victory to the player.\nYou play white and I play black.\n\nThis is my first artifical intelligence game!\nEnjoy...Matin Lotfali", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.end = false;
			MiniMax.board = new Board();
			this.Draw();
			this.pictureBox1.Image = this.bitmap;

            this.textBox1.Visible = true;
            this.button1.Visible = true;
            this.label1.Text = string.Empty;

        }

		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
		}

		private void Draw()
		{
			if (this.bitmap == null)
			{
				this.bitmap = new Bitmap(380, 380);
			}
			Graphics.FromImage(this.bitmap).Clear(Color.DarkOrange);
			Pen pen = new Pen(Brushes.LightGray);
			for (int i = 0; i < 19; i++)
			{
				Graphics.FromImage(this.bitmap).DrawLine(pen, 10, 10 + i * 20, 370, 10 + i * 20);
				Graphics.FromImage(this.bitmap).DrawLine(pen, 10 + i * 20, 10, 10 + i * 20, 370);
			}
			for (int j = 0; j < 19; j++)
			{
				for (int k = 0; k < 19; k++)
				{
					if (MiniMax.board.area[j, k] != Place.Null)
					{
						if (MiniMax.board.area[j, k] == Place.Black)
						{
							Graphics.FromImage(this.bitmap).FillEllipse(Brushes.Black, 20 * j, 20 * k, 20, 20);
						}
						else
						{
							Graphics.FromImage(this.bitmap).FillEllipse(Brushes.White, 20 * j, 20 * k, 20, 20);
						}
					}
				}
			}
			if (MiniMax.board.lastMoveX != -1)
			{
				Graphics.FromImage(this.bitmap).DrawEllipse(new Pen(Brushes.Yellow, 3f), 20 * MiniMax.board.lastMoveX, 20 * MiniMax.board.lastMoveY, 20, 20);
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (!this.end)
			{
				double num = (double)this.pictureBox1.Height / (double)this.bitmap.Height;
				int num2 = (int)((double)this.pictureBox1.Width - num * (double)this.bitmap.Width) / 2;
				int num3 = (int)((double)(e.X - num2) / num);
				int num4 = (int)((double)e.Y / num);
				num3 /= 20;
				num4 /= 20;
				if (num3 < 0 || num3 >= 19 || num4 < 0 || num4 >= 19)
				{
					return;
				}
				if (MiniMax.board.area[num3, num4] == Place.Null)
				{
					MiniMax.WhiteMove(num3, num4);
					Rectangle rectangle = MiniMax.board.IsGameOver();
					this.Draw();
					if (!rectangle.IsEmpty)
					{
						Graphics.FromImage(this.bitmap).DrawLine(new Pen(Brushes.Red, 5f), 10 + rectangle.Left * 20, 10 + rectangle.Top * 20, 10 + rectangle.Width * 20, 10 + rectangle.Height * 20);
						this.end = true;
						this.pictureBox1.Image = this.bitmap;
						this.pictureBox1.Update();
						Thread.Sleep(1000);
						if (MessageBox.Show("You won!!\nMatin thanks you for playing this game!!! :-)\nPlay again?", "You won!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							this.Form1_Load(null, null);
						}
						return;
					}
					this.pictureBox1.Image = this.bitmap;
					this.pictureBox1.Update();
					MiniMax.Play();
					MiniMax.board = MiniMax.BestPlay;
					rectangle = MiniMax.board.IsGameOver();
					this.Draw();
					if (!rectangle.IsEmpty)
					{
						Graphics.FromImage(this.bitmap).DrawLine(new Pen(Brushes.Red, 5f), 10 + rectangle.Left * 20, 10 + rectangle.Top * 20, 10 + rectangle.Width * 20, 10 + rectangle.Height * 20);
						this.pictureBox1.Image = this.bitmap;
						this.pictureBox1.Update();
						this.end = true;
						Thread.Sleep(1000);
						if (MessageBox.Show("You lost!\nPlay again?", "You lost!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							this.Form1_Load(null, null);
						}
						return;
					}
					this.pictureBox1.Image = this.bitmap;
				}
			}
		}
        private void button1_Click(object sender, EventArgs e)
        {
            string playerName = this.textBox1.Text;
            if (playerName != null)
            {
                label1.Text = playerName;
                this.textBox1.Visible = false;
                this.button1.Visible = false;
            }
        }



    }
}
