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
	public class CategoryModelMaster : IComparable
	{
        public string CategoryModelId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Metadata { set; get; }
        public long? MinimumValue { set; get; }
        public long? MaximumValue { set; get; }
        public string OrderDirection { set; get; }
        public string Scope { set; get; }
        public bool? UniqueByUserId { set; get; }
        public bool? Sum { set; get; }
        public int? CalculateFixedTimingHour { set; get; }
        public int? CalculateFixedTimingMinute { set; get; }
        public int? CalculateIntervalMinutes { set; get; }
        public string EntryPeriodEventId { set; get; }
        public string AccessPeriodEventId { set; get; }
        public string[] IgnoreUserIds { set; get; }
        public string Generation { set; get; }
        public long? CreatedAt { set; get; }
        public long? UpdatedAt { set; get; }
        public CategoryModelMaster WithCategoryModelId(string categoryModelId) {
            this.CategoryModelId = categoryModelId;
            return this;
        }
        public CategoryModelMaster WithName(string name) {
            this.Name = name;
            return this;
        }
        public CategoryModelMaster WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public CategoryModelMaster WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public CategoryModelMaster WithMinimumValue(long? minimumValue) {
            this.MinimumValue = minimumValue;
            return this;
        }
        public CategoryModelMaster WithMaximumValue(long? maximumValue) {
            this.MaximumValue = maximumValue;
            return this;
        }
        public CategoryModelMaster WithOrderDirection(string orderDirection) {
            this.OrderDirection = orderDirection;
            return this;
        }
        public CategoryModelMaster WithScope(string scope) {
            this.Scope = scope;
            return this;
        }
        public CategoryModelMaster WithUniqueByUserId(bool? uniqueByUserId) {
            this.UniqueByUserId = uniqueByUserId;
            return this;
        }
        public CategoryModelMaster WithSum(bool? sum) {
            this.Sum = sum;
            return this;
        }
        public CategoryModelMaster WithCalculateFixedTimingHour(int? calculateFixedTimingHour) {
            this.CalculateFixedTimingHour = calculateFixedTimingHour;
            return this;
        }
        public CategoryModelMaster WithCalculateFixedTimingMinute(int? calculateFixedTimingMinute) {
            this.CalculateFixedTimingMinute = calculateFixedTimingMinute;
            return this;
        }
        public CategoryModelMaster WithCalculateIntervalMinutes(int? calculateIntervalMinutes) {
            this.CalculateIntervalMinutes = calculateIntervalMinutes;
            return this;
        }
        public CategoryModelMaster WithEntryPeriodEventId(string entryPeriodEventId) {
            this.EntryPeriodEventId = entryPeriodEventId;
            return this;
        }
        public CategoryModelMaster WithAccessPeriodEventId(string accessPeriodEventId) {
            this.AccessPeriodEventId = accessPeriodEventId;
            return this;
        }
        public CategoryModelMaster WithIgnoreUserIds(string[] ignoreUserIds) {
            this.IgnoreUserIds = ignoreUserIds;
            return this;
        }
        public CategoryModelMaster WithGeneration(string generation) {
            this.Generation = generation;
            return this;
        }
        public CategoryModelMaster WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }
        public CategoryModelMaster WithUpdatedAt(long? updatedAt) {
            this.UpdatedAt = updatedAt;
            return this;
        }

        private static System.Text.RegularExpressions.Regex _regionRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):ranking:(?<namespaceName>.+):categoryModelMaster:(?<categoryName>.+)",
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
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):ranking:(?<namespaceName>.+):categoryModelMaster:(?<categoryName>.+)",
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
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):ranking:(?<namespaceName>.+):categoryModelMaster:(?<categoryName>.+)",
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

        private static System.Text.RegularExpressions.Regex _categoryNameRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):ranking:(?<namespaceName>.+):categoryModelMaster:(?<categoryName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetCategoryNameFromGrn(
            string grn
        )
        {
            var match = _categoryNameRegex.Match(grn);
            if (!match.Success || !match.Groups["categoryName"].Success)
            {
                return null;
            }
            return match.Groups["categoryName"].Value;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static CategoryModelMaster FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CategoryModelMaster()
                .WithCategoryModelId(!data.Keys.Contains("categoryModelId") || data["categoryModelId"] == null ? null : data["categoryModelId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithMinimumValue(!data.Keys.Contains("minimumValue") || data["minimumValue"] == null ? null : (long?)long.Parse(data["minimumValue"].ToString()))
                .WithMaximumValue(!data.Keys.Contains("maximumValue") || data["maximumValue"] == null ? null : (long?)long.Parse(data["maximumValue"].ToString()))
                .WithOrderDirection(!data.Keys.Contains("orderDirection") || data["orderDirection"] == null ? null : data["orderDirection"].ToString())
                .WithScope(!data.Keys.Contains("scope") || data["scope"] == null ? null : data["scope"].ToString())
                .WithUniqueByUserId(!data.Keys.Contains("uniqueByUserId") || data["uniqueByUserId"] == null ? null : (bool?)bool.Parse(data["uniqueByUserId"].ToString()))
                .WithSum(!data.Keys.Contains("sum") || data["sum"] == null ? null : (bool?)bool.Parse(data["sum"].ToString()))
                .WithCalculateFixedTimingHour(!data.Keys.Contains("calculateFixedTimingHour") || data["calculateFixedTimingHour"] == null ? null : (int?)int.Parse(data["calculateFixedTimingHour"].ToString()))
                .WithCalculateFixedTimingMinute(!data.Keys.Contains("calculateFixedTimingMinute") || data["calculateFixedTimingMinute"] == null ? null : (int?)int.Parse(data["calculateFixedTimingMinute"].ToString()))
                .WithCalculateIntervalMinutes(!data.Keys.Contains("calculateIntervalMinutes") || data["calculateIntervalMinutes"] == null ? null : (int?)int.Parse(data["calculateIntervalMinutes"].ToString()))
                .WithEntryPeriodEventId(!data.Keys.Contains("entryPeriodEventId") || data["entryPeriodEventId"] == null ? null : data["entryPeriodEventId"].ToString())
                .WithAccessPeriodEventId(!data.Keys.Contains("accessPeriodEventId") || data["accessPeriodEventId"] == null ? null : data["accessPeriodEventId"].ToString())
                .WithIgnoreUserIds(!data.Keys.Contains("ignoreUserIds") || data["ignoreUserIds"] == null ? new string[]{} : data["ignoreUserIds"].Cast<JsonData>().Select(v => {
                    return v.ToString();
                }).ToArray())
                .WithGeneration(!data.Keys.Contains("generation") || data["generation"] == null ? null : data["generation"].ToString())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()))
                .WithUpdatedAt(!data.Keys.Contains("updatedAt") || data["updatedAt"] == null ? null : (long?)long.Parse(data["updatedAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["categoryModelId"] = CategoryModelId,
                ["name"] = Name,
                ["description"] = Description,
                ["metadata"] = Metadata,
                ["minimumValue"] = MinimumValue,
                ["maximumValue"] = MaximumValue,
                ["orderDirection"] = OrderDirection,
                ["scope"] = Scope,
                ["uniqueByUserId"] = UniqueByUserId,
                ["sum"] = Sum,
                ["calculateFixedTimingHour"] = CalculateFixedTimingHour,
                ["calculateFixedTimingMinute"] = CalculateFixedTimingMinute,
                ["calculateIntervalMinutes"] = CalculateIntervalMinutes,
                ["entryPeriodEventId"] = EntryPeriodEventId,
                ["accessPeriodEventId"] = AccessPeriodEventId,
                ["ignoreUserIds"] = IgnoreUserIds == null ? null : new JsonData(
                        IgnoreUserIds.Select(v => {
                            return new JsonData(v.ToString());
                        }).ToArray()
                    ),
                ["generation"] = Generation,
                ["createdAt"] = CreatedAt,
                ["updatedAt"] = UpdatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (CategoryModelId != null) {
                writer.WritePropertyName("categoryModelId");
                writer.Write(CategoryModelId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (MinimumValue != null) {
                writer.WritePropertyName("minimumValue");
                writer.Write(long.Parse(MinimumValue.ToString()));
            }
            if (MaximumValue != null) {
                writer.WritePropertyName("maximumValue");
                writer.Write(long.Parse(MaximumValue.ToString()));
            }
            if (OrderDirection != null) {
                writer.WritePropertyName("orderDirection");
                writer.Write(OrderDirection.ToString());
            }
            if (Scope != null) {
                writer.WritePropertyName("scope");
                writer.Write(Scope.ToString());
            }
            if (UniqueByUserId != null) {
                writer.WritePropertyName("uniqueByUserId");
                writer.Write(bool.Parse(UniqueByUserId.ToString()));
            }
            if (Sum != null) {
                writer.WritePropertyName("sum");
                writer.Write(bool.Parse(Sum.ToString()));
            }
            if (CalculateFixedTimingHour != null) {
                writer.WritePropertyName("calculateFixedTimingHour");
                writer.Write(int.Parse(CalculateFixedTimingHour.ToString()));
            }
            if (CalculateFixedTimingMinute != null) {
                writer.WritePropertyName("calculateFixedTimingMinute");
                writer.Write(int.Parse(CalculateFixedTimingMinute.ToString()));
            }
            if (CalculateIntervalMinutes != null) {
                writer.WritePropertyName("calculateIntervalMinutes");
                writer.Write(int.Parse(CalculateIntervalMinutes.ToString()));
            }
            if (EntryPeriodEventId != null) {
                writer.WritePropertyName("entryPeriodEventId");
                writer.Write(EntryPeriodEventId.ToString());
            }
            if (AccessPeriodEventId != null) {
                writer.WritePropertyName("accessPeriodEventId");
                writer.Write(AccessPeriodEventId.ToString());
            }
            if (IgnoreUserIds != null) {
                writer.WritePropertyName("ignoreUserIds");
                writer.WriteArrayStart();
                foreach (var ignoreUserId in IgnoreUserIds)
                {
                    if (ignoreUserId != null) {
                        writer.Write(ignoreUserId.ToString());
                    }
                }
                writer.WriteArrayEnd();
            }
            if (Generation != null) {
                writer.WritePropertyName("generation");
                writer.Write(Generation.ToString());
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
            var other = obj as CategoryModelMaster;
            var diff = 0;
            if (CategoryModelId == null && CategoryModelId == other.CategoryModelId)
            {
                // null and null
            }
            else
            {
                diff += CategoryModelId.CompareTo(other.CategoryModelId);
            }
            if (Name == null && Name == other.Name)
            {
                // null and null
            }
            else
            {
                diff += Name.CompareTo(other.Name);
            }
            if (Description == null && Description == other.Description)
            {
                // null and null
            }
            else
            {
                diff += Description.CompareTo(other.Description);
            }
            if (Metadata == null && Metadata == other.Metadata)
            {
                // null and null
            }
            else
            {
                diff += Metadata.CompareTo(other.Metadata);
            }
            if (MinimumValue == null && MinimumValue == other.MinimumValue)
            {
                // null and null
            }
            else
            {
                diff += (int)(MinimumValue - other.MinimumValue);
            }
            if (MaximumValue == null && MaximumValue == other.MaximumValue)
            {
                // null and null
            }
            else
            {
                diff += (int)(MaximumValue - other.MaximumValue);
            }
            if (OrderDirection == null && OrderDirection == other.OrderDirection)
            {
                // null and null
            }
            else
            {
                diff += OrderDirection.CompareTo(other.OrderDirection);
            }
            if (Scope == null && Scope == other.Scope)
            {
                // null and null
            }
            else
            {
                diff += Scope.CompareTo(other.Scope);
            }
            if (UniqueByUserId == null && UniqueByUserId == other.UniqueByUserId)
            {
                // null and null
            }
            else
            {
                diff += UniqueByUserId == other.UniqueByUserId ? 0 : 1;
            }
            if (Sum == null && Sum == other.Sum)
            {
                // null and null
            }
            else
            {
                diff += Sum == other.Sum ? 0 : 1;
            }
            if (CalculateFixedTimingHour == null && CalculateFixedTimingHour == other.CalculateFixedTimingHour)
            {
                // null and null
            }
            else
            {
                diff += (int)(CalculateFixedTimingHour - other.CalculateFixedTimingHour);
            }
            if (CalculateFixedTimingMinute == null && CalculateFixedTimingMinute == other.CalculateFixedTimingMinute)
            {
                // null and null
            }
            else
            {
                diff += (int)(CalculateFixedTimingMinute - other.CalculateFixedTimingMinute);
            }
            if (CalculateIntervalMinutes == null && CalculateIntervalMinutes == other.CalculateIntervalMinutes)
            {
                // null and null
            }
            else
            {
                diff += (int)(CalculateIntervalMinutes - other.CalculateIntervalMinutes);
            }
            if (EntryPeriodEventId == null && EntryPeriodEventId == other.EntryPeriodEventId)
            {
                // null and null
            }
            else
            {
                diff += EntryPeriodEventId.CompareTo(other.EntryPeriodEventId);
            }
            if (AccessPeriodEventId == null && AccessPeriodEventId == other.AccessPeriodEventId)
            {
                // null and null
            }
            else
            {
                diff += AccessPeriodEventId.CompareTo(other.AccessPeriodEventId);
            }
            if (IgnoreUserIds == null && IgnoreUserIds == other.IgnoreUserIds)
            {
                // null and null
            }
            else
            {
                diff += IgnoreUserIds.Length - other.IgnoreUserIds.Length;
                for (var i = 0; i < IgnoreUserIds.Length; i++)
                {
                    diff += IgnoreUserIds[i].CompareTo(other.IgnoreUserIds[i]);
                }
            }
            if (Generation == null && Generation == other.Generation)
            {
                // null and null
            }
            else
            {
                diff += Generation.CompareTo(other.Generation);
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