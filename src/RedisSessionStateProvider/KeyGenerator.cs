﻿//
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
//

using System;

namespace Microsoft.Web.Redis
{
    internal class KeyGenerator
    {
        private string id;
        public string DataKey { get; private set; }
        public string LockKey { get; private set; }
        public string InternalKey { get; private set; }

		public string DataKeyV1 { get; private set; }
		public string LockKeyV1 { get; private set; }
		public string InternalKeyV1 { get; private set; }

		private void GenerateKeys(string id, string app)
        {
            this.id = id;

			DataKey = $"{{{app}_{id}}}_SessionStateItemCollection";
			LockKey = $"{{{app}_{id}}}_WriteLock";
			InternalKey = $"{{{app}_{id}}}_SessionTimeout";

			DataKeyV1 = "{" + app + "_" + id + "}_Data";
            LockKeyV1 = "{" + app + "_" + id + "}_Write_Lock";
            InternalKeyV1 = "{" + app + "_" + id + "}_Internal";
        }

        public KeyGenerator(string sessionId, string applicationName)
        {
            GenerateKeys(sessionId, applicationName);
        }

        public void RegenerateKeyStringIfIdModified(string sessionId, string applicationName)
        {
            if (!sessionId.Equals(this.id))
            {
                GenerateKeys(sessionId, applicationName);
            }
        }
    }
}