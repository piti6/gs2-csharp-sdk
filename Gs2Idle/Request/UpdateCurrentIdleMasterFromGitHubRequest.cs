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
using Gs2.Gs2Idle.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Idle.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class UpdateCurrentIdleMasterFromGitHubRequest : Gs2Request<UpdateCurrentIdleMasterFromGitHubRequest>
	{
        public string NamespaceName { set; get; }
        public Gs2.Gs2Idle.Model.GitHubCheckoutSetting CheckoutSetting { set; get; }
        public UpdateCurrentIdleMasterFromGitHubRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public UpdateCurrentIdleMasterFromGitHubRequest WithCheckoutSetting(Gs2.Gs2Idle.Model.GitHubCheckoutSetting checkoutSetting) {
            this.CheckoutSetting = checkoutSetting;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static UpdateCurrentIdleMasterFromGitHubRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateCurrentIdleMasterFromGitHubRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithCheckoutSetting(!data.Keys.Contains("checkoutSetting") || data["checkoutSetting"] == null ? null : Gs2.Gs2Idle.Model.GitHubCheckoutSetting.FromJson(data["checkoutSetting"]));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
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
            if (CheckoutSetting != null) {
                CheckoutSetting.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += CheckoutSetting + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply UpdateCurrentIdleMasterFromGitHubRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (UpdateCurrentIdleMasterFromGitHubRequest)x;
            return this;
        }
    }
}