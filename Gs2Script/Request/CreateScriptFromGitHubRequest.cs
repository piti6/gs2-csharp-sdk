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
using Gs2.Gs2Script.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Script.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class CreateScriptFromGitHubRequest : Gs2Request<CreateScriptFromGitHubRequest>
	{
        public string NamespaceName { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Gs2.Gs2Script.Model.GitHubCheckoutSetting CheckoutSetting { set; get; }
        public CreateScriptFromGitHubRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public CreateScriptFromGitHubRequest WithName(string name) {
            this.Name = name;
            return this;
        }
        public CreateScriptFromGitHubRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public CreateScriptFromGitHubRequest WithCheckoutSetting(Gs2.Gs2Script.Model.GitHubCheckoutSetting checkoutSetting) {
            this.CheckoutSetting = checkoutSetting;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static CreateScriptFromGitHubRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CreateScriptFromGitHubRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithCheckoutSetting(!data.Keys.Contains("checkoutSetting") || data["checkoutSetting"] == null ? null : Gs2.Gs2Script.Model.GitHubCheckoutSetting.FromJson(data["checkoutSetting"]));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["name"] = Name,
                ["description"] = Description,
                ["checkoutSetting"] = CheckoutSetting?.ToJson(),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (CheckoutSetting != null) {
                CheckoutSetting.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += Name + ":";
            key += Description + ":";
            key += CheckoutSetting + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply CreateScriptFromGitHubRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (CreateScriptFromGitHubRequest)x;
            return this;
        }
    }
}