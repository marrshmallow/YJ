using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 아바타가 없는 모델링의 아바타를 생성해 주는 스크립트입니다.
/// 생성을 원하는 모델링을 Hierarchy에서 선택 후
/// 상단 메뉴 CustomTools에서 원하는 아바타 종류를 선택하여 생성합니다.
/// 이 때 뼈 구조와 명칭은 이 스크립트에 정의된 것과 일치해야 합니다.
/// 
/// - 정진솔
/// </summary>

namespace Infrastructure.Editor
{
    public class GenerateAvatar : MonoBehaviour
    {
        [MenuItem("CustomTools/MakeAvatarMask")]
        private static void MakeAvatarMask()
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if (activeGameObject != null)
            {
                AvatarMask avatarMask = new AvatarMask();

                avatarMask.AddTransformPath(activeGameObject.transform);

                var path = string.Format("Assets/{0}.mask", activeGameObject.name.Replace(':', '_'));
                AssetDatabase.CreateAsset(avatarMask, path);
            }
        }

        [MenuItem("CustomTools/MakeAvatar")]
        private static void MakeAvatar()
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if (activeGameObject != null)
            {
                Avatar avatar = AvatarBuilder.BuildGenericAvatar(activeGameObject, "");
                avatar.name = activeGameObject.name;
                Debug.Log(avatar.isHuman ? "is human" : "is generic");

                var path = string.Format("Assets/{0}.ht", avatar.name.Replace(':', '_'));
                AssetDatabase.CreateAsset(avatar, path);
            }
        }

        [MenuItem("CustomTools/MakeBipedAvatar")]
        private static void MakeBipedAvatar()
        {
            var activeGameObject = Selection.activeGameObject;

            if (activeGameObject == null) return;

            var boneMapping = new Dictionary<string, string>
                        {
                            {"Hips", "Bip001 Pelvis"},
                            {"LeftUpperLeg", "Bip001 L Thigh"},
                            {"RightUpperLeg", "Bip001 R Thigh"},
                            {"LeftLowerLeg", "Bip001 L Calf"},
                            {"RightLowerLeg", "Bip001 R Calf"},
                            {"LeftFoot", "Bip001 L Foot"},
                            {"RightFoot", "Bip001 R Foot"},
                            {"Spine", "Bip001 Spine"},
                            {"Chest", "Bip001 Spine1"},
                            {"Neck", "Bip001 Neck"},
                            {"Head", "Bip001 Head"},
                            {"LeftShoulder", "Bip001 L Clavicle"},
                            {"RightShoulder", "Bip001 R Clavicle"},
                            {"LeftUpperArm", "Bip001 L UpperArm"},
                            {"RightUpperArm", "Bip001 R UpperArm"},
                            {"LeftLowerArm", "Bip001 L Forearm"},
                            {"RightLowerArm", "Bip001 R Forearm"},
                            {"LeftHand", "Bip001 L Hand"},
                            {"RightHand", "Bip001 R Hand"},
                            {"LeftToes", "Bip001 L Toe0"},
                            {"RightToes", "Bip001 R Toe0"},
                            {"UpperChest", "Bip001 Spine2"},
                        };

            var humanDescription = new HumanDescription
            {
                human = boneMapping.Select(mapping =>
                {
                    var bone = new HumanBone { humanName = mapping.Key, boneName = mapping.Value };
                    bone.limit.useDefaultValues = true;
                    return bone;
                }).ToArray(),
            };

            var avatar = AvatarBuilder.BuildHumanAvatar(activeGameObject, humanDescription);
            avatar.name = activeGameObject.name;

            if (!avatar.isValid)
            {
                Debug.LogError("Invalid avatar");
                return;
            }

            Debug.Log(avatar.isHuman ? "is human" : "is generic");

            var path = $"Assets/{avatar.name.Replace(':', '_')}.ht";
            AssetDatabase.CreateAsset(avatar, path);

        }

        [MenuItem("CustomTools/MakeHumanAvatar")]
        private static void MakeHumanAvatar()
        {
            var activeGameObject = Selection.activeGameObject;

            if (activeGameObject == null) return;

            /*            var boneMapping = new Dictionary<string, string>
                        {
                            {"Hips", "DEF-spine"},
                            {"LeftUpperLeg", "DEF-thigh.L"},
                            {"RightUpperLeg", "DEF-thigh.R"},
                            {"LeftLowerLeg", "DEF-shin.L"},
                            {"RightLowerLeg", "DEF-shin.R"},
                            {"LeftFoot", "DEF-foot.L"},
                            {"RightFoot", "DEF-foot.R"},
                            {"Spine", "DEF-spine.001"},
                            {"Chest", "DEF-spine.002"},
                            {"Neck", "DEF-neck"},
                            {"Head", "DEF-head"},
                            {"LeftShoulder", "DEF-shoulder.L"},
                            {"RightShoulder", "DEF-shoulder.R"},
                            {"LeftUpperArm", "DEF-upper_arm.L"},
                            {"RightUpperArm", "DEF-upper_arm.R"},
                            {"LeftLowerArm", "DEF-forearm.L"},
                            {"RightLowerArm", "DEF-forearm.R"},
                            {"LeftHand", "DEF-hand.L"},
                            {"RightHand", "DEF-hand.R"},
                            {"LeftToes", "DEF-toe.L"},
                            {"RightToes", "DEF-toe.R"},
                            {"UpperChest", "DEF-spine.003"},
                        };*/

            var boneMapping = new Dictionary<string, string>
            {
                {"Hips", "torso_joint_1"},
                {"LeftUpperLeg", "leg_joint_L_1"},
                {"RightUpperLeg", "leg_joint_R_1"},
                {"LeftLowerLeg", "leg_joint_L_2"},
                {"RightLowerLeg", "leg_joint_R_2"},
                {"LeftFoot", "leg_joint_L_3"},
                {"RightFoot", "leg_joint_R_3"},
                {"Spine", "torso_joint_2"},
                {"Chest", "torso_joint_3"},
                {"Neck", "neck_joint_1"},
                {"Head", "neck_joint_2"},
                {"LeftUpperArm", "arm_joint_L_1"},
                {"RightUpperArm", "arm_joint_R_1"},
                {"LeftLowerArm", "arm_joint_L_2"},
                {"RightLowerArm", "arm_joint_R_2"},
                {"LeftHand", "arm_joint_L_3"},
                {"RightHand", "arm_joint_R_3"},
                {"LeftToes", "leg_joint_L_5"},
                {"RightToes", "leg_joint_R_5"},
            };

            var humanDescription = new HumanDescription
            {
                human = boneMapping.Select(mapping =>
                {
                    var bone = new HumanBone { humanName = mapping.Key, boneName = mapping.Value };
                    bone.limit.useDefaultValues = true;
                    return bone;
                }).ToArray(),
            };

            var avatar = AvatarBuilder.BuildHumanAvatar(activeGameObject, humanDescription);
            avatar.name = activeGameObject.name;

            if (!avatar.isValid)
            {
                Debug.LogError("Invalid avatar");
                return;
            }

            Debug.Log(avatar.isHuman ? "is human" : "is generic");

            var path = $"Assets/{avatar.name.Replace(':', '_')}.ht";
            AssetDatabase.CreateAsset(avatar, path);
        }
    }
}
