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
using Gs2.Gs2Mission.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Mission.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class IncreaseCounterByUserIdRequest : Gs2Request<IncreaseCounterByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string CounterName { set; get; }
        public string UserId { set; get; }
        public long? Value { set; get; }
        public string DuplicationAvoider { set; get; }
        public IncreaseCounterByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public IncreaseCounterByUserIdRequest WithCounterName(string counterName) {
            this.CounterName = counterName;
            return this;
        }
        public IncreaseCounterByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public IncreaseCounterByUserIdRequest WithValue(long? value) {
            this.Value = value;
            return this;
        }

        public IncreaseCounterByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static IncreaseCounterByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new IncreaseCounterByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithCounterName(!data.Keys.Contains("counterName") || data["counterName"] == null ? null : data["counterName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithValue(!data.Keys.Contains("value") || data["value"] == null ? null : (long?)long.Parse(data["value"].ToString()));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["counterName"] = CounterName,
                ["userId"] = UserId,
                ["value"] = Value,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (CounterName != null) {
                writer.WritePropertyName("counterName");
                writer.Write(CounterName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Value != null) {
                writer.WritePropertyName("value");
                writer.Write(long.Parse(Value.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += CounterName + ":";
            key += UserId + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new IncreaseCounterByUserIdRequest {
                NamespaceName = NamespaceName,
                CounterName = CounterName,
                UserId = UserId,
                Value = Value * x,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (IncreaseCounterByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values IncreaseCounterByUserIdRequest::namespaceName");
            }
            if (CounterName != y.CounterName) {
                throw new ArithmeticException("mismatch parameter values IncreaseCounterByUserIdRequest::counterName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values IncreaseCounterByUserIdRequest::userId");
            }
            return new IncreaseCounterByUserIdRequest {
                NamespaceName = NamespaceName,
                CounterName = CounterName,
                UserId = UserId,
                Value = Value + y.Value,
            };
        }
    }
}