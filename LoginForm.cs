using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CCWin;
using CCWin.SkinControl;
using CSWeiXin.Util;
using CSWeiXin.Weixin;

namespace CSWeiXin
{
    public partial class LoginForm : CCWin.Skin_Mac
    {
        MainForm mainForm = null;

        public bool ValidationFailure
        {
            get;
            set;
        } = false;

        public LoginForm()
        {
            InitializeComponent();
            mainForm = new MainForm(this);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (!ValidationFailure && LoginHelper.InitWithData())
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            mainForm.Show();
                            this.Hide();
                            return;
                        }
                        catch { }
                    }));
                }
                this.BeginInvoke(new Action(OnFailed));
            });
        }

        private void OnFailed()
        {
            skinAnimator1.WaitAllAnimations();
            skinAnimator1.ShowSync(label1, true, Animation.HorizBlind);

            var uuid = LoginHelper.GetUUID();
            var image = LoginHelper.GetQR(uuid);
            new CalcTimeUtil().Start(25, OnChanged);
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.BeginInvoke(new Action(() =>
                {
                    pictureBox1.Image = image;
                    skinAnimator1.HideSync(pictureBox1, true, Animation.Particles);
                    skinAnimator1.WaitAllAnimations();
                    skinAnimator1.ShowSync(pictureBox1, true, Animation.Mosaic);
                }));
            }
            else
            {
                pictureBox1.Image = LoginHelper.GetQR(uuid);
                skinAnimator1.HideSync(pictureBox1, true, Animation.Particles);
                skinAnimator1.WaitAllAnimations();
                skinAnimator1.ShowSync(pictureBox1, true, Animation.Mosaic);
            }

            LoginHelper.GetLogin(uuid, OnFailed, OnSuccessed);
        }

        private void OnSuccessed()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.Hide();
                this.ValidationFailure = false;
                if (mainForm == null || mainForm.IsDisposed)
                {
                    mainForm = new MainForm(this);
                }
                mainForm.Show();
            }));

        }


        private void OnChanged(int seconds)
        {
            label2.BeginInvoke(new Action(() =>
            {
                label2.Text = seconds.ToString();
            }));
        }
    }
}
