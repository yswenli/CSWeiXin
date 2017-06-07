/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：cad53073-7474-4bb5-8e22-9482e085113a
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：WebClientUtil
 * 创建时间：2017/5/25 9:41:20
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSWeiXin.Util
{
    /// <summary>
    /// 自定义webclient
    /// </summary>
    public class WebClientUtil : WebClient
    {
        int _timeOut = 180 * 1000;

        /// <summary>
        /// 自定义webclient
        /// </summary>
        /// <param name="timeOut"></param>
        public WebClientUtil(int timeOut = 180 * 1000)
        {
            _timeOut = timeOut;
        }

        /// <summary>
        /// 重写后支持自解压
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            ServicePointManager.DefaultConnectionLimit = 512;
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.Timeout = request.ReadWriteTimeout = _timeOut;
            return request;
        }


        /// <summary>
        /// 将实体发送给远程服务器
        /// 发送json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Post(string url, Object obj)
        {
            return WebClientUtil.Post(url, SerializeUtil.Serialize(obj));
        }

        /// <summary>
        /// 发送实体到服务器并返回实体
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="url"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T2 Post<T1, T2>(string url, T1 t)
        {
            var json = WebClientUtil.Post(url, SerializeUtil.Serialize(t));
            if (string.IsNullOrEmpty(json))
            {
                return default(T2);
            }
            return SerializeUtil.Deserialize<T2>(json);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        /// <param name="headers"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string UploadFile(string url, string fileName, WebHeaderCollection headers = null, int timeOut = 180 * 1000)
        {
            using (WebClientUtil client = new WebClientUtil(timeOut))
            {
                ServicePointManager.DefaultConnectionLimit = 512;
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.Accept, "*/*");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
                client.Headers.Add("Accept-Encoding", "gzip");
                client.Headers.Add("ContentEncoding", "gzip");
                client.Headers.Add("Content-Type", "application/json");
                if (headers != null)
                {
                    foreach (var item in headers.AllKeys)
                    {
                        client.Headers.Add(item, headers[item]);
                    }
                }
                return System.Text.Encoding.UTF8.GetString(client.UploadFile(url, "POST", fileName));
            }
        }

        /// <summary>
        /// 将json发送给远程服务器
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="headers"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string Post(string url, string json, WebHeaderCollection headers = null, int timeOut = 180 * 1000)
        {
            using (WebClientUtil client = new WebClientUtil(timeOut))
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.Accept, "*/*");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
                client.Headers.Add("Accept-Encoding", "gzip");
                client.Headers.Add("ContentEncoding", "gzip");
                client.Headers.Add("Content-Type", "application/json");
                if (headers != null)
                {
                    foreach (var item in headers.AllKeys)
                    {
                        client.Headers.Add(item, headers[item]);
                    }
                }
                return client.UploadString(url, "POST", json);
            }
        }
        /// <summary>
        /// 上传数据到服务器
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static byte[] Post(string url, byte[] data, WebHeaderCollection headers = null, int timeOut = 180 * 1000)
        {
            using (WebClientUtil client = new WebClientUtil(timeOut))
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.Accept, "*/*");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
                client.Headers.Add("Accept-Encoding", "gzip");
                client.Headers.Add("ContentEncoding", "gzip");
                client.Headers.Add("Content-Type", "application/json");
                if (headers != null)
                {
                    foreach (var item in headers.AllKeys)
                    {
                        client.Headers.Add(item, headers[item]);
                    }
                }
                return client.UploadData(url, "POST", data);
            }
        }

        /// <summary>
        /// 获取远程服务器数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="headers"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string Get(string url, WebHeaderCollection headers = null, int timeOut = 180 * 1000)
        {
            using (WebClientUtil client = new WebClientUtil(timeOut))
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.Accept, "*/*");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
                client.Headers.Add("Accept-Encoding", "gzip");
                client.Headers.Add("ContentEncoding", "gzip");
                client.Headers.Add("Content-Type", "application/json");
                if (headers != null)
                {
                    foreach (var item in headers.AllKeys)
                    {
                        client.Headers.Add(item, headers[item]);
                    }
                }
                return client.DownloadString(url);
            }
        }


        public static string JsonDataPrex = "***json***";

        public static string GetResponseOnCookie(string url, string method, CookieContainer reqCookies, out CookieContainer resCookies, Dictionary<string, string> @params = null, string contentType = "application/x-www-form-urlencoded;charset=UTF-8", bool wx2 = false, int timeOut = 25000)
        {
            string result = string.Empty;
            resCookies = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "application/json, text/plain, */*";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.ContentType = contentType;
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version11;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.Timeout = timeOut;
                request.AllowAutoRedirect = true;
                request.Host = "login.wx.qq.com";
                request.Headers.Add("Origin", "https://wx.qq.com");
                request.Referer = "https://wx.qq.com/?&lang=zh_CN";
                if (wx2)
                {
                    request.Headers.Add("Origin", "https://wx2.qq.com");
                    request.Referer = "https://wx2.qq.com/?&lang=zh_CN";
                }
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.82 Safari/537.36";
                request.Headers.Add("Access-Control-Allow-Origin", "*");
                request.Headers.Add("Pragma", "no-cache");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("X-Cache-Lookup", "MISS from proxy:8080");
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

                if (reqCookies == null)
                {
                    request.CookieContainer = new CookieContainer();
                }
                else
                {
                    request.CookieContainer = reqCookies;
                }

                string cookiesstr = request.CookieContainer.GetCookieHeader(request.RequestUri);

                request.Method = method.ToUpper().Trim();

                ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;

                if (@params != null && @params.Any())
                {
                    string dataStr = string.Empty;

                    if (@params.Count == 1 && !string.IsNullOrEmpty(@params[JsonDataPrex]))
                    {
                        dataStr = @params[JsonDataPrex];
                    }
                    else
                    {
                        foreach (var item in @params)
                        {
                            if (string.IsNullOrEmpty(dataStr))
                                dataStr = item.Key + "=" + item.Value.ToString();
                            else
                                dataStr = "&" + item.Key + "=" + item.Value.ToString();
                        }
                    }

                    using (var dataStream = request.GetRequestStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(dataStr);
                        dataStream.Write(bytes, 0, bytes.Length);
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.Cookies != null && response.Cookies.Count > 0)
                        {
                            resCookies = new CookieContainer();
                            resCookies.Add(response.Cookies);
                        }
                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.Cookies != null && response.Cookies.Count > 0)
                        {
                            resCookies = new CookieContainer();
                            resCookies.Add(response.Cookies);
                        }

                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public static Stream GetResponseOnCookie(string url, string method, CookieContainer reqCookies, Dictionary<string, string> @params = null, string contentType = "application/x-www-form-urlencoded;charset=UTF-8", bool wx2 = false, int timeOut = 25000)
        {
            string result = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "application/json, text/plain, */*";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.ContentType = contentType;
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version11;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.Timeout = timeOut;
                request.AllowAutoRedirect = true;
                request.Host = "login.wx.qq.com";
                request.Headers.Add("Origin", "https://wx.qq.com");
                request.Referer = "https://wx.qq.com/?&lang=zh_CN";
                if (wx2)
                {
                    request.Headers.Add("Origin", "https://wx2.qq.com");
                    request.Referer = "https://wx2.qq.com/?&lang=zh_CN";
                }
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.82 Safari/537.36";
                request.Headers.Add("Access-Control-Allow-Origin", "*");
                request.Headers.Add("Pragma", "no-cache");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("X-Cache-Lookup", "MISS from proxy:8080");
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

                if (reqCookies == null)
                {
                    request.CookieContainer = new CookieContainer();
                }
                else
                {
                    request.CookieContainer = reqCookies;
                }

                string cookiesstr = request.CookieContainer.GetCookieHeader(request.RequestUri);

                request.Method = method.ToUpper().Trim();

                ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;

                if (@params != null && @params.Any())
                {
                    string dataStr = string.Empty;

                    if (@params.Count == 1 && !string.IsNullOrEmpty(@params[JsonDataPrex]))
                    {
                        dataStr = @params[JsonDataPrex];
                    }
                    else
                    {
                        foreach (var item in @params)
                        {
                            if (string.IsNullOrEmpty(dataStr))
                                dataStr = item.Key + "=" + item.Value.ToString();
                            else
                                dataStr = "&" + item.Key + "=" + item.Value.ToString();
                        }
                    }

                    using (var dataStream = request.GetRequestStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(dataStr);
                        dataStream.Write(bytes, 0, bytes.Length);
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        var ms = new MemoryStream();
                        response.GetResponseStream().CopyTo(ms);
                        return ms;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="mediaName"></param>
        /// <param name="uploadmediarequest"></param>
        /// <param name="webwx_data_ticket"></param>
        /// <param name="pass_ticket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string UploadMediaOnCookie(string url, string mediaName, string uploadmediarequest, string webwx_data_ticket, string pass_ticket, bool wx2 = false, int timeOut = 25000)
        {
            string result = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "*/*";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.ContentType = "multipart/form-data; boundary=----WebKitFormBoundarya4gGLw5MjIJx7nyC";
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version11;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.Timeout = timeOut;
                request.AllowAutoRedirect = true;
                request.Host = "file.wx.qq.com";
                request.Headers.Add("Origin", "https://wx.qq.com");
                request.Referer = "https://wx.qq.com/?&lang=zh_CN";
                if (wx2)
                {
                    request.Headers.Add("Origin", "https://wx2.qq.com");
                    request.Referer = "https://wx2.qq.com/?&lang=zh_CN";
                }
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.82 Safari/537.36";
                request.Headers.Add("Access-Control-Allow-Origin", "*");
                request.Headers.Add("Pragma", "no-cache");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("X-Cache-Lookup", "MISS from proxy:8080");
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
                request.Method = "POST";

                ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;

                using (var ms = new MemoryStream())
                {
                    StringBuilder sb = new StringBuilder();

                    var boundary = "----WebKitFormBoundarya4gGLw5MjIJx7nyC\r\n";

                    var lastBoundary = "----WebKitFormBoundarya4gGLw5MjIJx7nyC--\r\n";

                    var enterStr = "\r\n";

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"id\"\r\n\r\n");
                    sb.Append("WU_FILE_1" + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"name\"" + enterStr + enterStr);
                    sb.Append(mediaName.Substring(mediaName.LastIndexOf("\\") + 1) + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"type\"" + enterStr + enterStr);
                    sb.Append("image/jpeg\r\n");


                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"lastModifiedDate\"" + enterStr + enterStr);
                    sb.Append("Wed Dec 30 2015 15:24:14 GMT+0800 (中国标准时间)" + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"mediatype\"" + enterStr + enterStr);
                    sb.Append(new FileInfo(mediaName).Length + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"size\"\r\n\r\n");
                    sb.Append("doc" + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"uploadmediarequest\"\r\n\r\n");
                    sb.Append(uploadmediarequest + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"webwx_data_ticket\"\r\n\r\n");
                    sb.Append(webwx_data_ticket + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"pass_ticket\"\r\n\r\n");
                    sb.Append(pass_ticket + enterStr);

                    sb.Append(boundary);
                    sb.Append("Content-Disposition: form-data; name=\"filename\"; filename=\"" + mediaName.Substring(mediaName.LastIndexOf("\\") + 1) + "\"\r\n");
                    sb.Append("Content-Type: application/octet-stream\r\n\r\n");

                    var bytes1 = Encoding.UTF8.GetBytes(sb.ToString());
                    ms.Write(bytes1, 0, bytes1.Length);

                    sb.Clear();
                    sb = null;

                    var buffer = new byte[1024];
                    int offset = 0;
                    using (var fs = new FileStream(mediaName, FileMode.Open))
                    {
                        while ((offset = fs.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            ms.Write(buffer, 0, offset);
                        }
                    }

                    var bytes2 = Encoding.UTF8.GetBytes(lastBoundary);
                    ms.Write(bytes2, 0, bytes2.Length);

                    using (var dataStream = request.GetRequestStream())
                    {
                        var bytes3 = ms.ToArray();
                        dataStream.Write(bytes3, 0, bytes3.Length);
                    }
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        private static bool RemoteCertificateValidate(
          object sender, X509Certificate cert,
           X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
    }
}
