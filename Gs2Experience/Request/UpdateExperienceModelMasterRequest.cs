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
using Gs2.Gs2Experience.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Experience.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class UpdateExperienceModelMasterRequest : Gs2Request<UpdateExperienceModelMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string ExperienceName { set; get; }
        public string Description { set; get; }
        public string Metadata { set; get; }
        public long? DefaultExperience { set; get; }
        public long? DefaultRankCap { set; get; }
        public long? MaxRankCap { set; get; }
        public string RankThresholdName { set; get; }
        public UpdateExperienceModelMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithExperienceName(string experienceName) {
            this.ExperienceName = experienceName;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithDefaultExperience(long? defaultExperience) {
            this.DefaultExperience = defaultExperience;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithDefaultRankCap(long? defaultRankCap) {
            this.DefaultRankCap = defaultRankCap;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithMaxRankCap(long? maxRankCap) {
            this.MaxRankCap = maxRankCap;
            return this;
        }
        public UpdateExperienceModelMasterRequest WithRankThresholdName(string rankThresholdName) {
            this.RankThresholdName = rankThresholdName;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static UpdateExperienceModelMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateExperienceModelMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithExperienceName(!data.Keys.Contains("experienceName") || data["experienceName"] == null ? null : data["experienceName"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithDefaultExperience(!data.Keys.Contains("defaultExperience") || data["defaultExperience"] == null ? null : (long?)long.Parse(data["defaultExperience"].ToString()))
                .WithDefaultRankCap(!data.Keys.Contains("defaultRankCap") || data["defaultRankCap"] == null ? null : (long?)long.Parse(data["defaultRankCap"].ToString()))
                .WithMaxRankCap(!data.Keys.Contains("maxRankCap") || data["maxRankCap"] == null ? null : (long?)long.Parse(data["maxRankCap"].ToString()))
                .WithRankThresholdName(!data.Keys.Contains("rankThresholdName") || data["rankThresholdName"] == null ? null : data["rankThresholdName"].ToString());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["experienceName"] = ExperienceName,
                ["description"] = Description,
                ["metadata"] = Metadata,
                ["defaultExperience"] = DefaultExperience,
                ["defaultRankCap"] = DefaultRankCap,
                ["maxRankCap"] = MaxRankCap,
                ["rankThresholdName"] = RankThresholdName,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (ExperienceName != null) {
                writer.WritePropertyName("experienceName");
                writer.Write(ExperienceName.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (DefaultExperience != null) {
                writer.WritePropertyName("defaultExperience");
                writer.Write(long.Parse(DefaultExperience.ToString()));
            }
            if (DefaultRankCap != null) {
                writer.WritePropertyName("defaultRankCap");
                writer.Write(long.Parse(DefaultRankCap.ToString()));
            }
            if (MaxRankCap != null) {
                writer.WritePropertyName("maxRankCap");
                writer.Write(long.Parse(MaxRankCap.ToString()));
            }
            if (RankThresholdName != null) {
                writer.WritePropertyName("rankThresholdName");
                writer.Write(RankThresholdName.ToString());
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += ExperienceName + ":";
            key += Description + ":";
            key += Metadata + ":";
            key += DefaultExperience + ":";
            key += DefaultRankCap + ":";
            key += MaxRankCap + ":";
            key += RankThresholdName + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply UpdateExperienceModelMasterRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (UpdateExperienceModelMasterRequest)x;
            return this;
        }
    }
}