using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Tools.Services.Models;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Tools.Services
{
    /// <summary>
    /// 网站抓取服务
    /// </summary>
    public class SpiderService
    {
        /// <summary>
        /// 百度权重接口
        /// </summary>
        private static string BAIDUPRZZAPI = "http://www.link114.cn/get.php?baiduprzz&{0}&1259";

        /// <summary>
        ///  获取网址HTML
        /// </summary>
        /// <param name="URL">网址 </param>
        /// <returns> </returns>
        public static ResultModel<string> GetHtml(string URL)
        {
            ResultModel<string> resultModel = new ResultModel<string>();
            resultModel.Result = true;
            HttpWebRequest wrt;

            try
            {
                wrt = (HttpWebRequest)WebRequest.Create(URL);
                wrt.Timeout =8 * 1000;//8秒超时
                wrt.Method = "GET"; //请求方法
                wrt.Accept = "text/html"; //接受的内容
                wrt.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)"; ; //用户代理
                wrt.Credentials = CredentialCache.DefaultCredentials;
                WebResponse wrp = (HttpWebResponse)wrt.GetResponse();


                string acceptEncoding = wrp.Headers["Content-Encoding"] ?? ""; //获得压缩格式
                byte[] bytes;
                if (acceptEncoding.Contains("gzip"))
                {
                    using (GZipStream gzip = new GZipStream(wrp.GetResponseStream(), CompressionMode.Decompress)
                        )
                    {
                        bytes = StreamToBytes(gzip);
                    }
                }
                else
                {
                    bytes = StreamToBytes(wrp.GetResponseStream());
                }


                var stream = new StreamReader(BytesToStream(bytes), Encoding.GetEncoding("utf-8"));
                string htmlstring = stream.ReadToEnd();
                var encodingString = GetEncoding(htmlstring);

                //判断不是utf8不转码
                if (encodingString.ToLower() != "utf-8")
                {
                    htmlstring = new StreamReader(BytesToStream(bytes), Encoding.GetEncoding(encodingString)).ReadToEnd();
                }
                wrt.GetResponse().Close();
                //删除脚本
                resultModel.Data = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                if (string.IsNullOrWhiteSpace(resultModel.Data))
                {
                    resultModel.Result = false;
                    resultModel.Message = "无网页内容!";
                }
            }
            catch (Exception ex)
            {
                resultModel.Result = false;
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        /// <summary>
        /// 转化为字节
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            List<byte> bytes = new List<byte>();
            int temp = stream.ReadByte();
            while (temp != -1)
            {
                bytes.Add((byte)temp);
                temp = stream.ReadByte();
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// 转化为流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        /// <summary>
        /// 读取网页编码
        /// </summary>
        /// <returns></returns>
        private static string GetEncoding(string Html)
        {
            try
            {
                var reg = new Regex(@"<meta.+?charset=[^\w]?([-\w]+)", RegexOptions.IgnoreCase);
                string encodingStirng = reg.Match(Html).Groups[1].Value;
                return encodingStirng;
            }
            catch (Exception ex)
            {
            }
            return "gb18030";
        }

        /// <summary>
        /// 获取网站域名
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string GetHost(string Url)
        {
            try
            {
                var reg = new Regex(@"https?://(.*?)($|/)", RegexOptions.IgnoreCase);
                string host = reg.Match(Url).Value;
                if (!string.IsNullOrWhiteSpace(host))
                    host = host.TrimEnd('/');
                return host;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        /// <summary>
        /// 获取网站标题
        /// </summary>
        /// <param name="Html"></param>
        /// <returns></returns>
        public static string GetTitle(string Html)
        {
            var reg = new Regex(@"<title[^>]*?>(.*?)<\/title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return reg.Match(Html).Groups[1].Value;
        }

        /// <summary>
        /// 获取网页中的所有链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static ResultModel<List<string>> GetLinks(string html)
        {
            ResultModel<List<string>> resultModel = new ResultModel<List<string>>();
            resultModel.Result = true;
            resultModel.Data = new List<string>();
            try
            {
                const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection m = r.Matches(html);
                if (m != null)
                    foreach (var link in m)
                    {
                        if (UrlAvailable(link.ToString()))
                            resultModel.Data.Add(link.ToString().Trim().TrimEnd('/'));
                    }
            }
            catch (Exception exception)
            {
                resultModel.Result = false;
                resultModel.Message = exception.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 获取权重
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static int GetWeights(string url)
        {
            int weights = -1;
            WebClient client = new WebClient();
            try
            {
                var html = client.DownloadString(string.Format(BAIDUPRZZAPI, url));
                weights = Int32.Parse(html);
            }
            catch (Exception exception)
            {
            }
            finally
            {
                client.Dispose();
            }
            return weights;
        }

        /// <summary>
        /// 判断链接是否合法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool UrlAvailable(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url.Contains(".jpg") || url.Contains(".gif") || url.Contains(".gif")
                || url.Contains(".png") || url.Contains(".css") || url.Contains(".txt") || url.Contains(".bng")
                || url.Contains(".js") || url.Contains(".ioc") || url.Contains(".swf") || url.Contains(".zip"))
            {
                return false;
            }
            return true;
        }



    }
}
