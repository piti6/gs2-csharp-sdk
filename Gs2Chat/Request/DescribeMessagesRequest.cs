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
using Gs2.Gs2Chat.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Chat.Request
{
	[Preserve]
	[System.Serializable]
	public class DescribeMessagesRequest : Gs2Request<DescribeMessagesRequest>
	{
        public string NamespaceName { set; get; }
        public string RoomName { set; get; }
        public string Password { set; get; }
        public string AccessToken { set; get; }
        public long? StartAt { set; get; }
        public int? Limit { set; get; }

        public DescribeMessagesRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public DescribeMessagesRequest WithRoomName(string roomName) {
            this.RoomName = roomName;
            return this;
        }

        public DescribeMessagesRequest WithPassword(string password) {
            this.Password = password;
            return this;
        }

        public DescribeMessagesRequest WithAccessToken(string accessToken) {
            this.AccessToken = accessToken;
            return this;
        }

        public DescribeMessagesRequest WithStartAt(long? startAt) {
            this.StartAt = startAt;
            return this;
        }

        public DescribeMessagesRequest WithLimit(int? limit) {
            this.Limit = limit;
            return this;
        }

    	[Preserve]
        public static DescribeMessagesRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DescribeMessagesRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithRoomName(!data.Keys.Contains("roomName") || data["roomName"] == null ? null : data["roomName"].ToString())
                .WithPassword(!data.Keys.Contains("password") || data["password"] == null ? null : data["password"].ToString())
                .WithAccessToken(!data.Keys.Contains("accessToken") || data["accessToken"] == null ? null : data["accessToken"].ToString())
                .WithStartAt(!data.Keys.Contains("startAt") || data["startAt"] == null ? null : (long?)long.Parse(data["startAt"].ToString()))
                .WithLimit(!data.Keys.Contains("limit") || data["limit"] == null ? null : (int?)int.Parse(data["limit"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["roomName"] = RoomName,
                ["password"] = Password,
                ["accessToken"] = AccessToken,
                ["startAt"] = StartAt,
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
            if (RoomName != null) {
                writer.WritePropertyName("roomName");
                writer.Write(RoomName.ToString());
            }
            if (Password != null) {
                writer.WritePropertyName("password");
                writer.Write(Password.ToString());
            }
            if (AccessToken != null) {
                writer.WritePropertyName("accessToken");
                writer.Write(AccessToken.ToString());
            }
            if (StartAt != null) {
                writer.WritePropertyName("startAt");
                writer.Write(long.Parse(StartAt.ToString()));
            }
            if (Limit != null) {
                writer.WritePropertyName("limit");
                writer.Write(int.Parse(Limit.ToString()));
            }
            writer.WriteObjectEnd();
        }
    }
}