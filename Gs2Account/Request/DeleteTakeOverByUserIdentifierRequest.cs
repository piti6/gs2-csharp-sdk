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
using Gs2.Gs2Account.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Account.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class DeleteTakeOverByUserIdentifierRequest : Gs2Request<DeleteTakeOverByUserIdentifierRequest>
	{
        public string NamespaceName { set; get; }
        public int? Type { set; get; }
        public string UserIdentifier { set; get; }
        public string DuplicationAvoider { set; get; }
        public DeleteTakeOverByUserIdentifierRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public DeleteTakeOverByUserIdentifierRequest WithType(int? type) {
            this.Type = type;
            return this;
        }
        public DeleteTakeOverByUserIdentifierRequest WithUserIdentifier(string userIdentifier) {
            this.UserIdentifier = userIdentifier;
            return this;
        }

        public DeleteTakeOverByUserIdentifierRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DeleteTakeOverByUserIdentifierRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DeleteTakeOverByUserIdentifierRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithType(!data.Keys.Contains("type") || data["type"] == null ? null : (int?)int.Parse(data["type"].ToString()))
                .WithUserIdentifier(!data.Keys.Contains("userIdentifier") || data["userIdentifier"] == null ? null : data["userIdentifier"].ToString());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["type"] = Type,
                ["userIdentifier"] = UserIdentifier,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (Type != null) {
                writer.WritePropertyName("type");
                writer.Write(int.Parse(Type.ToString()));
            }
            if (UserIdentifier != null) {
                writer.WritePropertyName("userIdentifier");
                writer.Write(UserIdentifier.ToString());
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += Type + ":";
            key += UserIdentifier + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply DeleteTakeOverByUserIdentifierRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (DeleteTakeOverByUserIdentifierRequest)x;
            return this;
        }
    }
}