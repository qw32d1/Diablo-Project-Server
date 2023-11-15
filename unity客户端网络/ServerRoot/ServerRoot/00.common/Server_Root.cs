/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：服务器初始化
*****************************************************/


using ServerRoot._02_System.GuideSvc;
using ServerRoot._03Cache;
using ServerRoot._04DB;

public class Server_Root
{
    public static Server_Root instance=null;
    public static Server_Root Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Server_Root();
            } 
            return instance; }

    }
    public void Init()
    {
        //数据库
        DBMgt.Instance.Init();
        //服务层
        CacheSvc.Instance.Init();
        NetSvc.Instance.Init(); 
        //业务系统层
        LoginSvc.Instance.Init();   
        //导航模块
        GuideSvs.Instance.Init();
            
    }
    public void Update()
    {
        NetSvc.Instance.Updata();
    }
    private int serverid = 0;
    public int ServerId()
    {
        serverid += 1;
        if (serverid == int.MaxValue)
        {
            serverid = 0;
        }
        return serverid;
    }
}
