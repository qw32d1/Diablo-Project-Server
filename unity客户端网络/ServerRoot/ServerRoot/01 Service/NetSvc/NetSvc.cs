
/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：网络服务
*****************************************************/
using PENet;
using PEPortal;
using ServerRoot._01_Service;
using System.Collections.Generic;
public class Msgpack
{
    public ServersSession Session;
    public GameMsg msg;
    public  Msgpack(ServersSession session,GameMsg Msg) {
    this.Session = session;
    this.msg = Msg;}
}
public  class NetSvc
    {
    public static NetSvc instance = null;
    public  static NetSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetSvc();
            }
            return instance;
        }

    }
    public readonly string obj = "lock";
    public Queue<Msgpack >msgpackque=new Queue<Msgpack> ();
    public void Init()
    {
        PESocket<ServersSession, GameMsg> server = new PESocket<ServersSession, GameMsg>();
        server.StartAsServer(IpCfg.srvip, IpCfg.srvport);
        PECommon.log("正常，已经进入");
       
    }
    public void AddMsgQue(Msgpack msg)
    {
        lock (obj)
        {
            msgpackque.Enqueue(msg);
        }
    }
    public void Updata()
    {
        lock (obj) 
        {
            if(msgpackque.Count > 0)
            {
                Msgpack msg = msgpackque.Dequeue();
                HandOutMsg(msg);
            }
        }
    }
    private void HandOutMsg(Msgpack Msg)
    {
        switch((CMD)Msg.msg.cmd)
        {
            case CMD.RqLogin:
                LoginSvc.instance.ReqLogin(Msg);
                break;
            case CMD.RsName:
                LoginSvc.instance.Rsname(Msg);
                break;
            case CMD.Register:
                LoginSvc.instance.Register(Msg);
                break;
         
               
        }
    }
}
