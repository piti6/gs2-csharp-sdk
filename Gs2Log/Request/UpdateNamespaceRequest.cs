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
using Gs2.Gs2Log.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Log.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{
        public string NamespaceName { set; get; }
        public string Description { set; get; }
        public string Type { set; get; }
        public string GcpCredentialJson { set; get; }
        public string BigQueryDatasetName { set; get; }
        public int? LogExpireDays { set; get; }
        public string AwsRegion { set; get; }
        public string AwsAccessKeyId { set; get; }
        public string AwsSecretAccessKey { set; get; }
        public string FirehoseStreamName { set; get; }

        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public UpdateNamespaceRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }

        public UpdateNamespaceRequest WithType(string type) {
            this.Type = type;
            return this;
        }

        public UpdateNamespaceRequest WithGcpCredentialJson(string gcpCredentialJson) {
            this.GcpCredentialJson = gcpCredentialJson;
            return this;
        }

        public UpdateNamespaceRequest WithBigQueryDatasetName(string bigQueryDatasetName) {
            this.BigQueryDatasetName = bigQueryDatasetName;
            return this;
        }

        public UpdateNamespaceRequest WithLogExpireDays(int? logExpireDays) {
            this.LogExpireDays = logExpireDays;
            return this;
        }

        public UpdateNamespaceRequest WithAwsRegion(string awsRegion) {
            this.AwsRegion = awsRegion;
            return this;
        }

        public UpdateNamespaceRequest WithAwsAccessKeyId(string awsAccessKeyId) {
            this.AwsAccessKeyId = awsAccessKeyId;
            return this;
        }

        public UpdateNamespaceRequest WithAwsSecretAccessKey(string awsSecretAccessKey) {
            this.AwsSecretAccessKey = awsSecretAccessKey;
            return this;
        }

        public UpdateNamespaceRequest WithFirehoseStreamName(string firehoseStreamName) {
            this.FirehoseStreamName = firehoseStreamName;
            return this;
        }

    	[Preserve]
        public static UpdateNamespaceRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateNamespaceRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithType(!data.Keys.Contains("type") || data["type"] == null ? null : data["type"].ToString())
                .WithGcpCredentialJson(!data.Keys.Contains("gcpCredentialJson") || data["gcpCredentialJson"] == null ? null : data["gcpCredentialJson"].ToString())
                .WithBigQueryDatasetName(!data.Keys.Contains("bigQueryDatasetName") || data["bigQueryDatasetName"] == null ? null : data["bigQueryDatasetName"].ToString())
                .WithLogExpireDays(!data.Keys.Contains("logExpireDays") || data["logExpireDays"] == null ? null : (int?)int.Parse(data["logExpireDays"].ToString()))
                .WithAwsRegion(!data.Keys.Contains("awsRegion") || data["awsRegion"] == null ? null : data["awsRegion"].ToString())
                .WithAwsAccessKeyId(!data.Keys.Contains("awsAccessKeyId") || data["awsAccessKeyId"] == null ? null : data["awsAccessKeyId"].ToString())
                .WithAwsSecretAccessKey(!data.Keys.Contains("awsSecretAccessKey") || data["awsSecretAccessKey"] == null ? null : data["awsSecretAccessKey"].ToString())
                .WithFirehoseStreamName(!data.Keys.Contains("firehoseStreamName") || data["firehoseStreamName"] == null ? null : data["firehoseStreamName"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["description"] = Description,
                ["type"] = Type,
                ["gcpCredentialJson"] = GcpCredentialJson,
                ["bigQueryDatasetName"] = BigQueryDatasetName,
                ["logExpireDays"] = LogExpireDays,
                ["awsRegion"] = AwsRegion,
                ["awsAccessKeyId"] = AwsAccessKeyId,
                ["awsSecretAccessKey"] = AwsSecretAccessKey,
                ["firehoseStreamName"] = FirehoseStreamName,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Type != null) {
                writer.WritePropertyName("type");
                writer.Write(Type.ToString());
            }
            if (GcpCredentialJson != null) {
                writer.WritePropertyName("gcpCredentialJson");
                writer.Write(GcpCredentialJson.ToString());
            }
            if (BigQueryDatasetName != null) {
                writer.WritePropertyName("bigQueryDatasetName");
                writer.Write(BigQueryDatasetName.ToString());
            }
            if (LogExpireDays != null) {
                writer.WritePropertyName("logExpireDays");
                writer.Write(int.Parse(LogExpireDays.ToString()));
            }
            if (AwsRegion != null) {
                writer.WritePropertyName("awsRegion");
                writer.Write(AwsRegion.ToString());
            }
            if (AwsAccessKeyId != null) {
                writer.WritePropertyName("awsAccessKeyId");
                writer.Write(AwsAccessKeyId.ToString());
            }
            if (AwsSecretAccessKey != null) {
                writer.WritePropertyName("awsSecretAccessKey");
                writer.Write(AwsSecretAccessKey.ToString());
            }
            if (FirehoseStreamName != null) {
                writer.WritePropertyName("firehoseStreamName");
                writer.Write(FirehoseStreamName.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}