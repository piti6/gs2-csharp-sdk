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
using Gs2.Gs2Version.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Version.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class UpdateVersionModelMasterRequest : Gs2Request<UpdateVersionModelMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string VersionName { set; get; }
        public string Description { set; get; }
        public string Metadata { set; get; }
        public Gs2.Gs2Version.Model.Version_ WarningVersion { set; get; }
        public Gs2.Gs2Version.Model.Version_ ErrorVersion { set; get; }
        public string Scope { set; get; }
        public Gs2.Gs2Version.Model.Version_ CurrentVersion { set; get; }
        public bool? NeedSignature { set; get; }
        public string SignatureKeyId { set; get; }
        public UpdateVersionModelMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public UpdateVersionModelMasterRequest WithVersionName(string versionName) {
            this.VersionName = versionName;
            return this;
        }
        public UpdateVersionModelMasterRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public UpdateVersionModelMasterRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }
        public UpdateVersionModelMasterRequest WithWarningVersion(Gs2.Gs2Version.Model.Version_ warningVersion) {
            this.WarningVersion = warningVersion;
            return this;
        }
        public UpdateVersionModelMasterRequest WithErrorVersion(Gs2.Gs2Version.Model.Version_ errorVersion) {
            this.ErrorVersion = errorVersion;
            return this;
        }
        public UpdateVersionModelMasterRequest WithScope(string scope) {
            this.Scope = scope;
            return this;
        }
        public UpdateVersionModelMasterRequest WithCurrentVersion(Gs2.Gs2Version.Model.Version_ currentVersion) {
            this.CurrentVersion = currentVersion;
            return this;
        }
        public UpdateVersionModelMasterRequest WithNeedSignature(bool? needSignature) {
            this.NeedSignature = needSignature;
            return this;
        }
        public UpdateVersionModelMasterRequest WithSignatureKeyId(string signatureKeyId) {
            this.SignatureKeyId = signatureKeyId;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static UpdateVersionModelMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateVersionModelMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithVersionName(!data.Keys.Contains("versionName") || data["versionName"] == null ? null : data["versionName"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithWarningVersion(!data.Keys.Contains("warningVersion") || data["warningVersion"] == null ? null : Gs2.Gs2Version.Model.Version_.FromJson(data["warningVersion"]))
                .WithErrorVersion(!data.Keys.Contains("errorVersion") || data["errorVersion"] == null ? null : Gs2.Gs2Version.Model.Version_.FromJson(data["errorVersion"]))
                .WithScope(!data.Keys.Contains("scope") || data["scope"] == null ? null : data["scope"].ToString())
                .WithCurrentVersion(!data.Keys.Contains("currentVersion") || data["currentVersion"] == null ? null : Gs2.Gs2Version.Model.Version_.FromJson(data["currentVersion"]))
                .WithNeedSignature(!data.Keys.Contains("needSignature") || data["needSignature"] == null ? null : (bool?)bool.Parse(data["needSignature"].ToString()))
                .WithSignatureKeyId(!data.Keys.Contains("signatureKeyId") || data["signatureKeyId"] == null ? null : data["signatureKeyId"].ToString());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["versionName"] = VersionName,
                ["description"] = Description,
                ["metadata"] = Metadata,
                ["warningVersion"] = WarningVersion?.ToJson(),
                ["errorVersion"] = ErrorVersion?.ToJson(),
                ["scope"] = Scope,
                ["currentVersion"] = CurrentVersion?.ToJson(),
                ["needSignature"] = NeedSignature,
                ["signatureKeyId"] = SignatureKeyId,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (VersionName != null) {
                writer.WritePropertyName("versionName");
                writer.Write(VersionName.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (WarningVersion != null) {
                WarningVersion.WriteJson(writer);
            }
            if (ErrorVersion != null) {
                ErrorVersion.WriteJson(writer);
            }
            if (Scope != null) {
                writer.WritePropertyName("scope");
                writer.Write(Scope.ToString());
            }
            if (CurrentVersion != null) {
                CurrentVersion.WriteJson(writer);
            }
            if (NeedSignature != null) {
                writer.WritePropertyName("needSignature");
                writer.Write(bool.Parse(NeedSignature.ToString()));
            }
            if (SignatureKeyId != null) {
                writer.WritePropertyName("signatureKeyId");
                writer.Write(SignatureKeyId.ToString());
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += VersionName + ":";
            key += Description + ":";
            key += Metadata + ":";
            key += WarningVersion + ":";
            key += ErrorVersion + ":";
            key += Scope + ":";
            key += CurrentVersion + ":";
            key += NeedSignature + ":";
            key += SignatureKeyId + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply UpdateVersionModelMasterRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (UpdateVersionModelMasterRequest)x;
            return this;
        }
    }
}