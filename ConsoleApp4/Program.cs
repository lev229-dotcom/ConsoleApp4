using ConsoleApp4;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

MyValue myval1 = new MyValue() { MyV1 = "myv1", MyV2 = "myv2"};

MyValue myval2 = new MyValue() { MyV1 = "myv1", MyV2 = "myv2"};

var el1 = new List<Model>() { new Model() { Key = "1", Value = myval1 } };

var el2 = new List<Model>() { new Model() { Key = "1", Value = myval2 } };


Console.WriteLine(el1 == el2);
Console.WriteLine(el1.Equals(el2));
Console.WriteLine(el1.Contains(el2.First()));


var el1Ser = JsonConvert.SerializeObject(el1, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});

var el2Ser = JsonConvert.SerializeObject(el2, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
Console.WriteLine(el1Ser == el2Ser);
Console.WriteLine(el1Ser.SequenceEqual(el2Ser));
Console.WriteLine(el1Ser.Contains(el2Ser.First()));

var el1Des = JsonConvert.DeserializeObject<List<Model>>(el1Ser, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

var el2Des = JsonConvert.DeserializeObject<List<Model>>(el2Ser, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

Console.WriteLine(el1Des == el2Des);
Console.WriteLine(el1Des.Equals(el2Des));
Console.WriteLine(el1Des.Contains(el2Des.First()));


ProductA[] storeA = { new ProductA { Name = "apple", Code = 9 },
                       new ProductA { Name = "orange", Code = 4 } };

ProductA[] storeB = { new ProductA { Name = "apple", Code = 9 },
                       new ProductA { Name = "orange", Code = 4 } };

var storeAel1Ser = JsonConvert.SerializeObject(storeA, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

var storeBel2Ser = JsonConvert.SerializeObject(storeB, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

var storeAdes = JsonConvert.DeserializeObject<List<ProductA>>(storeAel1Ser, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

var storeBdes = JsonConvert.DeserializeObject<List<ProductA>>(storeBel2Ser, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });


bool equalAB = storeAdes.Contains(storeBdes.First());

Console.WriteLine("Equal? " + equalAB);

public class ProductA : IEquatable<ProductA>
{
    public string Name { get; set; }
    public int Code { get; set; }

    public bool Equals(ProductA other)
    {
        if (other is null)
            return false;

        return this.Name == other.Name && this.Code == other.Code;
    }

    public override bool Equals(object obj) => Equals(obj as ProductA);
    public override int GetHashCode() => (Name, Code).GetHashCode();
}

public class Model : IEquatable<Model>
{
    public string Key { get; set; }

    public MyValue Value { get; set; }

    public bool Equals(Model other)
    {
        if (other == null)
            return false;
        return this.Key == other.Key 
            && this.Value.MyV1 == other.Value.MyV1
            && this.Value.MyV2 == other.Value.MyV2
            ;
    }

    public override bool Equals(object obj) => Equals(obj as Model);

    public override int GetHashCode() => (Key, Value).GetHashCode();


}

public class MyValue
{
    public string MyV1 { get; set; }
    public string MyV2 { get; set; }

    public bool Equals(MyValue other)
    {
        if (other == null)
            return false;
        return this.MyV1 == other.MyV1 && this.MyV2 == other.MyV2;
    }

    public override bool Equals(object obj) => Equals(obj as MyValue);

    public override int GetHashCode() => (MyV1, MyV2).GetHashCode();
}