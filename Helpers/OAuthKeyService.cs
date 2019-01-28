//  Copyright:   Copyright 2013 MYOB Technology Pty Ltd. All rights reserved.
//  Website:     http://www.myob.com
//  Author:      MYOB
//  E-mail:      info@myob.com
//
// Documentation, code and sample applications provided by MYOB Australia are for 
// information purposes only. MYOB Technology Pty Ltd and its suppliers make no 
// warranties, either express or implied, in this document. 
//
// Information in this document or code, including website references, is subject
// to change without notice. Unless otherwise noted, the example companies, 
// organisations, products, domain names, email addresses, people, places, and 
// events are fictitious. 
//
// The entire risk of the use of this documentation or code remains with the user. 
// Complying with all applicable copyright laws is the responsibility of the user. 
//
// Copyright 2013 MYOB Technology Pty Ltd. All rights reserved.

using System.IO;
using System.IO.IsolatedStorage;
using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;
using Newtonsoft.Json;

namespace Web.Helpers
{
    public class OAuthKeyService : IOAuthKeyService
    {
        private const string CsTokensFile = "Tokens.json";

        private OAuthTokens _tokens;

        /// <summary>
        /// On creation read any settings from file
        /// </summary>
        /// <remarks></remarks>
        public OAuthKeyService()
        {
            ReadFromFile();
        }

        #region IOAuthKeyService Members

        /// <summary>
        /// Implements the property for OAuthResponse which holdes theTokens
        /// </summary>
        /// <value>object containing OAuthTokens</value>
        /// <returns>Contracts.OAuthTokens</returns>
        /// <remarks>Saves to isolated storage when set</remarks>
        public OAuthTokens OAuthResponse
        {
            get { return _tokens; }
            set
            {
                _tokens = value;
                SaveToFile();
            }
        }

        #endregion

        /// <summary>
        /// Method to read Tokens from Isolated storage
        /// </summary>
        /// <remarks></remarks>
        private void ReadFromFile()
        {
            try
            {
                // Get an isolated store for user and application 
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(
                    IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

                var isoStream = new IsolatedStorageFileStream(CsTokensFile, FileMode.Open,
                                                              FileAccess.Read, FileShare.Read);

                var reader = new StreamReader(isoStream);
                // Read the data.

                _tokens = JsonConvert.DeserializeObject<OAuthTokens>(reader.ReadToEnd());
                reader.Close();

                isoStore.Dispose();
                isoStore.Close();
            }
            catch (FileNotFoundException)
            {
                // Expected exception if a file cannot be found. This indicates that we have a new user.
                _tokens = null;
            }
        }


        /// <summary>
        /// Method to save tokens to isolated storage
        /// </summary>
        /// <remarks></remarks>
        private void SaveToFile()
        {
            // Get an isolated store for user and application 
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(
                IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

            // Create a file
            var isoStream = new IsolatedStorageFileStream(CsTokensFile, FileMode.OpenOrCreate,
                                                          FileAccess.Write, isoStore);
            isoStream.SetLength(0);
            //Position to overwrite the old data.

            // Write tokens to file
            var writer = new StreamWriter(isoStream);
            writer.Write(JsonConvert.SerializeObject(_tokens));
            writer.Close();

            isoStore.Dispose();
            isoStore.Close();
        }
    }
}