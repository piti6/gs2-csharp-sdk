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
using UnityEngine.Scripting;

namespace Gs2.Gs2Lottery.Model
{

	[Preserve]
	public class Probability : IComparable
	{
        public Gs2.Gs2Lottery.Model.DrawnPrize Prize { set; get; }
        public float? Rate { set; get; }

        public Probability WithPrize(Gs2.Gs2Lottery.Model.DrawnPrize prize) {
            this.Prize = prize;
            return this;
        }

        public Probability WithRate(float? rate) {
            this.Rate = rate;
            return this;
        }

    	[Preserve]
        public static Probability FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new Probability()
                .WithPrize(!data.Keys.Contains("prize") || data["prize"] == null ? null : Gs2.Gs2Lottery.Model.DrawnPrize.FromJson(data["prize"]))
                .WithRate(!data.Keys.Contains("rate") || data["rate"] == null ? null : (float?)float.Parse(data["rate"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["prize"] = Prize?.ToJson(),
                ["rate"] = Rate,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Prize != null) {
                writer.WritePropertyName("prize");
                Prize.WriteJson(writer);
            }
            if (Rate != null) {
                writer.WritePropertyName("rate");
                writer.Write(float.Parse(Rate.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Probability;
            var diff = 0;
            if (Prize == null && Prize == other.Prize)
            {
                // null and null
            }
            else
            {
                diff += Prize.CompareTo(other.Prize);
            }
            if (Rate == null && Rate == other.Rate)
            {
                // null and null
            }
            else
            {
                diff += (int)(Rate - other.Rate);
            }
            return diff;
        }
    }
}