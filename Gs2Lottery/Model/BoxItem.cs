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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Lottery.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class BoxItem : IComparable
	{
        public Gs2.Core.Model.AcquireAction[] AcquireActions { set; get; }
        public int? Remaining { set; get; }
        public int? Initial { set; get; }
        public BoxItem WithAcquireActions(Gs2.Core.Model.AcquireAction[] acquireActions) {
            this.AcquireActions = acquireActions;
            return this;
        }
        public BoxItem WithRemaining(int? remaining) {
            this.Remaining = remaining;
            return this;
        }
        public BoxItem WithInitial(int? initial) {
            this.Initial = initial;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static BoxItem FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new BoxItem()
                .WithAcquireActions(!data.Keys.Contains("acquireActions") || data["acquireActions"] == null ? new Gs2.Core.Model.AcquireAction[]{} : data["acquireActions"].Cast<JsonData>().Select(v => {
                    return Gs2.Core.Model.AcquireAction.FromJson(v);
                }).ToArray())
                .WithRemaining(!data.Keys.Contains("remaining") || data["remaining"] == null ? null : (int?)int.Parse(data["remaining"].ToString()))
                .WithInitial(!data.Keys.Contains("initial") || data["initial"] == null ? null : (int?)int.Parse(data["initial"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["acquireActions"] = AcquireActions == null ? null : new JsonData(
                        AcquireActions.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["remaining"] = Remaining,
                ["initial"] = Initial,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (AcquireActions != null) {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach (var acquireAction in AcquireActions)
                {
                    if (acquireAction != null) {
                        acquireAction.WriteJson(writer);
                    }
                }
                writer.WriteArrayEnd();
            }
            if (Remaining != null) {
                writer.WritePropertyName("remaining");
                writer.Write(int.Parse(Remaining.ToString()));
            }
            if (Initial != null) {
                writer.WritePropertyName("initial");
                writer.Write(int.Parse(Initial.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as BoxItem;
            var diff = 0;
            if (AcquireActions == null && AcquireActions == other.AcquireActions)
            {
                // null and null
            }
            else
            {
                diff += AcquireActions.Length - other.AcquireActions.Length;
                for (var i = 0; i < AcquireActions.Length; i++)
                {
                    diff += AcquireActions[i].CompareTo(other.AcquireActions[i]);
                }
            }
            if (Remaining == null && Remaining == other.Remaining)
            {
                // null and null
            }
            else
            {
                diff += (int)(Remaining - other.Remaining);
            }
            if (Initial == null && Initial == other.Initial)
            {
                // null and null
            }
            else
            {
                diff += (int)(Initial - other.Initial);
            }
            return diff;
        }
    }
}