//namespace BehaviorDesigner.Runtime
//{
//    [System.Serializable]
//    public class SharedT : SharedVariable<T>
//    {
//        public static implicit operator SharedT(T value)
//        {
//            return new SharedT { mValue = value };
//        }
//    }
//}


namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedBlog : SharedVariable<Blog>
    {
        public static implicit operator SharedBlog(Blog value)
        {
            return new SharedBlog { mValue = value };
        }
    }
}


