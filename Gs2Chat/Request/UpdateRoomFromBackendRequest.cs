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

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Chat.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class UpdateRoomFromBackendRequest : Gs2Request<UpdateRoomFromBackendRequest>
	{
        public string NamespaceName { set; get; }
        public string RoomName { set; get; }
        public string Metadata { set; get; }
        public string Password { set; get; }
        public string[] WhiteListUserIds { set; get; }
        public string UserId { set; get; }
        public string DuplicationAvoider { set; get; }
        public UpdateRoomFromBackendRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public UpdateRoomFromBackendRequest WithRoomName(string roomName) {
            this.RoomName = roomName;
            return this;
        }
        public UpdateRoomFromBackendRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public UpdateRoomFromBackendRequest WithPassword(string password) {
            this.Password = password;
            return this;
        }
        public UpdateRoomFromBackendRequest WithWhiteListUserIds(string[] whiteListUserIds) {
            this.WhiteListUserIds = whiteListUserIds;
            return this;
        }
        public UpdateRoomFromBackendRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }

        public UpdateRoomFromBackendRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static UpdateRoomFromBackendRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateRoomFromBackendRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithRoomName(!data.Keys.Contains("roomName") || data["roomName"] == null ? null : data["roomName"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithPassword(!data.Keys.Contains("password") || data["password"] == null ? null : data["password"].ToString())
                .WithWhiteListUserIds(!data.Keys.Contains("whiteListUserIds") || data["whiteListUserIds"] == null ? new string[]{} : data["whiteListUserIds"].Cast<JsonData>().Select(v => {
                    return v.ToString();
                }).ToArray())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["roomName"] = RoomName,
                ["metadata"] = Metadata,
                ["password"] = Password,
                ["whiteListUserIds"] = WhiteListUserIds == null ? null : new JsonData(
                        WhiteListUserIds.Select(v => {
                            return new JsonData(v.ToString());
                        }).ToArray()
                    ),
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
            if (RoomName != null) {
                writer.WritePropertyName("roomName");
                writer.Write(RoomName.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (Password != null) {
                writer.WritePropertyName("password");
                writer.Write(Password.ToString());
            }
            writer.WriteArrayStart();
            foreach (var whiteListUserId in WhiteListUserIds)
            {
                writer.Write(whiteListUserId.ToString());
            }
            writer.WriteArrayEnd();
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += RoomName + ":";
            key += Metadata + ":";
            key += Password + ":";
            key += WhiteListUserIds + ":";
            key += UserId + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply UpdateRoomFromBackendRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (UpdateRoomFromBackendRequest)x;
            return this;
        }
    }
}