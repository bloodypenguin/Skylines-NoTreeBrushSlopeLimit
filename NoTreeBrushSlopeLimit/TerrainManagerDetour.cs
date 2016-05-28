using System;
using System.Reflection;
using ColossalFramework.Math;
using UnityEngine;

namespace NoTreeBrushSlopeLimit
{
    public class TerrainManagerDetour : TerrainManager
    {

        private static RedirectCallsState _state1;

        private static readonly MethodInfo Method1 = typeof(TerrainManager).GetMethod("SampleDetailHeight", new[] { typeof(Vector3), typeof(float).MakeByRefType(), typeof(float).MakeByRefType() });
        private static readonly MethodInfo Detour1 = typeof(TerrainManagerDetour).GetMethod("SampleDetailHeight", new[] { typeof(Vector3), typeof(float).MakeByRefType(), typeof(float).MakeByRefType() });
 

        private static bool _deployed;

        public static void Deploy()
        {
            if (_deployed)
            {
                return;
            }
            try
            {
                _state1 = RedirectionHelper.RedirectCalls(Method1, Detour1);
 
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            _deployed = true;
        }

        public static void Revert()
        {
            if (!_deployed)
            {
                return;
            }
            try
            {
                RedirectionHelper.RevertRedirect(Method1, _state1);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            _deployed = false;
        }

        public new float SampleDetailHeight(Vector3 worldPos, out float slopeX, out float slopeZ)
        {
            var num = (float) (SampleDetailHeight((float)(worldPos.x / 4.0 + 2160.0), (float)(worldPos.z / 4.0 + 2160.0), out slopeX, out slopeZ) * (1.0 / 64.0));
            if (ToolsModifierControl.GetCurrentTool<TreeTool>() != null ||
                ToolsModifierControl.GetCurrentTool<PropTool>() != null)
            {
                slopeX = 0;
                slopeZ = 0;
            }
            else { 
                slopeX = (float) (slopeX * (1.0 / 256.0));
                slopeZ = (float) (slopeZ * (1.0 / 256.0));
            }
            return num;
        } 
    }
}