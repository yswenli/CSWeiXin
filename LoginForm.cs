using System;
using System.Threading.Tasks;
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
            Task.Factory.StartNew(() =>
            {
                skinAnimator1.WaitAllAnimations();

                skinAnimator1.ShowSync(label1, true, Animation.HorizBlind);

                var uuid = LoginHelper.GetUUID();
                var image = LoginHelper.GetQR(uuid);
                new CalcTimeUtil().Start(25, OnChanged);

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            pictureBox1.Image = image;
                            skinAnimator1.HideSync(pictureBox1, true, Animation.Particles);
                            skinAnimator1.WaitAllAnimations();
                            skinAnimator1.ShowSync(pictureBox1, true, Animation.Mosaic);
                        }
                        catch (Exception ex)
                        {
                            LogUtil.WriteLog("OnFailed", ex);
                        }
                    }));
                }
                else
                {
                    try
                    {
                        pictureBox1.Image = image;
                        skinAnimator1.HideSync(pictureBox1, true, Animation.Particles);
                        skinAnimator1.WaitAllAnimations();
                        skinAnimator1.ShowSync(pictureBox1, true, Animation.Mosaic);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.WriteLog("OnFailed", ex);
                    }
                }
                LoginHelper.Login(uuid, OnFailed, OnSuccessed);
            });
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
            this.BeginInvoke(new Action(() =>
            {
                label2.Text = seconds.ToString();
            }));
        }
    }
}
