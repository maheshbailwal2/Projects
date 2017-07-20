﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.IO;
using System.Threading;

using FileSystem;

using UserActivityLogger;
using Core;
using EventPublisher;
using ActivityLogger;

namespace RecordSession
{
    public partial class frmPictureViewer : Form
    {
        private int currentImageIndex = 0;
        public Action<int> DisplayChange;
        public int incrementCount = 1;
        IActivityRepositary _activityRepositary;
        IActivityReader _activityReader;
        public Action<string> OnCommentsFetched;
        public Action<int> OnIndexChanged;

        public bool FastMode { get; set; }

       
        public int Index
        {
            get
            {
                return currentImageIndex;
            }

            set
            {
                currentImageIndex = value;
            }
        }

        public frmPictureViewer()
        {
            _activityRepositary = new ActivityRepositary(new JarFileFactory(), new ImageCommentEmbedder(), new ActivityReaderFactory(new JarFileFactory()));
            InitializeComponent();
            EventContainer.SubscribeEvent(RecordSession.Events.CloseCurrentSession.ToString(), OnCloseCurrentSession);
        }

        public void ChangeNextImagePostion(int index)
        {
            timer2.Enabled = false;
            _activityReader.ChangePostion(index);
            timer2.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            WindowState = FormWindowState.Normal;
        }

        public int Play(IEnumerable<string> files)
        {
            if (timer2.Enabled)
                timer2.Enabled = false;
            else
            {
                timer2.Enabled = true;

             
                if (_activityReader != null)
                {
                    _activityReader.Dispose();
                }

                _activityReader = _activityRepositary.GetReader(files);
                return _activityReader.FileCount();
            }

            return 0;
        }
        public void MinimizeWindow()
        {
            WindowState = FormWindowState.Minimized;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (!_activityReader.GetEnumerator().MoveNext())
            {
                timer2.Enabled = false;
                //_activityReader.Dispose();
                return;
            }

            
            var activity = _activityReader.GetEnumerator().Current;
            pictureBox1.Image = activity.ScreenShot;


            if (!FastMode)
            {
                EventContainer.PublishEvent(RecordSession.Events.OnCommentsFetched.ToString(), new EventArg(Guid.NewGuid(), activity.KeyPressedData));
            }

            Index += incrementCount;
         
            if (Index <= 0)
            {
                timer2.Enabled = false;
                EventContainer.PublishEvent(RecordSession.Events.VideoPaused.ToString(), new EventArg(Guid.NewGuid(), e));
                //_activityReader.Dispose();
                return;
            }

            DisplayChange(Index);


            if (incrementCount == -1)
            {
                ChangeNextImagePostion(Index);
            }

            Text = "Playing " + Index.ToString();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        private void GetComments(Stream stream)
        {
            if (stream == null)
                return;

            var comments = new ImageCommentEmbedder().GetComments(stream);

            OnCommentsFetched?.Invoke(comments);
        }

        private void frmPictureViewer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 39 || e.KeyValue == 40)
                {
                    Index++;
                    OnIndexChanged(Index);
                }
                if (e.KeyValue == 37 || e.KeyValue == 38)
                {
                    if (Index < 1)
                        return;
                    Index--;
                    OnIndexChanged(Index);
                }

                if (e.KeyCode == Keys.Space)
                {
                    TogglePausePlay();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    MinimizeWindow();
                }
            }

            catch { }
        }

        private void frmPictureViewer_MaximumSizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

        }

        private void frmPictureViewer_Resize(object sender, EventArgs e)
        {
            if (MdiParent.WindowState == FormWindowState.Maximized)
            {
                pictureBox1.Dock = DockStyle.None;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            else
            {
                pictureBox1.Dock = DockStyle.Fill;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TogglePausePlay();
        }

        private void TogglePausePlay()
        {
            timer2.Enabled = !timer2.Enabled;


            if (timer2.Enabled)
            {
                Text = "Playing" + Index.ToString();
            }
            else
            {
                Text = "Paused " + Index.ToString();
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            EventContainer.PublishEvent(RecordSession.Events.OnPictureViwerResize.ToString(), new EventArg(Guid.NewGuid(), e));
        }

        private void OnCloseCurrentSession(EventArg eventArg)
        {
            timer2.Enabled = false;
            if (_activityReader != null)
            {
                _activityReader.Dispose();
            }
            Close();
            Dispose();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            EventContainer.PublishEvent(RecordSession.Events.ShowVideoControlBox.ToString(), new EventArg(Guid.NewGuid(), e));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
