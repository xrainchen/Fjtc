﻿using System.Collections.Concurrent;
using Fjtc.DAL.MpWeiXin;
using Fjtc.Model.Entity;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinAccessSettingBll
    {
        private readonly MpWeiXinAccessSettingDal _mpWeiXinAccessSettingDal = new MpWeiXinAccessSettingDal();
        private static readonly ConcurrentDictionary<string, MpWeiXinAccessSetting> AccessSettingDic = new ConcurrentDictionary<string, MpWeiXinAccessSetting>();

        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(long userId)
        {
            return _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSetting(userId);
        }

        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(string userName)
        {
            return AccessSettingDic.GetOrAdd(userName, x => _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSetting(userName));
        }

        public MpWeiXinAccessSetting GetMpWeiXinAccessSettingByHost(string host, bool isCace = true)
        {
            if (isCace)
            {
                if (!AccessSettingDic.ContainsKey(host) || AccessSettingDic[host] == null)
                {
                    AccessSettingDic.AddOrUpdate(
                        host,
                        x => _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSettingByHost(host),
                        (x, setting) => _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSettingByHost(host)
                        );
                }
            }
            else
            {
                AccessSettingDic.AddOrUpdate(
                        host,
                        x => _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSettingByHost(host),
                        (x, setting) => _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSettingByHost(host)
                        );
            }
            return AccessSettingDic.GetOrAdd(host,
                   x =>
                       _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSettingByHost(host));
        }
        public bool AddOrUpdate(MpWeiXinAccessSetting entity)
        {
            if (_mpWeiXinAccessSettingDal.AddOrUpdate(entity))
            {
                var user = new ProductUserBLL().Get(entity.UserId);
                AccessSettingDic.TryRemove(user.BindHost, out entity);
                return true;
            }
            return false;
        }
    }
}
