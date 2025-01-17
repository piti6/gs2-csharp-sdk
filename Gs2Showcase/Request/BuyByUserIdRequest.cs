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
using Gs2.Gs2Showcase.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Showcase.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class BuyByUserIdRequest : Gs2Request<BuyByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string ShowcaseName { set; get; }
        public string DisplayItemId { set; get; }
        public string UserId { set; get; }
        public int? Quantity { set; get; }
        public Gs2.Gs2Showcase.Model.Config[] Config { set; get; }
        public string DuplicationAvoider { set; get; }
        public BuyByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public BuyByUserIdRequest WithShowcaseName(string showcaseName) {
            this.ShowcaseName = showcaseName;
            return this;
        }
        public BuyByUserIdRequest WithDisplayItemId(string displayItemId) {
            this.DisplayItemId = displayItemId;
            return this;
        }
        public BuyByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public BuyByUserIdRequest WithQuantity(int? quantity) {
            this.Quantity = quantity;
            return this;
        }
        public BuyByUserIdRequest WithConfig(Gs2.Gs2Showcase.Model.Config[] config) {
            this.Config = config;
            return this;
        }

        public BuyByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static BuyByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new BuyByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithShowcaseName(!data.Keys.Contains("showcaseName") || data["showcaseName"] == null ? null : data["showcaseName"].ToString())
                .WithDisplayItemId(!data.Keys.Contains("displayItemId") || data["displayItemId"] == null ? null : data["displayItemId"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithQuantity(!data.Keys.Contains("quantity") || data["quantity"] == null ? null : (int?)int.Parse(data["quantity"].ToString()))
                .WithConfig(!data.Keys.Contains("config") || data["config"] == null ? new Gs2.Gs2Showcase.Model.Config[]{} : data["config"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Showcase.Model.Config.FromJson(v);
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["showcaseName"] = ShowcaseName,
                ["displayItemId"] = DisplayItemId,
                ["userId"] = UserId,
                ["quantity"] = Quantity,
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
            if (ShowcaseName != null) {
                writer.WritePropertyName("showcaseName");
                writer.Write(ShowcaseName.ToString());
            }
            if (DisplayItemId != null) {
                writer.WritePropertyName("displayItemId");
                writer.Write(DisplayItemId.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Quantity != null) {
                writer.WritePropertyName("quantity");
                writer.Write(int.Parse(Quantity.ToString()));
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
            key += ShowcaseName + ":";
            key += DisplayItemId + ":";
            key += UserId + ":";
            key += Quantity + ":";
            key += Config + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply BuyByUserIdRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (BuyByUserIdRequest)x;
            return this;
        }
    }
}