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

namespace Gs2.Gs2Stamina.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class MaxStaminaTableMaster : IComparable
	{
        public string MaxStaminaTableId { set; get; }
        public string Name { set; get; }
        public string Metadata { set; get; }
        public string Description { set; get; }
        public string ExperienceModelId { set; get; }
        public int[] Values { set; get; }
        public long? CreatedAt { set; get; }
        public long? UpdatedAt { set; get; }
        public MaxStaminaTableMaster WithMaxStaminaTableId(string maxStaminaTableId) {
            this.MaxStaminaTableId = maxStaminaTableId;
            return this;
        }
        public MaxStaminaTableMaster WithName(string name) {
            this.Name = name;
            return this;
        }
        public MaxStaminaTableMaster WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public MaxStaminaTableMaster WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public MaxStaminaTableMaster WithExperienceModelId(string experienceModelId) {
            this.ExperienceModelId = experienceModelId;
            return this;
        }
        public MaxStaminaTableMaster WithValues(int[] values) {
            this.Values = values;
            return this;
        }
        public MaxStaminaTableMaster WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }
        public MaxStaminaTableMaster WithUpdatedAt(long? updatedAt) {
            this.UpdatedAt = updatedAt;
            return this;
        }

        private static System.Text.RegularExpressions.Regex _regionRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):stamina:(?<namespaceName>.+):maxStaminaTable:(?<maxStaminaTableName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetRegionFromGrn(
            string grn
        )
        {
            var match = _regionRegex.Match(grn);
            if (!match.Success || !match.Groups["region"].Success)
            {
                return null;
            }
            return match.Groups["region"].Value;
        }

        private static System.Text.RegularExpressions.Regex _ownerIdRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):stamina:(?<namespaceName>.+):maxStaminaTable:(?<maxStaminaTableName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetOwnerIdFromGrn(
            string grn
        )
        {
            var match = _ownerIdRegex.Match(grn);
            if (!match.Success || !match.Groups["ownerId"].Success)
            {
                return null;
            }
            return match.Groups["ownerId"].Value;
        }

        private static System.Text.RegularExpressions.Regex _namespaceNameRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):stamina:(?<namespaceName>.+):maxStaminaTable:(?<maxStaminaTableName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetNamespaceNameFromGrn(
            string grn
        )
        {
            var match = _namespaceNameRegex.Match(grn);
            if (!match.Success || !match.Groups["namespaceName"].Success)
            {
                return null;
            }
            return match.Groups["namespaceName"].Value;
        }

        private static System.Text.RegularExpressions.Regex _maxStaminaTableNameRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):stamina:(?<namespaceName>.+):maxStaminaTable:(?<maxStaminaTableName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetMaxStaminaTableNameFromGrn(
            string grn
        )
        {
            var match = _maxStaminaTableNameRegex.Match(grn);
            if (!match.Success || !match.Groups["maxStaminaTableName"].Success)
            {
                return null;
            }
            return match.Groups["maxStaminaTableName"].Value;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static MaxStaminaTableMaster FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new MaxStaminaTableMaster()
                .WithMaxStaminaTableId(!data.Keys.Contains("maxStaminaTableId") || data["maxStaminaTableId"] == null ? null : data["maxStaminaTableId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithExperienceModelId(!data.Keys.Contains("experienceModelId") || data["experienceModelId"] == null ? null : data["experienceModelId"].ToString())
                .WithValues(!data.Keys.Contains("values") || data["values"] == null ? new int[]{} : data["values"].Cast<JsonData>().Select(v => {
                    return int.Parse(v.ToString());
                }).ToArray())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()))
                .WithUpdatedAt(!data.Keys.Contains("updatedAt") || data["updatedAt"] == null ? null : (long?)long.Parse(data["updatedAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["maxStaminaTableId"] = MaxStaminaTableId,
                ["name"] = Name,
                ["metadata"] = Metadata,
                ["description"] = Description,
                ["experienceModelId"] = ExperienceModelId,
                ["values"] = Values == null ? null : new JsonData(
                        Values.Select(v => {
                            return new JsonData((int?)int.Parse(v.ToString()));
                        }).ToArray()
                    ),
                ["createdAt"] = CreatedAt,
                ["updatedAt"] = UpdatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (MaxStaminaTableId != null) {
                writer.WritePropertyName("maxStaminaTableId");
                writer.Write(MaxStaminaTableId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (ExperienceModelId != null) {
                writer.WritePropertyName("experienceModelId");
                writer.Write(ExperienceModelId.ToString());
            }
            if (Values != null) {
                writer.WritePropertyName("values");
                writer.WriteArrayStart();
                foreach (var value in Values)
                {
                    if (value != null) {
                        writer.Write(int.Parse(value.ToString()));
                    }
                }
                writer.WriteArrayEnd();
            }
            if (CreatedAt != null) {
                writer.WritePropertyName("createdAt");
                writer.Write(long.Parse(CreatedAt.ToString()));
            }
            if (UpdatedAt != null) {
                writer.WritePropertyName("updatedAt");
                writer.Write(long.Parse(UpdatedAt.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as MaxStaminaTableMaster;
            var diff = 0;
            if (MaxStaminaTableId == null && MaxStaminaTableId == other.MaxStaminaTableId)
            {
                // null and null
            }
            else
            {
                diff += MaxStaminaTableId.CompareTo(other.MaxStaminaTableId);
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
            if (Description == null && Description == other.Description)
            {
                // null and null
            }
            else
            {
                diff += Description.CompareTo(other.Description);
            }
            if (ExperienceModelId == null && ExperienceModelId == other.ExperienceModelId)
            {
                // null and null
            }
            else
            {
                diff += ExperienceModelId.CompareTo(other.ExperienceModelId);
            }
            if (Values == null && Values == other.Values)
            {
                // null and null
            }
            else
            {
                diff += Values.Length - other.Values.Length;
                for (var i = 0; i < Values.Length; i++)
                {
                    diff += (int)(Values[i] - other.Values[i]);
                }
            }
            if (CreatedAt == null && CreatedAt == other.CreatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(CreatedAt - other.CreatedAt);
            }
            if (UpdatedAt == null && UpdatedAt == other.UpdatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(UpdatedAt - other.UpdatedAt);
            }
            return diff;
        }
    }
}