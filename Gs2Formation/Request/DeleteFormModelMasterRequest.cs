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
using Gs2.Gs2Formation.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Formation.Request
{
	[Preserve]
	[System.Serializable]
	public class DeleteFormModelMasterRequest : Gs2Request<DeleteFormModelMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string FormModelName { set; get; }

        public DeleteFormModelMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public DeleteFormModelMasterRequest WithFormModelName(string formModelName) {
            this.FormModelName = formModelName;
            return this;
        }

    	[Preserve]
        public static DeleteFormModelMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DeleteFormModelMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithFormModelName(!data.Keys.Contains("formModelName") || data["formModelName"] == null ? null : data["formModelName"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["formModelName"] = FormModelName,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (FormModelName != null) {
                writer.WritePropertyName("formModelName");
                writer.Write(FormModelName.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}