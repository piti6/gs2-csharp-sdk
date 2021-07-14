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
using Gs2.Gs2Project.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Request
{
	[Preserve]
	[System.Serializable]
	public class GetProjectTokenRequest : Gs2Request<GetProjectTokenRequest>
	{
        public string ProjectName { set; get; }
        public string AccountToken { set; get; }

        public GetProjectTokenRequest WithProjectName(string projectName) {
            this.ProjectName = projectName;
            return this;
        }

        public GetProjectTokenRequest WithAccountToken(string accountToken) {
            this.AccountToken = accountToken;
            return this;
        }

    	[Preserve]
        public static GetProjectTokenRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new GetProjectTokenRequest()
                .WithProjectName(!data.Keys.Contains("projectName") || data["projectName"] == null ? null : data["projectName"].ToString())
                .WithAccountToken(!data.Keys.Contains("accountToken") || data["accountToken"] == null ? null : data["accountToken"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["projectName"] = ProjectName,
                ["accountToken"] = AccountToken,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (ProjectName != null) {
                writer.WritePropertyName("projectName");
                writer.Write(ProjectName.ToString());
            }
            if (AccountToken != null) {
                writer.WritePropertyName("accountToken");
                writer.Write(AccountToken.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}