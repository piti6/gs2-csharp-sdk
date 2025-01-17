﻿/*
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

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Core.Model
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class RequestError
	{
		// ReSharper disable once InconsistentNaming
		public string component;
		// ReSharper disable once InconsistentNaming
		public string message;
		// ReSharper disable once InconsistentNaming
		public string code;

		public RequestError()
		{
			
		}

		public RequestError(
			string component,
			string message
		)
		{
			this.component = component;
			this.message = message;
		}

		public RequestError(
			string component,
			string message,
			string code
		)
		{
			this.component = component;
			this.message = message;
			this.code = code;
		}

		public string Component
		{
			set => component = value;
			get => component;
		}
		public string Message
		{
			set => message = value;
			get => message;
		}
		public string Code
		{
			set => code = value;
			get => code;
		}
	}
}