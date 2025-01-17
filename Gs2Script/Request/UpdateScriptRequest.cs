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
	public class UpdateScriptRequest : Gs2Request<UpdateScriptRequest>
	{
        public string NamespaceName { set; get; }
        public string ScriptName { set; get; }
        public string Description { set; get; }
        public string Script { set; get; }
        public UpdateScriptRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public UpdateScriptRequest WithScriptName(string scriptName) {
            this.ScriptName = scriptName;
            return this;
        }
        public UpdateScriptRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }
        public UpdateScriptRequest WithScript(string script) {
            this.Script = script;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static UpdateScriptRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateScriptRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithScriptName(!data.Keys.Contains("scriptName") || data["scriptName"] == null ? null : data["scriptName"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithScript(!data.Keys.Contains("script") || data["script"] == null ? null : data["script"].ToString());
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["scriptName"] = ScriptName,
                ["description"] = Description,
                ["script"] = Script,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (ScriptName != null) {
                writer.WritePropertyName("scriptName");
                writer.Write(ScriptName.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Script != null) {
                writer.WritePropertyName("script");
                writer.Write(Script.ToString());
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += ScriptName + ":";
            key += Description + ":";
            key += Script + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply UpdateScriptRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (UpdateScriptRequest)x;
            return this;
        }
    }
}