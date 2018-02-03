using HouraiTeahouse;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PermutationTests {

	[Test]
	public void Generate_is_empty_when_next_is_empty() {
    var baseSet = new object[] { 1, 2, 3, 4, 5 };
    var next = new object[] {};

    Assert.AreEqual(new object[] {}, Permutation.Generate(baseSet, next));
	}

	[Test]
	public void Generate_appends_new_elements_to_end() {
    var baseSet = new object[] { 1, 2, 3, 4, 5 };
    var next = new object[] { 0, 1, 2, 4 };
    var results = new object[] {
      new object[] { 1, 2, 3, 4, 5, 0 },
      new object[] { 1, 2, 3, 4, 5, 1 },
      new object[] { 1, 2, 3, 4, 5, 2 },
      new object[] { 1, 2, 3, 4, 5, 4 },
    };

    CollectionAssert.AreEqual(results, Permutation.Generate(baseSet, next));
	}

	[Test]
	public void Generate_generates_next_if_baseset_is_empty() {
    var baseSet = new object[] {};
    var next = new object[] { 0, 1, 2, 4 };
    var results = new object[] {
      new object[] { 0 },
      new object[] { 1 },
      new object[] { 2 },
      new object[] { 4 },
    };

    CollectionAssert.AreEqual(results, Permutation.Generate(baseSet, next));
	}

	[Test]
	public void GenerateAll_generates_empty_if_no_inputs() {
    var inputs = new IEnumerable[] { };
    var results = new object[] { };

    CollectionAssert.AreEqual(results, Permutation.GenerateAll(inputs));
	}

	[Test]
	public void GenerateAll_generates_empty_if_any_inputs_are_empty() {
    var inputs1 = new IEnumerable[] { 
      new object[] { 1, 2, 3, 4, 5 },
      new object[] { },
      new object[] { 1, 2, 3 }
    };
    var inputs2 = new IEnumerable[] { 
      new object[] { },
      new object[] { 1, 2, 3, 4, 5 },
      new object[] { 1, 2, 3 }
    };
    var inputs3 = new IEnumerable[] { 
      new object[] { 1, 2, 3, 4, 5 },
      new object[] { 1, 2, 3 },
      new object[] { },
    };
    var results = new object[] { };

    CollectionAssert.AreEqual(results, Permutation.GenerateAll(inputs1));
    CollectionAssert.AreEqual(results, Permutation.GenerateAll(inputs2));
    CollectionAssert.AreEqual(results, Permutation.GenerateAll(inputs3));
	}

	[Test]
	public void GenerateAll_generates_correct_permutations() {
    var inputs = new IEnumerable[] { 
      new object[] { 1, 2, 3, 4 },
      new object[] { 4, 3, 2, 1 },
      new object[] { 1, 3, 2, 4 }
    };
    var results = new object[] { 
      new object[] { 1, 4, 1 }, new object[] { 1, 4, 3 }, new object[] { 1, 4, 2 }, new object[] { 1, 4, 4 },
      new object[] { 1, 3, 1 }, new object[] { 1, 3, 3 }, new object[] { 1, 3, 2 }, new object[] { 1, 3, 4 },
      new object[] { 1, 2, 1 }, new object[] { 1, 2, 3 }, new object[] { 1, 2, 2 }, new object[] { 1, 2, 4 },
      new object[] { 1, 1, 1 }, new object[] { 1, 1, 3 }, new object[] { 1, 1, 2 }, new object[] { 1, 1, 4 },
      new object[] { 2, 4, 1 }, new object[] { 2, 4, 3 }, new object[] { 2, 4, 2 }, new object[] { 2, 4, 4 },
      new object[] { 2, 3, 1 }, new object[] { 2, 3, 3 }, new object[] { 2, 3, 2 }, new object[] { 2, 3, 4 },
      new object[] { 2, 2, 1 }, new object[] { 2, 2, 3 }, new object[] { 2, 2, 2 }, new object[] { 2, 2, 4 },
      new object[] { 2, 1, 1 }, new object[] { 2, 1, 3 }, new object[] { 2, 1, 2 }, new object[] { 2, 1, 4 },
      new object[] { 3, 4, 1 }, new object[] { 3, 4, 3 }, new object[] { 3, 4, 2 }, new object[] { 3, 4, 4 },
      new object[] { 3, 3, 1 }, new object[] { 3, 3, 3 }, new object[] { 3, 3, 2 }, new object[] { 3, 3, 4 },
      new object[] { 3, 2, 1 }, new object[] { 3, 2, 3 }, new object[] { 3, 2, 2 }, new object[] { 3, 2, 4 },
      new object[] { 3, 1, 1 }, new object[] { 3, 1, 3 }, new object[] { 3, 1, 2 }, new object[] { 3, 1, 4 },
      new object[] { 4, 4, 1 }, new object[] { 4, 4, 3 }, new object[] { 4, 4, 2 }, new object[] { 4, 4, 4 },
      new object[] { 4, 3, 1 }, new object[] { 4, 3, 3 }, new object[] { 4, 3, 2 }, new object[] { 4, 3, 4 },
      new object[] { 4, 2, 1 }, new object[] { 4, 2, 3 }, new object[] { 4, 2, 2 }, new object[] { 4, 2, 4 },
      new object[] { 4, 1, 1 }, new object[] { 4, 1, 3 }, new object[] { 4, 1, 2 }, new object[] { 4, 1, 4 },
    };

    CollectionAssert.AreEqual(results, Permutation.GenerateAll(inputs));
	}

}
