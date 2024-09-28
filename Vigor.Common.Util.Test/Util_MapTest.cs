namespace Vigor.Common.Util.Test;

public partial class UtilTest
{
#nullable disable
  [Fact]
  public void Map_DestIsNull_ThrowsArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => Util.Map<object, object>(null, new object()));
  }

  [Fact]
  public void Map_SrcIsNull_ThrowsArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => Util.Map<object, object>(new object(), null));
  }

  [Fact]
  public void Map_FailedToCreateDest_ThrowsMissingMethodException()
  {
    Assert.Throws<MissingMethodException>(
      () => Util.Map<Person>(new PersonView("Isaac", 7)));
  }
#nullable restore

  [Fact]
  public void Map_WithoutSpecifyingIncludedProperties_MapsSrcUsingAllSrcProperties()
  {
    var dest = new Person("Isaac", 7);
    var src = new PersonView("Ari", 8);

    Util.Map(dest, src);

    Assert.Equal(src.Name, dest.Name);
    Assert.Equal(src.Age, dest.Age);
  }

  [Fact]
  public void Map_SpecicIncludedProperties_MapsSrcUsingTheSpecifiedProperties()
  {
    var name = "Isaac";
    var dest = new Person(name, 7);
    var src = new PersonView("Ari", 8);

    Util.Map(dest, src, [nameof(src.Age)]);

    Assert.NotEqual(src.Name, dest.Name);
    Assert.Equal(name, dest.Name);
    Assert.Equal(src.Age, dest.Age);
  }

  [Fact]
  public void Map_WithoutDestArg_InstantiatesAndMap()
  {
    var src = new PersonView("Ari", 8);
    var dest = Util.Map<Creature>(src);

    Assert.Equal(src.Name, dest.Name);
    Assert.Equal(src.Age, dest.Age);
  }

  [Fact]
  public void Map_SrcPropertyIsNull_Skips()
  {
    var expectedJob = "Teacher";
    var dest = new Person("Isaac", 7) { Job = expectedJob };
    var src = new PersonView("Ari", 8);

    Util.Map(dest, src);

    Assert.Equal(src.Name, dest.Name);
    Assert.Equal(src.Age, dest.Age);
    Assert.Equal(expectedJob, dest.Job);
  }
}

class Person(string name, int age)
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string Name { get; set; } = name;
  public int Age { get; set; } = age;
  public string? Job { get; set; }
}

class Creature
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string? Name { get; set; }
  public int? Age { get; set; }
  public string? Job { get; set; }
}

class PersonView(string name, int age)
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string Name { get; set; } = name;
  public int Age { get; set; } = age;
  public string? Job { get; set; }
}
