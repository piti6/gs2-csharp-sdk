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
using Gs2.Gs2Gateway.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Gateway.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class SendMobileNotificationByUserIdRequest : Gs2Request<SendMobileNotificationByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public string Subject { set; get; }
        public string Payload { set; get; }
        public string Sound { set; get; }
        public string DuplicationAvoider { set; get; }
        public SendMobileNotificationByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public SendMobileNotificationByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public SendMobileNotificationByUserIdRequest WithSubject(string subject) {
            this.Subject = subject;
            return this;
        }
        public SendMobileNotificationByUserIdRequest WithPayload(string payload) {
            this.Payload = payload;
            return this;
        }
        public SendMobileNotificationByUserIdRequest WithSound(string sound) {
            this.Sound = sound;
            return this;
        }

        public SendMobileNotificationByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static SendMobileNotificationByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new SendMobileNotificationByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithSubject(!data.Keys.Contains("subject") || data["subject"] == null ? null : data["subject"].ToString())
                .WithPayload(!data.Keys.Contains("payload") || data["payload"] == null ? null : data["payload"].ToString())
                .WithSound(!data.Keys.Contains("sound") || data["sound"] == null ? null : data["sound"].ToString());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["subject"] = Subject,
                ["payload"] = Payload,
                ["sound"] = Sound,
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
            if (Subject != null) {
                writer.WritePropertyName("subject");
                writer.Write(Subject.ToString());
            }
            if (Payload != null) {
                writer.WritePropertyName("payload");
                writer.Write(Payload.ToString());
            }
            if (Sound != null) {
                writer.WritePropertyName("sound");
                writer.Write(Sound.ToString());
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += UserId + ":";
            key += Subject + ":";
            key += Payload + ":";
            key += Sound + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply SendMobileNotificationByUserIdRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (SendMobileNotificationByUserIdRequest)x;
            return this;
        }
    }
}