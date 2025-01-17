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
using Gs2.Gs2Money.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Money.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class DepositByUserIdRequest : Gs2Request<DepositByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public int? Slot { set; get; }
        public float? Price { set; get; }
        public int? Count { set; get; }
        public string DuplicationAvoider { set; get; }
        public DepositByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public DepositByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public DepositByUserIdRequest WithSlot(int? slot) {
            this.Slot = slot;
            return this;
        }
        public DepositByUserIdRequest WithPrice(float? price) {
            this.Price = price;
            return this;
        }
        public DepositByUserIdRequest WithCount(int? count) {
            this.Count = count;
            return this;
        }

        public DepositByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DepositByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DepositByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithSlot(!data.Keys.Contains("slot") || data["slot"] == null ? null : (int?)int.Parse(data["slot"].ToString()))
                .WithPrice(!data.Keys.Contains("price") || data["price"] == null ? null : (float?)float.Parse(data["price"].ToString()))
                .WithCount(!data.Keys.Contains("count") || data["count"] == null ? null : (int?)int.Parse(data["count"].ToString()));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["slot"] = Slot,
                ["price"] = Price,
                ["count"] = Count,
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
            if (Slot != null) {
                writer.WritePropertyName("slot");
                writer.Write(int.Parse(Slot.ToString()));
            }
            if (Price != null) {
                writer.WritePropertyName("price");
                writer.Write(float.Parse(Price.ToString()));
            }
            if (Count != null) {
                writer.WritePropertyName("count");
                writer.Write(int.Parse(Count.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += UserId + ":";
            key += Slot + ":";
            key += Price + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new DepositByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                Slot = Slot,
                Price = Price,
                Count = Count * x,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (DepositByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values DepositByUserIdRequest::namespaceName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values DepositByUserIdRequest::userId");
            }
            if (Slot != y.Slot) {
                throw new ArithmeticException("mismatch parameter values DepositByUserIdRequest::slot");
            }
            if (Price != y.Price) {
                throw new ArithmeticException("mismatch parameter values DepositByUserIdRequest::price");
            }
            return new DepositByUserIdRequest {
                NamespaceName = NamespaceName,
                UserId = UserId,
                Slot = Slot,
                Price = Price,
                Count = Count + y.Count,
            };
        }
    }
}