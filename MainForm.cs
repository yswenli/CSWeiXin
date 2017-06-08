/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：37431ae9-ec1f-4f81-83fd-c9931fe0054d
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin
 * 类名称：MainForm
 * 创建时间：2017/5/27 14:37:08
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin.SkinControl;
using CSWeiXin.Controls;
using CSWeiXin.Util;
using CSWeiXin.Weixin;
using GFF.Component.Capture;

namespace CSWeiXin
{
    public partial class MainForm : CCWin.Skin_Mac
    {
        bool isAutoClose = false;

        LoginForm LoginForm = null;

        bool isActived = false;

        string choose = "@@b216d3a9e025a4f203ae688561f84349c3736c7be37aa604a12cabfefba53766";


        bool isInit = false;

        public bool TokenExpired
        {
            get;
            set;
        }

        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();

            LoginForm = loginForm;

            this.Activated += MainForm_Activated;
            this.Deactivate += MainForm_Deactivate;
            this.SizeChanged += MainForm_SizeChanged;

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            MessageHelper.OnMessage += MessageHelper_OnMessage;
            MessageHelper.OnTokenFailed += MessageHelper_OnTokenFailed;

            ValidateToken();
        }



        #region 私用方法
        private void InitAsync()
        {
            Task.Factory.StartNew(() =>
            {
                this.TokenExpired = !InitHelper.InitWidthLogin();

                isInit = true;

                this.Invoke(new Action(() =>
                {
                    chatListBox1.Items.Clear();
                }));

                if (!this.TokenExpired)
                {
                    Task.Factory.StartNew(() =>
                    {
                        InitHelper.GetContactList();

                        this.BeginInvoke(new Action(() =>
                        {
                            label1.Text = InitHelper.WebWeixinInit.User.NickName;

                            label3.Text = InitHelper.WebWeixinInit.User.Signature;
                        }));

                        this.SetImageAsync(pictureBox1, InitHelper.WebWeixinInit.User.HeadImgUrl);

                        this.ShowSessionList();

                        this.ShowContactList();

                        Loaded();
                    });
                }
            });
        }


        void ValidateToken()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (isInit && this.TokenExpired)
                    {
                        MessageBox.Show("登录信息已过期，请重新扫码！", "CSWeixin — developer:wenli 2017");
                        this.BeginInvoke(new Action(() =>
                        {
                            LoginForm.ValidationFailure = true;
                            LoginForm.Show();
                            isAutoClose = true;
                            this.Close();
                        }));
                        break;
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void Loading()
        {
            gifBox1.Image = Properties.Resources.loadubg;
            gifBox1.Width = 329;
            gifBox1.Height = 300;
            var left = (this.Width - gifBox1.Width) / 2;
            var top = (this.Height - gifBox1.Height) / 2;
            gifBox1.Location = new Point(left, top);
            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }
            gifBox1.Visible = true;
        }

        private void Loaded()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    Loaded();
                }));
            }
            else
            {
                foreach (Control c in this.Controls)
                {
                    c.Visible = true;
                }
                gifBox1.Visible = false;
            }
        }



        private void ShowSessionList()
        {
            ChatListItem item = new ChatListItem("会话列表");
            item.IsOpen = true;

            foreach (var contact in InitHelper.WebWeixinInit.ContactList)
            {
                ChatListSubItem sitem = new ChatListSubItem()
                {
                    NicName = contact.UserName,
                    DisplayName = contact.NickName,
                    PersonalMsg = contact.Signature
                };
                InitHelper.SetImageAsync(sitem, contact.HeadImgUrl);
                item.SubItems.Add(sitem);
            }
            chatListBox1.BeginInvoke(new Action(() =>
            {
                chatListBox1.Items.Add(item);
            }));
        }

        private void ShowContactList()
        {
            ChatListItem item = new ChatListItem("联系人");

            foreach (var contact in InitHelper.BatchGetContact.MemberList)
            {
                ChatListSubItem sitem = new ChatListSubItem()
                {
                    NicName = contact.UserName,
                    DisplayName = contact.NickName,
                    PersonalMsg = contact.Signature
                };
                InitHelper.SetImageAsync(sitem, contact.HeadImgUrl);
                item.SubItems.Add(sitem);
            }
            chatListBox1.BeginInvoke(new Action(() =>
            {
                chatListBox1.Items.Add(item);
            }));
        }


        private void MessageHelper_OnMessage(IList<Models.AddMsgList> msgs)
        {
            try
            {
                string msgStr = string.Empty;

                if (msgs != null && msgs.Count > 0)
                {
                    if (!isActived)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            Win32Util.FlashWindow(this.Handle, true);
                        }));
                    }

                    foreach (var item in msgs)
                    {
                        if (!string.IsNullOrEmpty(item.Content))
                        {
                            var nickName = string.Empty;

                            var user = InitHelper.BatchGetContact.MemberList.Where(b => b.UserName == item.FromUserName).FirstOrDefault();

                            if (user == null)
                            {
                                var suser = InitHelper.WebWeixinInit.ContactList.Where(b => b.UserName == item.FromUserName).FirstOrDefault();
                                if (suser != null)
                                {
                                    nickName = suser.NickName;
                                }
                            }
                            else
                            {
                                nickName = user.NickName;
                            }
                            if (user.UserName == item.FromUserName)
                            {
                                displayer.AppendMsg(nickName, item.Content);
                            }
                            MessageUtil.Set(item.FromUserName, Displayer.GenerateMsgHtml(nickName, item.Content));

                        }
                    }
                    displayer.GoBottom();
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void MessageHelper_OnTokenFailed()
        {
            this.TokenExpired = true;
        }
        #endregion

        #region 控件事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Loading();
            this.InitAsync();
        }

        private void chatListBox1_ClickSubItem(object sender, ChatListClickEventArgs e, MouseEventArgs es)
        {
            if (choose != e.SelectSubItem.NicName)
            {
                choose = e.SelectSubItem.NicName;

                displayer.Html = MessageUtil.Get(choose);
                displayer.GoBottom();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msgStr = textBox3.Text;

            if (!string.IsNullOrEmpty(msgStr))
            {
                textBox3.Text = "";

                Task.Factory.StartNew(() =>
                {
                    MessageHelper.SendMsg(InitHelper.WebWeixinInit.User.UserName, choose, msgStr);
                });

                MessageUtil.Set(choose, displayer.AppendMyMsg(InitHelper.WebWeixinInit.User.NickName, msgStr));

                displayer.GoBottom();
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isAutoClose)
            {
                if (MessageBox.Show("确定要退出么？", "CSWeixin — developer:wenli 2017", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Environment.Exit(-1);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var mediaName = openFileDialog1.FileName;

                Task.Factory.StartNew(() =>
                {
                    MessageHelper.UploadMedia(InitHelper.WebWeixinInit.User.UserName, choose, mediaName);
                });
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Loading();
            this.InitAsync();
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(null, null);
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            isActived = true;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            isActived = false;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                isActived = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var captureForm = new CaptureForm();
            captureForm.OnCaptured += CaptureForm_OnCaptured;
            captureForm.Show();
        }

        private void CaptureForm_OnCaptured(Image imgDatas)
        {
            var mediaName = Environment.CurrentDirectory + "\\" + Guid.NewGuid().ToString("N") + ".png";

            imgDatas.Save(mediaName);

            Task.Factory.StartNew(() =>
            {
                MessageHelper.UploadMedia(InitHelper.WebWeixinInit.User.UserName, choose, mediaName);
            });
        }
        #endregion




    }
}
