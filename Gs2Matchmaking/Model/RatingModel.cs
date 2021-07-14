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

namespace Gs2.Gs2Matchmaking.Model
{

	[Preserve]
	public class RatingModel : IComparable
	{
        public string RatingModelId { set; get; }
        public string Name { set; get; }
        public string Metadata { set; get; }
        public int? Volatility { set; get; }

        public RatingModel WithRatingModelId(string ratingModelId) {
            this.RatingModelId = ratingModelId;
            return this;
        }

        public RatingModel WithName(string name) {
            this.Name = name;
            return this;
        }

        public RatingModel WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }

        public RatingModel WithVolatility(int? volatility) {
            this.Volatility = volatility;
            return this;
        }

    	[Preserve]
        public static RatingModel FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new RatingModel()
                .WithRatingModelId(!data.Keys.Contains("ratingModelId") || data["ratingModelId"] == null ? null : data["ratingModelId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithVolatility(!data.Keys.Contains("volatility") || data["volatility"] == null ? null : (int?)int.Parse(data["volatility"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["ratingModelId"] = RatingModelId,
                ["name"] = Name,
                ["metadata"] = Metadata,
                ["volatility"] = Volatility,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (RatingModelId != null) {
                writer.WritePropertyName("ratingModelId");
                writer.Write(RatingModelId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (Volatility != null) {
                writer.WritePropertyName("volatility");
                writer.Write(int.Parse(Volatility.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as RatingModel;
            var diff = 0;
            if (RatingModelId == null && RatingModelId == other.RatingModelId)
            {
                // null and null
            }
            else
            {
                diff += RatingModelId.CompareTo(other.RatingModelId);
            }
            if (Name == null && Name == other.Name)
            {
                // null and null
            }
            else
            {
                diff += Name.CompareTo(other.Name);
            }
            if (Metadata == null && Metadata == other.Metadata)
            {
                // null and null
            }
            else
            {
                diff += Metadata.CompareTo(other.Metadata);
            }
            if (Volatility == null && Volatility == other.Volatility)
            {
                // null and null
            }
            else
            {
                diff += (int)(Volatility - other.Volatility);
            }
            return diff;
        }
    }
}