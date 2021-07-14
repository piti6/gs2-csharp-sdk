/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2News.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2News.Request
{
	[Preserve]
	[System.Serializable]
	public class WantGrantByUserIdRequest : Gs2Request<WantGrantByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }

        public WantGrantByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public WantGrantByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }

    	[Preserve]
        public static WantGrantByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new WantGrantByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}