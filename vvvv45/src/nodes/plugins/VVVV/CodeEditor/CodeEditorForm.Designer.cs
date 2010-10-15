﻿
namespace VVVV.HDE.CodeEditor
{
	partial class CodeEditorForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.FSplitContainer = new System.Windows.Forms.SplitContainer();
			this.FErrorTableViewer = new VVVV.HDE.Viewer.WinFormsViewer.TableViewer();
			this.FImageList = new System.Windows.Forms.ImageList(this.components);
			this.FSplitContainer.Panel1.SuspendLayout();
			this.FSplitContainer.Panel2.SuspendLayout();
			this.FSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// FSplitContainer
			// 
			this.FSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.FSplitContainer.Name = "FSplitContainer";
			this.FSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// FSplitContainer.Panel2
			// 
			this.FSplitContainer.Panel2.Controls.Add(this.FErrorTableViewer);
			this.FSplitContainer.Panel2Collapsed = true;
			this.FSplitContainer.Size = new System.Drawing.Size(881, 476);
			this.FSplitContainer.SplitterDistance = 293;
			this.FSplitContainer.TabIndex = 1;
			// 
			// FErrorTableViewer
			// 
			this.FErrorTableViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FErrorTableViewer.Location = new System.Drawing.Point(0, 0);
			this.FErrorTableViewer.Name = "FErrorTableViewer";
			this.FErrorTableViewer.RowHeight = 16;
			this.FErrorTableViewer.Size = new System.Drawing.Size(150, 46);
			this.FErrorTableViewer.TabIndex = 0;
			this.FErrorTableViewer.DoubleClick += new VVVV.HDE.Viewer.WinFormsViewer.ClickHandler(this.FErrorTableViewerDoubleClick);
			// 
			// FImageList
			// 
			this.FImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.FImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.FImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CodeEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Silver;
			this.ClientSize = new System.Drawing.Size(881, 476);
			this.ControlBox = false;
			this.Controls.Add(this.FSplitContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CodeEditorForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "CodeEditorForm";
			this.TopMost = true;
			this.FSplitContainer.Panel1.ResumeLayout(false);
			this.FSplitContainer.Panel2.ResumeLayout(false);
			this.FSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private VVVV.HDE.Viewer.WinFormsViewer.TableViewer FErrorTableViewer;
		private System.Windows.Forms.ImageList FImageList;
		private System.Windows.Forms.SplitContainer FSplitContainer;
		

	}
}
