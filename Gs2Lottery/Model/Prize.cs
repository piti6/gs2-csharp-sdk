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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Lottery.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class Prize : IComparable
	{
        public string PrizeId { set; get; }
        public string Type { set; get; }
        public Gs2.Gs2Lottery.Model.AcquireAction[] AcquireActions { set; get; }
        public int? DrawnLimit { set; get; }
        public string LimitFailOverPrizeId { set; get; }
        public string PrizeTableName { set; get; }
        public int? Weight { set; get; }
        public Prize WithPrizeId(string prizeId) {
            this.PrizeId = prizeId;
            return this;
        }
        public Prize WithType(string type) {
            this.Type = type;
            return this;
        }
        public Prize WithAcquireActions(Gs2.Gs2Lottery.Model.AcquireAction[] acquireActions) {
            this.AcquireActions = acquireActions;
            return this;
        }
        public Prize WithDrawnLimit(int? drawnLimit) {
            this.DrawnLimit = drawnLimit;
            return this;
        }
        public Prize WithLimitFailOverPrizeId(string limitFailOverPrizeId) {
            this.LimitFailOverPrizeId = limitFailOverPrizeId;
            return this;
        }
        public Prize WithPrizeTableName(string prizeTableName) {
            this.PrizeTableName = prizeTableName;
            return this;
        }
        public Prize WithWeight(int? weight) {
            this.Weight = weight;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static Prize FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new Prize()
                .WithPrizeId(!data.Keys.Contains("prizeId") || data["prizeId"] == null ? null : data["prizeId"].ToString())
                .WithType(!data.Keys.Contains("type") || data["type"] == null ? null : data["type"].ToString())
                .WithAcquireActions(!data.Keys.Contains("acquireActions") || data["acquireActions"] == null ? new Gs2.Gs2Lottery.Model.AcquireAction[]{} : data["acquireActions"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Lottery.Model.AcquireAction.FromJson(v);
                }).ToArray())
                .WithDrawnLimit(!data.Keys.Contains("drawnLimit") || data["drawnLimit"] == null ? null : (int?)int.Parse(data["drawnLimit"].ToString()))
                .WithLimitFailOverPrizeId(!data.Keys.Contains("limitFailOverPrizeId") || data["limitFailOverPrizeId"] == null ? null : data["limitFailOverPrizeId"].ToString())
                .WithPrizeTableName(!data.Keys.Contains("prizeTableName") || data["prizeTableName"] == null ? null : data["prizeTableName"].ToString())
                .WithWeight(!data.Keys.Contains("weight") || data["weight"] == null ? null : (int?)int.Parse(data["weight"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["prizeId"] = PrizeId,
                ["type"] = Type,
                ["acquireActions"] = new JsonData(AcquireActions == null ? new JsonData[]{} :
                        AcquireActions.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["drawnLimit"] = DrawnLimit,
                ["limitFailOverPrizeId"] = LimitFailOverPrizeId,
                ["prizeTableName"] = PrizeTableName,
                ["weight"] = Weight,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (PrizeId != null) {
                writer.WritePropertyName("prizeId");
                writer.Write(PrizeId.ToString());
            }
            if (Type != null) {
                writer.WritePropertyName("type");
                writer.Write(Type.ToString());
            }
            if (AcquireActions != null) {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach (var acquireAction in AcquireActions)
                {
                    if (acquireAction != null) {
                        acquireAction.WriteJson(writer);
                    }
                }
                writer.WriteArrayEnd();
            }
            if (DrawnLimit != null) {
                writer.WritePropertyName("drawnLimit");
                writer.Write(int.Parse(DrawnLimit.ToString()));
            }
            if (LimitFailOverPrizeId != null) {
                writer.WritePropertyName("limitFailOverPrizeId");
                writer.Write(LimitFailOverPrizeId.ToString());
            }
            if (PrizeTableName != null) {
                writer.WritePropertyName("prizeTableName");
                writer.Write(PrizeTableName.ToString());
            }
            if (Weight != null) {
                writer.WritePropertyName("weight");
                writer.Write(int.Parse(Weight.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Prize;
            var diff = 0;
            if (PrizeId == null && PrizeId == other.PrizeId)
            {
                // null and null
            }
            else
            {
                diff += PrizeId.CompareTo(other.PrizeId);
            }
            if (Type == null && Type == other.Type)
            {
                // null and null
            }
            else
            {
                diff += Type.CompareTo(other.Type);
            }
            if (AcquireActions == null && AcquireActions == other.AcquireActions)
            {
                // null and null
            }
            else
            {
                diff += AcquireActions.Length - other.AcquireActions.Length;
                for (var i = 0; i < AcquireActions.Length; i++)
                {
                    diff += AcquireActions[i].CompareTo(other.AcquireActions[i]);
                }
            }
            if (DrawnLimit == null && DrawnLimit == other.DrawnLimit)
            {
                // null and null
            }
            else
            {
                diff += (int)(DrawnLimit - other.DrawnLimit);
            }
            if (LimitFailOverPrizeId == null && LimitFailOverPrizeId == other.LimitFailOverPrizeId)
            {
                // null and null
            }
            else
            {
                diff += LimitFailOverPrizeId.CompareTo(other.LimitFailOverPrizeId);
            }
            if (PrizeTableName == null && PrizeTableName == other.PrizeTableName)
            {
                // null and null
            }
            else
            {
                diff += PrizeTableName.CompareTo(other.PrizeTableName);
            }
            if (Weight == null && Weight == other.Weight)
            {
                // null and null
            }
            else
            {
                diff += (int)(Weight - other.Weight);
            }
            return diff;
        }
    }
}