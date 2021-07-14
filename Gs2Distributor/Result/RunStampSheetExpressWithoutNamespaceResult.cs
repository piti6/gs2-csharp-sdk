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
using Gs2.Gs2Distributor.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Distributor.Result
{
	[Preserve]
	[System.Serializable]
	public class RunStampSheetExpressWithoutNamespaceResult : IResult
	{
        public string[] TaskResults { set; get; }
        public string SheetResult { set; get; }

        public RunStampSheetExpressWithoutNamespaceResult WithTaskResults(string[] taskResults) {
            this.TaskResults = taskResults;
            return this;
        }

        public RunStampSheetExpressWithoutNamespaceResult WithSheetResult(string sheetResult) {
            this.SheetResult = sheetResult;
            return this;
        }

    	[Preserve]
        public static RunStampSheetExpressWithoutNamespaceResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new RunStampSheetExpressWithoutNamespaceResult()
                .WithTaskResults(!data.Keys.Contains("taskResults") || data["taskResults"] == null ? new string[]{} : data["taskResults"].Cast<JsonData>().Select(v => {
                    return v.ToString();
                }).ToArray())
                .WithSheetResult(!data.Keys.Contains("sheetResult") || data["sheetResult"] == null ? null : data["sheetResult"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["taskResults"] = new JsonData(TaskResults == null ? new JsonData[]{} :
                        TaskResults.Select(v => {
                            return new JsonData(v.ToString());
                        }).ToArray()
                    ),
                ["sheetResult"] = SheetResult,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteArrayStart();
            foreach (var taskResult in TaskResults)
            {
                if (taskResult != null) {
                    writer.Write(taskResult.ToString());
                }
            }
            writer.WriteArrayEnd();
            if (SheetResult != null) {
                writer.WritePropertyName("sheetResult");
                writer.Write(SheetResult.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}