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

namespace Gs2.Gs2Ranking.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class Ranking : IComparable
	{
        public long? Rank { set; get; }
        public long? Index { set; get; }
        public string CategoryName { set; get; }
        public string UserId { set; get; }
        public long? Score { set; get; }
        public string Metadata { set; get; }
        public long? CreatedAt { set; get; }
        public Ranking WithRank(long? rank) {
            this.Rank = rank;
            return this;
        }
        public Ranking WithIndex(long? index) {
            this.Index = index;
            return this;
        }
        public Ranking WithCategoryName(string categoryName) {
            this.CategoryName = categoryName;
            return this;
        }
        public Ranking WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public Ranking WithScore(long? score) {
            this.Score = score;
            return this;
        }
        public Ranking WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public Ranking WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static Ranking FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new Ranking()
                .WithRank(!data.Keys.Contains("rank") || data["rank"] == null ? null : (long?)long.Parse(data["rank"].ToString()))
                .WithIndex(!data.Keys.Contains("index") || data["index"] == null ? null : (long?)long.Parse(data["index"].ToString()))
                .WithCategoryName(!data.Keys.Contains("categoryName") || data["categoryName"] == null ? null : data["categoryName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithScore(!data.Keys.Contains("score") || data["score"] == null ? null : (long?)long.Parse(data["score"].ToString()))
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["rank"] = Rank,
                ["index"] = Index,
                ["categoryName"] = CategoryName,
                ["userId"] = UserId,
                ["score"] = Score,
                ["metadata"] = Metadata,
                ["createdAt"] = CreatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Rank != null) {
                writer.WritePropertyName("rank");
                writer.Write(long.Parse(Rank.ToString()));
            }
            if (Index != null) {
                writer.WritePropertyName("index");
                writer.Write(long.Parse(Index.ToString()));
            }
            if (CategoryName != null) {
                writer.WritePropertyName("categoryName");
                writer.Write(CategoryName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Score != null) {
                writer.WritePropertyName("score");
                writer.Write(long.Parse(Score.ToString()));
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (CreatedAt != null) {
                writer.WritePropertyName("createdAt");
                writer.Write(long.Parse(CreatedAt.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Ranking;
            var diff = 0;
            if (Rank == null && Rank == other.Rank)
            {
                // null and null
            }
            else
            {
                diff += (int)(Rank - other.Rank);
            }
            if (Index == null && Index == other.Index)
            {
                // null and null
            }
            else
            {
                diff += (int)(Index - other.Index);
            }
            if (CategoryName == null && CategoryName == other.CategoryName)
            {
                // null and null
            }
            else
            {
                diff += CategoryName.CompareTo(other.CategoryName);
            }
            if (UserId == null && UserId == other.UserId)
            {
                // null and null
            }
            else
            {
                diff += UserId.CompareTo(other.UserId);
            }
            if (Score == null && Score == other.Score)
            {
                // null and null
            }
            else
            {
                diff += (int)(Score - other.Score);
            }
            if (Metadata == null && Metadata == other.Metadata)
            {
                // null and null
            }
            else
            {
                diff += Metadata.CompareTo(other.Metadata);
            }
            if (CreatedAt == null && CreatedAt == other.CreatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(CreatedAt - other.CreatedAt);
            }
            return diff;
        }
    }
}