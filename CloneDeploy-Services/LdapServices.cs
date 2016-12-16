﻿using System;
using System.DirectoryServices;
using CloneDeploy_Services.Helpers;
using log4net;

namespace CloneDeploy_Services
{
    /// <summary>
    /// Summary description for Ldap
    /// </summary>
    public class LdapServices
    {
        private readonly ILog log = LogManager.GetLogger("ApplicationLog");
        public LdapServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool Authenticate(string username, string pwd, string ldapGroup=null)
        {
            if (Settings.LdapEnabled != "1") return false;

            string path = "LDAP://" + Settings.LdapServer + ":" + Settings.LdapPort + "/" + Settings.LdapBaseDN;
            string _filterAttribute = null;
           
            DirectoryEntry entry = new DirectoryEntry(path,username,pwd);

            if(Settings.LdapAuthType == "Basic")
                entry.AuthenticationType = AuthenticationTypes.None;
            else if (Settings.LdapAuthType == "Secure")
                entry.AuthenticationType = AuthenticationTypes.Secure;
            else if (Settings.LdapAuthType == "SSL")
                entry.AuthenticationType = AuthenticationTypes.SecureSocketsLayer;
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(" + Settings.LdapAuthAttribute + "=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("memberOf");
             
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }

                // Update the new path to the user in the directory
                path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                log.Debug("Could Not Authenticate User: " + username + " " + ex.Message);
                return false;
            }

            if (ldapGroup != null)
            {
                return GetGroups(_filterAttribute, path,ldapGroup);
            }
            else
                return true;
        }



        public bool GetGroups(string _filterAttribute, string _path, string ldapGroup)
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");        
            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount;
                     propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return false;
                    }
                    if (String.Equals(ldapGroup, dn.Substring((equalsIndex + 1),
                        (commaIndex - equalsIndex) - 1), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug("Error obtaining group names. " + ex.Message);
                return false;
            }
            return false;
        }
    }
}