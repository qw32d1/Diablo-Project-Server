
/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：客户端服务器共用工具类
*****************************************************/
using PENet;

public enum logtype
{
    log=0,
    warn=1,
    error=2,
    info=3
}
namespace PEPortal
{
   public  class PECommon
    {
        public static void log(string msg,logtype o=logtype.log)
        {
            LogLevel lv=(LogLevel)o;
            PETool.LogMsg(msg,lv);
        }
        public static int GetFightNumber(Playerdata data)
        {
            return data.Lv*100+data.ad+data.ap+data.addef+data.apdef;
        }
        public static int GetpowerLimt(int lv)
        {
            return ((lv - 1) / 10) * 150 + 150;
        }
        public static int GetExpLv(int lv)
        {
            return 100 * lv * lv;
        }
    }
}
