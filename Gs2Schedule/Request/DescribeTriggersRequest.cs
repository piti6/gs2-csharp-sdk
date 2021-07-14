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
using Gs2.Gs2Schedule.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Schedule.Request
{
	[Preserve]
	[System.Serializable]
	public class DescribeTriggersRequest : Gs2Request<DescribeTriggersRequest>
	{
        public string NamespaceName { set; get; }
        public string AccessToken { set; get; }
        public string PageToken { set; get; }
        public int? Limit { set; get; }

        public DescribeTriggersRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public DescribeTriggersRequest WithAccessToken(string accessToken) {
            this.AccessToken = accessToken;
            return this;
        }

        public DescribeTriggersRequest WithPageToken(string pageToken) {
            this.PageToken = pageToken;
            return this;
        }

        public DescribeTriggersRequest WithLimit(int? limit) {
            this.Limit = limit;
            return this;
        }

    	[Preserve]
        public static DescribeTriggersRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DescribeTriggersRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithAccessToken(!data.Keys.Contains("accessToken") || data["accessToken"] == null ? null : data["accessToken"].ToString())
                .WithPageToken(!data.Keys.Contains("pageToken") || data["pageToken"] == null ? null : data["pageToken"].ToString())
                .WithLimit(!data.Keys.Contains("limit") || data["limit"] == null ? null : (int?)int.Parse(data["limit"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["accessToken"] = AccessToken,
                ["pageToken"] = PageToken,
                ["limit"] = Limit,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (AccessToken != null) {
                writer.WritePropertyName("accessToken");
                writer.Write(AccessToken.ToString());
            }
            if (PageToken != null) {
                writer.WritePropertyName("pageToken");
                writer.Write(PageToken.ToString());
            }
            if (Limit != null) {
                writer.WritePropertyName("limit");
                writer.Write(int.Parse(Limit.ToString()));
            }
            writer.WriteObjectEnd();
        }
    }
}