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
	public class SubscribeRequest : Gs2Request<SubscribeRequest>
	{
        public string NamespaceName { set; get; }
        public string RoomName { set; get; }
        public string AccessToken { set; get; }
        public Gs2.Gs2Chat.Model.NotificationType[] NotificationTypes { set; get; }
        public string DuplicationAvoider { set; get; }
        public SubscribeRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public SubscribeRequest WithRoomName(string roomName) {
            this.RoomName = roomName;
            return this;
        }
        public SubscribeRequest WithAccessToken(string accessToken) {
            this.AccessToken = accessToken;
            return this;
        }
        public SubscribeRequest WithNotificationTypes(Gs2.Gs2Chat.Model.NotificationType[] notificationTypes) {
            this.NotificationTypes = notificationTypes;
            return this;
        }

        public SubscribeRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static SubscribeRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new SubscribeRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithRoomName(!data.Keys.Contains("roomName") || data["roomName"] == null ? null : data["roomName"].ToString())
                .WithAccessToken(!data.Keys.Contains("accessToken") || data["accessToken"] == null ? null : data["accessToken"].ToString())
                .WithNotificationTypes(!data.Keys.Contains("notificationTypes") || data["notificationTypes"] == null ? new Gs2.Gs2Chat.Model.NotificationType[]{} : data["notificationTypes"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Chat.Model.NotificationType.FromJson(v);
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["roomName"] = RoomName,
                ["accessToken"] = AccessToken,
                ["notificationTypes"] = NotificationTypes == null ? null : new JsonData(
                        NotificationTypes.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
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
            if (AccessToken != null) {
                writer.WritePropertyName("accessToken");
                writer.Write(AccessToken.ToString());
            }
            writer.WriteArrayStart();
            foreach (var notificationType in NotificationTypes)
            {
                if (notificationType != null) {
                    notificationType.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += RoomName + ":";
            key += AccessToken + ":";
            key += NotificationTypes + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply SubscribeRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (SubscribeRequest)x;
            return this;
        }
    }
}