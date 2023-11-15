
using PENet;
using PEPortal;

namespace ServerRoot._01_Service
{
   public class ServersSession:PESession<GameMsg>
    {
       public  int ServerId = 0;
        protected override void OnConnected()
        {
            ServerId = Server_Root.instance.ServerId();
           PECommon.log("建立连接"+"Server_ID"+ServerId);
        }

        protected override void OnReciveMsg(GameMsg msg)
        {
           PECommon.log("Server_ID" + ServerId+"收到" +msg.cmd.ToString() );
           NetSvc.instance.AddMsgQue(new Msgpack(this,msg));
        }

        protected override void OnDisConnected()
        {
            LoginSvc.Instance.RsPlayerQuit(this);
            PECommon.log("Server_ID" + ServerId+"断开连接");

        }
    }
}
