/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：业务系统
*****************************************************/



using PEPortal;
using ServerRoot._01_Service;
using ServerRoot._03Cache;



public class LoginSvc
{
    public static LoginSvc instance = null;
    public static LoginSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoginSvc();
            }
            return instance;
        }

    }
    public CacheSvc Cache = null;
    public readonly string obj = "lock";
    public void Init()
    {
        Cache = CacheSvc.Instance;
    }
    public void ReqLogin(Msgpack gamemsg)
    {
        ReqLoginin data = gamemsg.msg.reloginin;
        gamemsg.msg = new GameMsg() { cmd = (int)CMD.RsLogin, reloginin = new ReqLoginin { } };
        if (Cache.IsActionLine(data.Act))
        {
            //判断是否账号已经上线
            gamemsg.msg.err = (int)ErrorCode.AcctOnLine;
        }
        else
        {

            Playerdata playerData = Cache.GetPlayerdata(data.Act, data.Passwd);
            if (playerData == null)
            {
                gamemsg.msg.err = (int)ErrorCode.WordPasswd;

            }
            else
            {
                gamemsg.msg.rsqloginin = new RspLogin() { playerdata = playerData };
                Cache.AcctOnline(data.Act, gamemsg.Session, playerData);
            }
        }

        gamemsg.Session.SendMsg(gamemsg.msg);

    }
    public void Rsname(Msgpack pack)
    {
        Resname rqname = pack.msg.resname;
        GameMsg msg = new GameMsg() { cmd = (int)CMD.RqName };
        //判断名字是否存在

        if (CacheSvc.Instance.IsExistsName(rqname.rsname))
        {
            msg.err = (int)ErrorCode.NameExists;
        }
        else
        {

            Playerdata player = CacheSvc.Instance.GetPlayerDataSession(pack.Session);
            player.name = rqname.rsname;
            if (!CacheSvc.Instance.UpdataPlayerData(player.id, player))
            {
                msg.err = (int)ErrorCode.UpdataDBError;
            }
            else
            {

                msg.rsqname = new Rqname() { rqname = player.name };

            }

        }


        pack.Session.SendMsg(msg);
    }
    public void Register(Msgpack pack)
    {
        ReqLoginin rspLogin = pack.msg.reloginin;
        GameMsg msg = new GameMsg { cmd = (int)CMD.RegisterEnter };
        if (rspLogin != null)
        {

            Playerdata playerdata = Cache.GetPlayerdata(rspLogin.Act, rspLogin.Passwd);
            if (playerdata != null)
            {
                msg.err = (int)ErrorCode.AccountExist;
            }
            else
            {
                playerdata = new Playerdata();
                msg.rsqloginin = new RspLogin() { playerdata = Cache.Register(playerdata, rspLogin.Act, rspLogin.Passwd) };
            }


        }



        pack.Session.SendMsg(msg);

    }
    public void RsPlayerQuit(ServersSession session)
    {
       Cache.Clearofficename(session);
    }
}

