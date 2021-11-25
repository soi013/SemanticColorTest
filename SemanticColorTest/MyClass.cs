using System;
using System.Linq;
namespace SemanticColorTest
{
    [Flags]
    enum MyEnum
    {
        enumTypeA,
        enumTypeB,
    }

    struct MyStruct { public int Prop { get; set; } }

    interface IMyInterface { public int Prop { get; } }

    record MyRecord : IMyInterface { public int Prop { get; set; } }

    record struct MyStrRecord { public int Prop { get; set; } }

    internal class MyClass
    {
        //privateとpublicで色の区別はできない
        private string privateField = "プライベートフィールド";
        public string PublicField = "パブリックフィールド";
        public string Property { get; set; } = "プロパティ";
        public const string CONST_VALUE = "定数";

        public string Method() => "メソッド";
        //静的メソッドは太字になる
        public static string StaticMethod() => "静的メソッド";
        internal delegate string MyDelegate();
        internal event MyDelegate? MyEvent;

        public void Show(string input)
        {
            string local = "local";

            var myClass = new MyClass();
            var myRecord = new MyRecord() as IMyInterface;

            myClass.MyEvent += () => "イベント結果";

            if (input == "INPUT")
                return;

            //呼び出すときに色で区別がつく
            new[]
            {
                input,
                local,
                MyEnum.enumTypeA.ToString(),
                new MyClass().ToString(),
               new MyRecord().ToString(),
               (new MyRecord() as IMyInterface).ToString(),
                new MyStrRecord().ToString(),
                privateField,
                PublicField,
                Property,
                CONST_VALUE,
                Method(),
                MyClass.StaticMethod(),
                myClass.MyEvent(),
            }
            .ToList()
            //拡張メソッドは普通のメソッドと色分けできる
            .ToArray() //System.Collections.Generic.List<T>クラスの普通のメソッド
            .ToArray() //System.Linq.Enumerableクラスに定義された拡張メソッド
            .ToList()
            .ForEach(x => Console.WriteLine(x));

            { { ((local)))} }
        }

        static string? GenericMethod<T>(T input) where T : notnull => input.ToString();
    }
}