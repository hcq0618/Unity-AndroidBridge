//  Created by hcq on 07/20/2016.
using UnityEngine;

namespace UnityAndroidBridge
{
    public static class JavaCharacter
    {
        public static AndroidJavaClass GetCharacterClass()
        {
            return new AndroidJavaClass("java.lang.Character");
        }

        public static bool IsJavaIdentifierPart(char c)
        {
            using (AndroidJavaClass characterClass = GetCharacterClass())
            {
                return characterClass.CallStatic<bool>("isJavaIdentifierPart", c);
            }
        }
    }
}

