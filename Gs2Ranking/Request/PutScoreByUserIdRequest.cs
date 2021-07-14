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
using Gs2.Gs2Ranking.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Ranking.Request
{
	[Preserve]
	[System.Serializable]
	public class PutScoreByUserIdRequest : Gs2Request<PutScoreByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string CategoryName { set; get; }
        public string UserId { set; get; }
        public long? Score { set; get; }
        public string Metadata { set; get; }

        public PutScoreByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public PutScoreByUserIdRequest WithCategoryName(string categoryName) {
            this.CategoryName = categoryName;
            return this;
        }

        public PutScoreByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }

        public PutScoreByUserIdRequest WithScore(long? score) {
            this.Score = score;
            return this;
        }

        public PutScoreByUserIdRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }

    	[Preserve]
        public static PutScoreByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new PutScoreByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithCategoryName(!data.Keys.Contains("categoryName") || data["categoryName"] == null ? null : data["categoryName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithScore(!data.Keys.Contains("score") || data["score"] == null ? null : (long?)long.Parse(data["score"].ToString()))
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["categoryName"] = CategoryName,
                ["userId"] = UserId,
                ["score"] = Score,
                ["metadata"] = Metadata,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
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
            writer.WriteObjectEnd();
        }
    }
}