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

namespace Gs2.Gs2JobQueue.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class DeadLetterJob : IComparable
	{
        public string DeadLetterJobId { set; get; }
        public string Name { set; get; }
        public string UserId { set; get; }
        public string ScriptId { set; get; }
        public string Args { set; get; }
        public Gs2.Gs2JobQueue.Model.JobResultBody[] Result { set; get; }
        public long? CreatedAt { set; get; }
        public long? UpdatedAt { set; get; }
        public DeadLetterJob WithDeadLetterJobId(string deadLetterJobId) {
            this.DeadLetterJobId = deadLetterJobId;
            return this;
        }
        public DeadLetterJob WithName(string name) {
            this.Name = name;
            return this;
        }
        public DeadLetterJob WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public DeadLetterJob WithScriptId(string scriptId) {
            this.ScriptId = scriptId;
            return this;
        }
        public DeadLetterJob WithArgs(string args) {
            this.Args = args;
            return this;
        }
        public DeadLetterJob WithResult(Gs2.Gs2JobQueue.Model.JobResultBody[] result) {
            this.Result = result;
            return this;
        }
        public DeadLetterJob WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }
        public DeadLetterJob WithUpdatedAt(long? updatedAt) {
            this.UpdatedAt = updatedAt;
            return this;
        }

        private static System.Text.RegularExpressions.Regex _regionRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):queue:(?<namespaceName>.+):user:(?<userId>.+):dead:(?<deadLetterJobName>.+)",
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
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):queue:(?<namespaceName>.+):user:(?<userId>.+):dead:(?<deadLetterJobName>.+)",
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
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):queue:(?<namespaceName>.+):user:(?<userId>.+):dead:(?<deadLetterJobName>.+)",
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

        private static System.Text.RegularExpressions.Regex _userIdRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):queue:(?<namespaceName>.+):user:(?<userId>.+):dead:(?<deadLetterJobName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetUserIdFromGrn(
            string grn
        )
        {
            var match = _userIdRegex.Match(grn);
            if (!match.Success || !match.Groups["userId"].Success)
            {
                return null;
            }
            return match.Groups["userId"].Value;
        }

        private static System.Text.RegularExpressions.Regex _deadLetterJobNameRegex = new System.Text.RegularExpressions.Regex(
                @"grn:gs2:(?<region>.+):(?<ownerId>.+):queue:(?<namespaceName>.+):user:(?<userId>.+):dead:(?<deadLetterJobName>.+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        public static string GetDeadLetterJobNameFromGrn(
            string grn
        )
        {
            var match = _deadLetterJobNameRegex.Match(grn);
            if (!match.Success || !match.Groups["deadLetterJobName"].Success)
            {
                return null;
            }
            return match.Groups["deadLetterJobName"].Value;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static DeadLetterJob FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DeadLetterJob()
                .WithDeadLetterJobId(!data.Keys.Contains("deadLetterJobId") || data["deadLetterJobId"] == null ? null : data["deadLetterJobId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithScriptId(!data.Keys.Contains("scriptId") || data["scriptId"] == null ? null : data["scriptId"].ToString())
                .WithArgs(!data.Keys.Contains("args") || data["args"] == null ? null : data["args"].ToString())
                .WithResult(!data.Keys.Contains("result") || data["result"] == null ? new Gs2.Gs2JobQueue.Model.JobResultBody[]{} : data["result"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2JobQueue.Model.JobResultBody.FromJson(v);
                }).ToArray())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()))
                .WithUpdatedAt(!data.Keys.Contains("updatedAt") || data["updatedAt"] == null ? null : (long?)long.Parse(data["updatedAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["deadLetterJobId"] = DeadLetterJobId,
                ["name"] = Name,
                ["userId"] = UserId,
                ["scriptId"] = ScriptId,
                ["args"] = Args,
                ["result"] = Result == null ? null : new JsonData(
                        Result.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["createdAt"] = CreatedAt,
                ["updatedAt"] = UpdatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (DeadLetterJobId != null) {
                writer.WritePropertyName("deadLetterJobId");
                writer.Write(DeadLetterJobId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (ScriptId != null) {
                writer.WritePropertyName("scriptId");
                writer.Write(ScriptId.ToString());
            }
            if (Args != null) {
                writer.WritePropertyName("args");
                writer.Write(Args.ToString());
            }
            if (Result != null) {
                writer.WritePropertyName("result");
                writer.WriteArrayStart();
                foreach (var resul in Result)
                {
                    if (resul != null) {
                        resul.WriteJson(writer);
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
            var other = obj as DeadLetterJob;
            var diff = 0;
            if (DeadLetterJobId == null && DeadLetterJobId == other.DeadLetterJobId)
            {
                // null and null
            }
            else
            {
                diff += DeadLetterJobId.CompareTo(other.DeadLetterJobId);
            }
            if (Name == null && Name == other.Name)
            {
                // null and null
            }
            else
            {
                diff += Name.CompareTo(other.Name);
            }
            if (UserId == null && UserId == other.UserId)
            {
                // null and null
            }
            else
            {
                diff += UserId.CompareTo(other.UserId);
            }
            if (ScriptId == null && ScriptId == other.ScriptId)
            {
                // null and null
            }
            else
            {
                diff += ScriptId.CompareTo(other.ScriptId);
            }
            if (Args == null && Args == other.Args)
            {
                // null and null
            }
            else
            {
                diff += Args.CompareTo(other.Args);
            }
            if (Result == null && Result == other.Result)
            {
                // null and null
            }
            else
            {
                diff += Result.Length - other.Result.Length;
                for (var i = 0; i < Result.Length; i++)
                {
                    diff += Result[i].CompareTo(other.Result[i]);
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