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

namespace Gs2.Gs2Deploy.Model
{

	[Preserve]
	public class Output : IComparable
	{
        public string OutputId { set; get; }
        public string Name { set; get; }
        public string Value { set; get; }
        public long? CreatedAt { set; get; }

        public Output WithOutputId(string outputId) {
            this.OutputId = outputId;
            return this;
        }

        public Output WithName(string name) {
            this.Name = name;
            return this;
        }

        public Output WithValue(string value) {
            this.Value = value;
            return this;
        }

        public Output WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }

    	[Preserve]
        public static Output FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new Output()
                .WithOutputId(!data.Keys.Contains("outputId") || data["outputId"] == null ? null : data["outputId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithValue(!data.Keys.Contains("value") || data["value"] == null ? null : data["value"].ToString())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["outputId"] = OutputId,
                ["name"] = Name,
                ["value"] = Value,
                ["createdAt"] = CreatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (OutputId != null) {
                writer.WritePropertyName("outputId");
                writer.Write(OutputId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Value != null) {
                writer.WritePropertyName("value");
                writer.Write(Value.ToString());
            }
            if (CreatedAt != null) {
                writer.WritePropertyName("createdAt");
                writer.Write(long.Parse(CreatedAt.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Output;
            var diff = 0;
            if (OutputId == null && OutputId == other.OutputId)
            {
                // null and null
            }
            else
            {
                diff += OutputId.CompareTo(other.OutputId);
            }
            if (Name == null && Name == other.Name)
            {
                // null and null
            }
            else
            {
                diff += Name.CompareTo(other.Name);
            }
            if (Value == null && Value == other.Value)
            {
                // null and null
            }
            else
            {
                diff += Value.CompareTo(other.Value);
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