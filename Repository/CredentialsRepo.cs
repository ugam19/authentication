using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Repository
{
    public class CredentialsRepo: ICredentialsRepo
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CredentialsRepo));
        private Dictionary<string, string> ValidUsersDictionary = new Dictionary<string, string>()
        {
               {"pod1","pod1@123"},
               {"cog","cog@123"},
               {"int018","int018@123"},
               {"AAUB","AAUB@123"}
        };
        public Dictionary<string,string> GetCredentials()
        {
            try
            {
                _log4net.Info(nameof(GetCredentials)+ " invoked");
                Dictionary<string, string> userDictionary = new Dictionary<string, string>();
                userDictionary = ValidUsersDictionary;
                return userDictionary;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured " + e.Message + " from " + nameof(GetCredentials));
                return null;
            }
            
        }
    }
}
