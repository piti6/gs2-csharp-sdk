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
using Gs2.Gs2Lottery.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Lottery.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class DrawByUserIdRequest : Gs2Request<DrawByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string LotteryName { set; get; }
        public string UserId { set; get; }
        public int? Count { set; get; }
        public Gs2.Gs2Lottery.Model.Config[] Config { set; get; }
        public string DuplicationAvoider { set; get; }
        public DrawByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public DrawByUserIdRequest WithLotteryName(string lotteryName) {
            this.LotteryName = lotteryName;
            return this;
        }
        public DrawByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public DrawByUserIdRequest WithCount(int? count) {
            this.Count = count;
            return this;
        }
        public DrawByUserIdRequest WithConfig(Gs2.Gs2Lottery.Model.Config[] config) {
            this.Config = config;
            return this;
        }

        public DrawByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DrawByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DrawByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithLotteryName(!data.Keys.Contains("lotteryName") || data["lotteryName"] == null ? null : data["lotteryName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithCount(!data.Keys.Contains("count") || data["count"] == null ? null : (int?)int.Parse(data["count"].ToString()))
                .WithConfig(!data.Keys.Contains("config") || data["config"] == null ? new Gs2.Gs2Lottery.Model.Config[]{} : data["config"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Lottery.Model.Config.FromJson(v);
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["lotteryName"] = LotteryName,
                ["userId"] = UserId,
                ["count"] = Count,
                ["config"] = Config == null ? null : new JsonData(
                        Config.Select(v => {
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
            if (LotteryName != null) {
                writer.WritePropertyName("lotteryName");
                writer.Write(LotteryName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Count != null) {
                writer.WritePropertyName("count");
                writer.Write(int.Parse(Count.ToString()));
            }
            writer.WriteArrayStart();
            foreach (var confi in Config)
            {
                if (confi != null) {
                    confi.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += LotteryName + ":";
            key += UserId + ":";
            key += Config + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            return new DrawByUserIdRequest {
                NamespaceName = NamespaceName,
                LotteryName = LotteryName,
                UserId = UserId,
                Count = Count * x,
                Config = Config,
            };
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (DrawByUserIdRequest)x;
            if (NamespaceName != y.NamespaceName) {
                throw new ArithmeticException("mismatch parameter values DrawByUserIdRequest::namespaceName");
            }
            if (LotteryName != y.LotteryName) {
                throw new ArithmeticException("mismatch parameter values DrawByUserIdRequest::lotteryName");
            }
            if (UserId != y.UserId) {
                throw new ArithmeticException("mismatch parameter values DrawByUserIdRequest::userId");
            }
            if (Config != y.Config) {
                throw new ArithmeticException("mismatch parameter values DrawByUserIdRequest::config");
            }
            return new DrawByUserIdRequest {
                NamespaceName = NamespaceName,
                LotteryName = LotteryName,
                UserId = UserId,
                Count = Count + y.Count,
                Config = Config,
            };
        }
    }
}