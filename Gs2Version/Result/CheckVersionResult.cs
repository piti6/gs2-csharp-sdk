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
using UnityEngine.Scripting;

namespace Gs2.Gs2Version.Result
{
	[Preserve]
	[System.Serializable]
	public class CheckVersionResult : IResult
	{
        public string ProjectToken { set; get; }
        public Gs2.Gs2Version.Model.Status[] Warnings { set; get; }
        public Gs2.Gs2Version.Model.Status[] Errors { set; get; }

        public CheckVersionResult WithProjectToken(string projectToken) {
            this.ProjectToken = projectToken;
            return this;
        }

        public CheckVersionResult WithWarnings(Gs2.Gs2Version.Model.Status[] warnings) {
            this.Warnings = warnings;
            return this;
        }

        public CheckVersionResult WithErrors(Gs2.Gs2Version.Model.Status[] errors) {
            this.Errors = errors;
            return this;
        }

    	[Preserve]
        public static CheckVersionResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CheckVersionResult()
                .WithProjectToken(!data.Keys.Contains("projectToken") || data["projectToken"] == null ? null : data["projectToken"].ToString())
                .WithWarnings(!data.Keys.Contains("warnings") || data["warnings"] == null ? new Gs2.Gs2Version.Model.Status[]{} : data["warnings"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Version.Model.Status.FromJson(v);
                }).ToArray())
                .WithErrors(!data.Keys.Contains("errors") || data["errors"] == null ? new Gs2.Gs2Version.Model.Status[]{} : data["errors"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Version.Model.Status.FromJson(v);
                }).ToArray());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["projectToken"] = ProjectToken,
                ["warnings"] = new JsonData(Warnings == null ? new JsonData[]{} :
                        Warnings.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["errors"] = new JsonData(Errors == null ? new JsonData[]{} :
                        Errors.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (ProjectToken != null) {
                writer.WritePropertyName("projectToken");
                writer.Write(ProjectToken.ToString());
            }
            writer.WriteArrayStart();
            foreach (var warning in Warnings)
            {
                if (warning != null) {
                    warning.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteArrayStart();
            foreach (var error in Errors)
            {
                if (error != null) {
                    error.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }
    }
}