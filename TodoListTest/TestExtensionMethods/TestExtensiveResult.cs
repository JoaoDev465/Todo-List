using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoList.Proj.ExtensionMethods;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.ExtensionsTests;


public class TestExtensiveResult
{
    [Fact]
    public  void TestExtensiveTestModelstate_returnErrors()
    {
        var modelstateDictionaryAreReturnErrorsValues = new ModelStateDictionary();
        modelstateDictionaryAreReturnErrorsValues.AddModelError
            ("error1", "error message1");
        modelstateDictionaryAreReturnErrorsValues.AddModelError
            ("error2", "error message2");

        try
        {
            List<string> errorsInDictionaryModelState =
                ModelStateExtension.GetErrors(modelstateDictionaryAreReturnErrorsValues);
            
            Assert.IsNotNull(errorsInDictionaryModelState);
            Assert.AreEqual("error message1",errorsInDictionaryModelState[0]);
            Assert.AreEqual("error message2", errorsInDictionaryModelState[1]);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
        
    }
    
}

[TestClass]
public class ExtensiveResultsNulls
{
    [TestMethod]
    public void TestModelStateToSeeIfValueReturnNull()
    {
        var ModelStateEmptyList = new ModelStateDictionary();

       List<string> Errors = ModelStateExtension.GetErrors(ModelStateEmptyList);
       
       Assert.IsNotNull(Errors);
       Assert.AreEqual(0,Errors.Count);
        
    }
}