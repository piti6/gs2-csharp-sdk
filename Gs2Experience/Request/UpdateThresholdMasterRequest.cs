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
	public class UpdateThresholdMasterRequest : Gs2Request<UpdateThresholdMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string ThresholdName { set; get; }
        public string Description { set; get; }
        public string Metadata { set; get; }
        public long[] Values { set; get; }
        public UpdateThresholdMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public UpdateThresholdMasterRequest WithThresholdName(string thresholdName) {
            this.ThresholdName = thresholdName;
            return this;
        }
        public UpdateThresholdMasterRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public UpdateThresholdMasterRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public UpdateThresholdMasterRequest WithValues(long[] values) {
            this.Values = values;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static UpdateThresholdMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateThresholdMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithThresholdName(!data.Keys.Contains("thresholdName") || data["thresholdName"] == null ? null : data["thresholdName"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithValues(!data.Keys.Contains("values") || data["values"] == null ? new long[]{} : data["values"].Cast<JsonData>().Select(v => {
                    return long.Parse(v.ToString());
                }).ToArray());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["thresholdName"] = ThresholdName,
                ["description"] = Description,
                ["metadata"] = Metadata,
                ["values"] = Values == null ? null : new JsonData(
                        Values.Select(v => {
                            return new JsonData((long?)long.Parse(v.ToString()));
                        }).ToArray()
                    ),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (ThresholdName != null) {
                writer.WritePropertyName("thresholdName");
                writer.Write(ThresholdName.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            writer.WriteArrayStart();
            foreach (var value in Values)
            {
                writer.Write(long.Parse(value.ToString()));
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += ThresholdName + ":";
            key += Description + ":";
            key += Metadata + ":";
            key += Values + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply UpdateThresholdMasterRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (UpdateThresholdMasterRequest)x;
            return this;
        }
    }
}