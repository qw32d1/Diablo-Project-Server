/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：业务系统
*****************************************************/



using PEPortal;
using ServerRoot._01_Service;
using ServerRoot._04DB;
using System.Collections.Generic;

namespace ServerRoot._03Cache
{
    public class CacheSvc
    {
        public static CacheSvc instance = null;
        public static CacheSvc Instance
        {
            get
            {
                if (instance == null) { instance = new CacheSvc(); }
                return instance;
            }

        }
        public void Init()
        {

        }
        private Dictionary<string, ServersSession> OnLineAcctDic = new Dictionary<string, ServersSession>();
        private Dictionary<ServersSession, Playerdata> onlinesessiondlc = new Dictionary<ServersSession, Playerdata>();
        public bool IsActionLine(string act)
        {
            if (OnLineAcctDic.ContainsKey(act)) { return true; }
            else { return false; }

        }
        public Playerdata GetPlayerdata(string act, string passwd)
        {
            //TODO 
            //从数据库获取账号数据
            return DBMgt.Instance.QuaryPlayerData(act, passwd);

        }
        public void AcctOnline(string act, ServersSession session, Playerdata playerdata)
        {
            OnLineAcctDic.Add(act, session);
            onlinesessiondlc.Add(session, playerdata);
        }
        public void RemoveOnline(string act,ServersSession session, Playerdata playerdata)
        {
            OnLineAcctDic.Remove(act);
            onlinesessiondlc.Remove(session);

        }
        public bool IsExistsName(string tname)
        {
            return DBMgt.Instance.QuaryTheSameName(tname);
        }
        public ServersSession GetServerSession(string act)
        {
            if(OnLineAcctDic.TryGetValue(act,out ServersSession session))
            {

                return session;
            }
            else
            {
                return null;
            }
        }
        public Playerdata GetPlayerDataSession(ServersSession session)
        {
            if (onlinesessiondlc.TryGetValue(session, out Playerdata playerdata))
            { return playerdata; }
            else
            {
                return null;
            }
        }
        public void Clearofficename(ServersSession session)
        {
          
            foreach (var item in OnLineAcctDic)
            {
                if (item.Value == session)
                {
                    OnLineAcctDic.Remove(item.Key);
                    break;
                }
            }
            onlinesessiondlc.Remove(session);
        }
        public bool UpdataPlayerData(int id,Playerdata data)
        {
            return DBMgt.Instance.UpdatePlayerdata(id, data);
         
        }
        public Playerdata Register(Playerdata data,string acct ,string password)
        {
            return DBMgt.Instance.Registerisnew(data, acct, password);
        }
        public ReqLoginin GetPlayer_Act_Password(int id)
        {
            return DBMgt.Instance.GetAct_and_Password(id);
        }
    }

}
