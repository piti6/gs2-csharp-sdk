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
using Gs2.Gs2Inventory.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Inventory.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class SetCapacityByUserIdRequest : Gs2Request<SetCapacityByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string InventoryName { set; get; }
        public string UserId { set; get; }
        public int? NewCapacityValue { set; get; }
        public string DuplicationAvoider { set; get; }
        public SetCapacityByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public SetCapacityByUserIdRequest WithInventoryName(string inventoryName) {
            this.InventoryName = inventoryName;
            return this;
        }
        public SetCapacityByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public SetCapacityByUserIdRequest WithNewCapacityValue(int? newCapacityValue) {
            this.NewCapacityValue = newCapacityValue;
            return this;
        }

        public SetCapacityByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static SetCapacityByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new SetCapacityByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithInventoryName(!data.Keys.Contains("inventoryName") || data["inventoryName"] == null ? null : data["inventoryName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithNewCapacityValue(!data.Keys.Contains("newCapacityValue") || data["newCapacityValue"] == null ? null : (int?)int.Parse(data["newCapacityValue"].ToString()));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["inventoryName"] = InventoryName,
                ["userId"] = UserId,
                ["newCapacityValue"] = NewCapacityValue,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (InventoryName != null) {
                writer.WritePropertyName("inventoryName");
                writer.Write(InventoryName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (NewCapacityValue != null) {
                writer.WritePropertyName("newCapacityValue");
                writer.Write(int.Parse(NewCapacityValue.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += InventoryName + ":";
            key += UserId + ":";
            key += NewCapacityValue + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new SetCapacityByUserIdRequest {
                NamespaceName = NamespaceName,
                InventoryName = InventoryName,
                UserId = UserId,
                NewCapacityValue = NewCapacityValue,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (SetCapacityByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values SetCapacityByUserIdRequest::namespaceName");
            }
            if (InventoryName != y.InventoryName) {
                throw new ArithmeticException("mismatch parameter values SetCapacityByUserIdRequest::inventoryName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values SetCapacityByUserIdRequest::userId");
            }
            if (NewCapacityValue != y.NewCapacityValue) {
                throw new ArithmeticException("mismatch parameter values SetCapacityByUserIdRequest::newCapacityValue");
            }
            return new SetCapacityByUserIdRequest {
                NamespaceName = NamespaceName,
                InventoryName = InventoryName,
                UserId = UserId,
                NewCapacityValue = NewCapacityValue,
            };
        }
    }
}