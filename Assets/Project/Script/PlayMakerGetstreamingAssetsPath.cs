using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("取得裝置streamingAssets")]
	public class PlayMakerGetstreamingAssetsPath : FsmStateAction
	{
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storePath;
		

	
		public override void OnEnter()
        {
            storePath.Value = Application.streamingAssetsPath + "/";
        }
		

	}
}